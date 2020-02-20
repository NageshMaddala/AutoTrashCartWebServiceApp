using System.Collections.Generic;
using AutoTrashCartWebServiceApp.Models;

namespace AutoTrashCartWebServiceApp.Interfaces
{
    public interface IAutoTrashCartRepository
    {
        bool SetSchedule(Schedule schedule);

        bool SetPath(string pathId, string s, string e, string[] leftb, string[] rightb, string[] centerl, string so, string eo);

        Schedule GetSchedule(string scheduleId);

        Path GetPath(string pathId);

        bool SetSync(string token, Schedule schedule, Path path);

        Sync GetSync(string token);

        IEnumerable<AutoTrashCartApiLog> GetApplicationLogs();

        IEnumerable<AutoTrashCartApiError> GetApplicationErrors();
    }
}