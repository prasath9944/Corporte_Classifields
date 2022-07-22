using CLassifiedsUIPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CLassifiedsUIPortal.Provider
{
    public interface IPointsProvider
    {
        Task<HttpResponseMessage> GetPointsByEmployeeId(int employeeId, string token);
        Task<HttpResponseMessage> RefreshPointsByEmployee(int employeeId, string token);
    }
}
