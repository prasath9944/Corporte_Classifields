using CLassifiedsUIPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CLassifiedsUIPortal.Provider
{
    public interface IOfferProvider
    {

        Task<HttpResponseMessage> GetOfferById(int id, string token);
        Task<HttpResponseMessage> GetOfferByCategory(string category, string token);
        Task<HttpResponseMessage> GetOfferByTopThreeLikes(string category, string token);
        Task<HttpResponseMessage> GetOfferByOpenedDate(string openedDate, string token);

        Task<HttpResponseMessage> PostOffer(OfferData offer, string token);
        Task<HttpResponseMessage> EngageOffer(OfferData offerDetails, string token);
        Task<HttpResponseMessage> GetOffersList(string token);

        Task<HttpResponseMessage> LikeOffer(OfferData offerDetails, string token);
        Task<HttpResponseMessage> EditOffer(OfferData offer,string token);
    }
}
    
