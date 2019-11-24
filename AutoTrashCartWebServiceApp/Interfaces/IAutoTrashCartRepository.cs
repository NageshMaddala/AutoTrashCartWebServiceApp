using AutoTrashCartWebServiceApp.Models;

namespace AutoTrashCartWebServiceApp.Interfaces
{
    public interface IAutoTrashCartRepository
    {
        bool SetSchedule(Schedule schedule);

        bool SetPath(string token, string s, string e, string[] leftb, string[] rightb, string[] centerl, string so, string eo);

        Schedule GetSchedule(string token);

        Path GetPath(string token);

        bool SetSync(string token, Schedule schedule, Path path);

        Sync GetSync(string token);
    }
}