using CLassifiedsUIPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CLassifiedsUIPortal.Provider
{
  public  interface IAuthProvider
    {
        public Task<HttpResponseMessage> Login(User user); //it includes the status code and data
    }
}
