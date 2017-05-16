using System;
using System.Collections.Specialized;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

using Newtonsoft.Json;

namespace AmoCrm.Client
{


    public class AmoCrmClient
    {
        private readonly HttpClient _client = new HttpClient();

        public AmoCrmClient()
        {
            this._client.BaseAddress = new Uri("https://shinecity.amocrm.ru/private/api/");
            this._client.DefaultRequestHeaders.Accept.Clear();
            this._client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<AmoHttpResponse<AmoResponse>> Auth(string login, string hash)
        {
            var response = await this._client.PostAsJsonAsync(
                               "auth.php?type=json",
                               new AuthRequestModel { Hash = hash, UserLogin = login });
            response.EnsureSuccessStatusCode();
            var res = await response.Content.ReadAsAsync<AmoHttpResponse<AmoResponse>>();
            return res;
        }


        public async Task<AmoHttpResponse<SetLeadsResponse>> GetLeadByEmail(string email)
        {
            email = HttpUtility.UrlEncode(email);
            var response = await this._client.GetAsync($"v2/json/leads/list?query={email}");
            response.EnsureSuccessStatusCode();
            var r = await response.Content.ReadAsAsync<AmoHttpResponse<SetLeadsResponse>>();
           return r;
        }

        //public static async Task<Uri> CreateProductAsync(Product product)
        //{
        //    var response = await Client.PostAsJsonAsync("api/products", product);
        //    response.EnsureSuccessStatusCode();

        //    // return URI of the created resource.
        //    return response.Headers.Location;
        //}

        //static async Task<Product> GetProductAsync(string path)
        //{
        //    Product product = null;
        //    var response = await Client.GetAsync(path);
        //    if (response.IsSuccessStatusCode)
        //    {
        //        product = await response.Content.ReadAsAsync<Product>();
        //    }
        //    return product;
        //}

        //static async Task<Product> UpdateProductAsync(Product product)
        //{
        //    var response = await Client.PutAsJsonAsync($"api/products/{product.Id}", product);
        //    response.EnsureSuccessStatusCode();

        //    // Deserialize the updated product from the response body.
        //    product = await response.Content.ReadAsAsync<Product>();
        //    return product;
        //}

        //static async Task<HttpStatusCode> DeleteProductAsync(string id)
        //{
        //    var response = await Client.DeleteAsync($"api/products/{id}");
        //    return response.StatusCode;
        //}

        //static void Main()
        //{
        //    RunAsync().Wait();
        //}

        public async Task<HttpStatusCode> UpdateVisitorIdForUserLeads(string userEmail, string visitorId)
        {
            var leadsResponse = await GetLeadByEmail(userEmail);
            if (leadsResponse?.Response?.Leads != null)
            {
                var date = DateTime.Now;
                foreach (var leadViewModel in leadsResponse.Response.Leads)
                {
                    leadViewModel.Id = null;
                    leadViewModel.VisitorId = visitorId;
                    leadViewModel.LastModifiedTime = UnixTicks(date).ToString(CultureInfo.InvariantCulture);
                    leadViewModel.Name = "Test";

                }
                var setModel = new SetLeadsViewModel { Update = leadsResponse?.Response?.Leads };
                var setLeadsRequest = new SetLeadsRequest { Leads = setModel };
                var request = new AmoRequest<SetLeadsRequest> { Request = setLeadsRequest };

                var response = await _client.PostAsJsonAsync("v2/json/leads/set", new {request = new {leads = setModel}});
                var d = await response.Content.ReadAsAsync<dynamic>();
                return response.StatusCode;
            }
            else
            {
                throw new Exception();
            }
        }


        public static double UnixTicks(DateTime dt)
        {
            DateTime d1 = new DateTime(1970, 1, 1);
            DateTime d2 = dt.ToUniversalTime();
            TimeSpan ts = new TimeSpan(d2.Ticks - d1.Ticks);
            return ts.TotalMilliseconds;
        }
    }
}
