using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CLassifiedsUIPortal.Provider
{
    public interface IEmployeeProvider
    {
        Task<HttpResponseMessage> ViewEmployeeOffers(int employeeId, string token);
        Task<HttpResponseMessage> ViewMostLikedOffers(int employeeId, string token);

        //Task<HttpResponseMessage>GetPointsByEmployeeId(int employeeId, string token);
        Task<HttpResponseMessage> GetEmployeeList(string token);

    }
}
