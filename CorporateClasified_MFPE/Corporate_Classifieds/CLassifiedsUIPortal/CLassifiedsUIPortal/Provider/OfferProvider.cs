using CLassifiedsUIPortal.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace CLassifiedsUIPortal.Provider
{


    public class OfferHelper
    {
        public HttpClient Initial()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://offermicroservice3.azurewebsites.net");
            return client;
        }
    }
    public class OfferProvider : IOfferProvider{
        OfferHelper _api = new OfferHelper();
        public async Task<HttpResponseMessage> GetOfferById(int id, string token)
        {
               
                using (HttpClient client = _api.Initial())
                {

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await client.GetAsync($"api/offer/GetOfferById/{id}" );
                
                return response;
            
                }
        }
        public async Task<HttpResponseMessage> GetOfferByCategory(string category, string token)
        {

            using (HttpClient client = _api.Initial())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await client.GetAsync($"api/offer/GetOfferByCategory/{category}");

                return response;
            }
        }

        public async Task<HttpResponseMessage> GetOfferByTopThreeLikes(string category, string token)
        {
            using (HttpClient client = _api.Initial())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await client.GetAsync($"api/offer/GetOfferByTopThreeLikes/{category}");

                return response;
            }
        }

        public async Task<HttpResponseMessage> GetOfferByOpenedDate(string openedDate, string token)
        {
            using (HttpClient client = _api.Initial())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await client.GetAsync($"api/offer/GetOfferByOpenedDate/{openedDate}");

                return response;
            }
        }

        public async Task<HttpResponseMessage> PostOffer(OfferData offer, string token)
        {
            using (HttpClient client = _api.Initial())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await client.PostAsJsonAsync<OfferData>("api/offer/PostOffer",offer);

                return response;
            }
        }

        public async Task<HttpResponseMessage> EngageOffer(OfferData offerDetails, string token)
        {
            using (HttpClient client = _api.Initial())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await client.PostAsJsonAsync<OfferData>("api/offer/EngageOffer",offerDetails);

                return response;
            }
        }

        public async Task<HttpResponseMessage> GetOffersList(string token)
        {
            using (HttpClient client = _api.Initial())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await client.GetAsync($"api/offer/GetOffersList");

                return response;
            }
        }

        public async Task<HttpResponseMessage> LikeOffer(OfferData offerDetails, string token)
        {
            OfferHelper _api = new OfferHelper();

            using (HttpClient client = _api.Initial())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await client.PostAsJsonAsync<OfferData>("api/Offer/LikeOffer", offerDetails);

                return response;
            }
        
    }

        public async Task<HttpResponseMessage> EditOffer(OfferData offer, string token)
        {
            using (HttpClient client = _api.Initial())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await client.PostAsJsonAsync<OfferData>("api/offer/EditOffer", offer);

                return response;
            }
        }
    }
}

