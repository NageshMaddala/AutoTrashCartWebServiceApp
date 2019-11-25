using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using AutoTrashCartWebServiceApp.Interfaces;
using AutoTrashCartWebServiceApp.Models;
using Dapper;

namespace AutoTrashCartWebServiceApp.DAL
{
    public class AutoTrashCartRepository : IAutoTrashCartRepository
    {
        private readonly IDbConnection _db;

        public AutoTrashCartRepository()
        {
            _db = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        }

        public Schedule GetSchedule(string token)
        {
            return _db.Query<Schedule>("Select * from [Schedule] where Token=" + token).FirstOrDefault();
        }

        public bool SetSchedule(Schedule schedule)
        {
            int rowsAffected = _db.Execute(
                @"IF EXISTS (SELECT TOP 1 * FROM [DBO].[SCHEDULE] WHERE TOKEN = @Token)
                        UPDATE [DBO].[SCHEDULE] SET [Day] = @Day, [Pickup] = @Pickup, [Holidays] = @Holidays WHERE Token = @Token
                     ELSE
                        INSERT INTO [DBO].[SCHEDULE] ([Token],[Day],[Pickup],[Holidays]) VALUES (@Token, @Day, @Pickup, @Holidays)",
                new
                {
                    Token = schedule.Token,
                    Day = schedule.Day,
                    Pickup = schedule.Pickup,
                    Holidays = schedule.Holidays
                });

            if (rowsAffected > 0)
            {
                return true;
            }

            return false;
        }

        public Path GetPath(string token)
        {
            var location = _db.Query<Location>("SELECT * FROM [LOCATION] WHERE TOKEN =" + token).ToArray<Location>();

            var orientation = _db.Query<Orientation>("SELECT * FROM [ORIENTATION] WHERE TOKEN =" + token).ToArray<Orientation>();

            var path = new Path(token, location, orientation);

            return path;
        }

        public bool SetPath(string token, string s, string e, string[] leftb, string[] rightb, string[] centerl, string so, string eo)
        {
            InsertStartingAndEndingPoints(token, s, "s");
            InsertStartingAndEndingPoints(token, e, "e");
            InsertLocation(token, leftb, "leftb");
            InsertLocation(token, rightb, "rightb");
            InsertLocation(token, centerl, "centerl");
            InsertOrientation(token, "so", so);
            InsertOrientation(token, "eo", eo);
            return true;
        }

        public void InsertOrientation(string token, string keyword, string orientation)
        {
            string[] points = orientation.Split(',').ToArray();

            if (points.Length > 0)
            {
                int insertOrientation = _db.Execute(
                    @"IF EXISTS (SELECT TOP 1 * FROM [DBO].[ORIENTATION] WHERE Token = @Token AND [OrientationType] = @OrientationType)
						 UPDATE [DBO].[ORIENTATION] SET [OrientationType] = @OrientationType, [X] = @X, [Y] = @Y, [Z] = @Z 
                         WHERE Token = @Token AND [OrientationType] = @OrientationType
					ELSE
						 INSERT INTO [DBO].[ORIENTATION] ([Token],[OrientationType],[X],[Y],[Z]) VALUES (@Token, @OrientationType, @X, @Y, @Z)",
                    new
                    {
                        Token = token,
                        OrientationType = keyword,
                        X = points[0],
                        Y = points[1],
                        Z = points[2]
                    });
            }
        }

        public void InsertStartingAndEndingPoints(string token, string point, string keyword)
        {
            string[] points = point.Split(',').ToArray();

            if (points.Length > 0)
            {
                int insertStartingPoints = _db.Execute(
                    @"IF EXISTS (SELECT TOP 1 * FROM [DBO].[LOCATION] WHERE Token = @Token AND [LocationType] = @LocationType)
						UPDATE [dbo].[Location] SET [LocationType] = @LocationType,[Latitude0] = @Latitude0,[Longitude0] = @Longitude0
                        WHERE Token = @Token AND [LocationType] = @LocationType
					ELSE
						INSERT INTO [dbo].[Location]([Token],[LocationType],[Latitude0],[Longitude0]) VALUES (@Token, @LocationType, @Latitude0, @Longitude0)",
                    new
                    {
                        Token = token,
                        LocationType = keyword,
                        Latitude0 = points[0],
                        Longitude0 = points[1]
                    });
            }
        }

        public void InsertLocation(string token, string[] points, string keyword)
        {
            if (points == null)
                return;

            if (points.Length < 3)
                return;

            string[] point0 = points[0].Split(',').ToArray();
            string[] point1 = points[1].Split(',').ToArray();
            string[] point2 = points[2].Split(',').ToArray();

            int insertStartingPoints = _db.Execute(
                @"IF EXISTS (SELECT TOP 1 * FROM [DBO].[LOCATION] WHERE Token = @Token AND [LocationType] = @LocationType)
						UPDATE [LOCATION] SET [LocationType] = @LocationType,[Latitude0] = @Latitude0,[Longitude0] = @Longitude0,[Latitude1] = @Latitude1,[Longitude1] = @Longitude1, [Latitude2] = @Latitude2,[Longitude2] = @Longitude2
                        WHERE Token = @Token AND [LocationType] = @LocationType
					ELSE
						INSERT INTO [dbo].[Location]([Token],[LocationType],[Latitude0],[Longitude0],[Latitude1],[Longitude1],[Latitude2],[Longitude2]) 
                    VALUES (@Token, @LocationType, @Latitude0, @Longitude0, @Latitude1, @Longitude1, @Latitude2, @Longitude2)",
                new
                {
                    Token = token,
                    LocationType = keyword,
                    Latitude0 = point0[0],
                    Longitude0 = point0[1],
                    Latitude1 = point1[0],
                    Longitude1 = point1[1],
                    Latitude2 = point2[0],
                    Longitude2 = point2[1]
                });
        }

        public Sync GetSync(string token)
        {
            var schedule = GetSchedule(token);

            var path = GetPath(token);

            Sync sync = new Sync();

            if (schedule != null || path != null)
                sync = new Sync(token, schedule, path);

            return sync;
        }

        public bool SetSync(string token, Schedule schedule, Path path)
        {
            SetSchedule(schedule);

            SetPath(path.Token, path.S, path.E, path.Leftb, path.Rightb, path.Centerl, path.So, path.Eo);

            return true;
        }
    }
}