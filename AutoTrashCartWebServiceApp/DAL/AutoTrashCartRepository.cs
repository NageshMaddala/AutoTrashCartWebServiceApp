using System;
using System.Collections.Generic;
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


        public IEnumerable<AutoTrashCartApiLog> GetApplicationLogs()
        {
            return _db.Query<AutoTrashCartApiLog>("Select * from AutoTrashCartAPILog where Host not in ('::1', '72.180.106.147','23.22.16.221') order by 5 desc");
        }

        public IEnumerable<AutoTrashCartApiError> GetApplicationErrors()
        {
            return _db.Query<AutoTrashCartApiError>("Select * from AutoTrashCartAPIError order by 4 desc");
        }

        public Schedule GetSchedule(string scheduleId)
        {
            return _db.Query<Schedule>("Select * from [Schedule] where ScheduleId=" + scheduleId).FirstOrDefault();
        }

        public bool SetSchedule(Schedule schedule)
        {
            int rowsAffected = _db.Execute(
                @"IF EXISTS (SELECT TOP 1 * FROM [DBO].[SCHEDULE] WHERE ScheduleId = @ScheduleId)
                        UPDATE [DBO].[SCHEDULE] SET [Day] = @Day, [Pickup] = @Pickup, [Holidays] = @Holidays WHERE ScheduleId = @ScheduleId
                     ELSE
                        INSERT INTO [DBO].[SCHEDULE] ([ScheduleId],[Day],[Pickup],[Holidays]) VALUES (@ScheduleId, @Day, @Pickup, @Holidays)",
                new
                {
                    ScheduleId = schedule.ScheduleId,
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

        public Path GetPath(string pathId)
        {
            var location = _db.Query<Location>("SELECT * FROM [LOCATION] WHERE LocationId =" + pathId).ToArray<Location>();

            var orientation = _db.Query<Orientation>("SELECT * FROM [ORIENTATION] WHERE OrientationId =" + pathId).ToArray<Orientation>();

            var path = new Path(pathId, location, orientation);

            return path;
        }

        public bool SetPath(string pathId, string s, string e, string[] leftb, string[] rightb, string[] centerl, string so, string eo)
        {
            InsertStartingAndEndingPoints(pathId, s, "s");
            InsertStartingAndEndingPoints(pathId, e, "e");
            InsertLocation(pathId, leftb, "leftb");
            InsertLocation(pathId, rightb, "rightb");
            InsertLocation(pathId, centerl, "centerl");
            InsertOrientation(pathId, "so", so);
            InsertOrientation(pathId, "eo", eo);
            return true;
        }

        public void InsertOrientation(string pathId, string keyword, string orientation)
        {
            string[] points = orientation.Split(',').ToArray();

            if (points.Length > 0)
            {
                int insertOrientation = _db.Execute(
                    @"IF EXISTS (SELECT TOP 1 * FROM [DBO].[ORIENTATION] WHERE OrientationId = @OrientationId AND [OrientationType] = @OrientationType)
						 UPDATE [DBO].[ORIENTATION] SET [OrientationType] = @OrientationType, [X] = @X, [Y] = @Y, [Z] = @Z 
                         WHERE OrientationId = @OrientationId AND [OrientationType] = @OrientationType
					ELSE
						 INSERT INTO [DBO].[ORIENTATION] ([OrientationId],[OrientationType],[X],[Y],[Z]) VALUES (@OrientationId, @OrientationType, @X, @Y, @Z)",
                    new
                    {
                        OrientationId = pathId,
                        OrientationType = keyword,
                        X = points[0],
                        Y = points[1],
                        Z = points[2]
                    });
            }
        }

        public void InsertStartingAndEndingPoints(string pathId, string point, string keyword)
        {
            string[] points = point.Split(',').ToArray();

            if (points.Length > 0)
            {
                int insertStartingPoints = _db.Execute(
                    @"IF EXISTS (SELECT TOP 1 * FROM [DBO].[LOCATION] WHERE LocationId = @LocationId AND [LocationType] = @LocationType)
						UPDATE [dbo].[Location] SET [LocationType] = @LocationType,[Latitude0] = @Latitude0,[Longitude0] = @Longitude0
                        WHERE LocationId = @LocationId AND [LocationType] = @LocationType
					ELSE
						INSERT INTO [dbo].[Location]([LocationId],[LocationType],[Latitude0],[Longitude0]) VALUES (@LocationId, @LocationType, @Latitude0, @Longitude0)",
                    new
                    {
                        LocationId = pathId,
                        LocationType = keyword,
                        Latitude0 = points[0],
                        Longitude0 = points[1]
                    });
            }
        }

        public void InsertLocation(string pathId, string[] points, string keyword)
        {
            if (points == null)
                return;

            if (points.Length < 3)
                return;

            string[] point0 = points[0].Split(',').ToArray();
            string[] point1 = points[1].Split(',').ToArray();
            string[] point2 = points[2].Split(',').ToArray();

            int insertStartingPoints = _db.Execute(
                @"IF EXISTS (SELECT TOP 1 * FROM [DBO].[LOCATION] WHERE LocationId = @LocationId AND [LocationType] = @LocationType)
						UPDATE [LOCATION] SET [LocationType] = @LocationType,[Latitude0] = @Latitude0,[Longitude0] = @Longitude0,[Latitude1] = @Latitude1,[Longitude1] = @Longitude1, [Latitude2] = @Latitude2,[Longitude2] = @Longitude2
                        WHERE LocationId = @LocationId AND [LocationType] = @LocationType
					ELSE
						INSERT INTO [dbo].[Location]([LocationId],[LocationType],[Latitude0],[Longitude0],[Latitude1],[Longitude1],[Latitude2],[Longitude2]) 
                    VALUES (@LocationId, @LocationType, @Latitude0, @Longitude0, @Latitude1, @Longitude1, @Latitude2, @Longitude2)",
                new
                {
                    LocationId = pathId,
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

            SetPath(path.PathId, path.S, path.E, path.Leftb, path.Rightb, path.Centerl, path.So, path.Eo);

            return true;
        }
    }
}