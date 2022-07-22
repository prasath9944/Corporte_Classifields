using CLassifiedsUIPortal.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CLassifiedsUIPortal.Provider
{
    public class AuthProvider:IAuthProvider
    {
        public async Task<HttpResponseMessage> Login(User user)
        {
            using (var httpClient = new HttpClient())
            {
                StringContent content1 = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                var response1 = await httpClient.PostAsync("https://authorizationsvc2.azurewebsites.net/api/Authenticate", content1);
                return response1;
            }

        }
    }
}
