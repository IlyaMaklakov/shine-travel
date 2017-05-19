#region Usings

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;

using AmoCrm.Client.Accounts;
using AmoCrm.Client.Auth;
using AmoCrm.Client.Contacts;
using AmoCrm.Client.Crud;
using AmoCrm.Client.Http;
using AmoCrm.Client.Leads;
using AmoCrm.Client.Pipelines;

using Shine.Framework.Threading;

#endregion

namespace AmoCrm.Client
{
    public class AmoCrmClient
    {
        private readonly HttpClient _client = new HttpClient();

        public Dictionary<string, List<string>> PipelinePreStatusesForVideos { get; set; }
        public Dictionary<string, string> PipelineVideoStatuses { get; set; }

        public AmoCrmClient()
        {
            this._client.BaseAddress = new Uri("https://shinecity.amocrm.ru/private/api/");
            this._client.DefaultRequestHeaders.Accept.Clear();
            this._client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            PipelineVideoStatuses =
                new Dictionary<string, string>
                    {
                        { "storytelling", "14830110" },
                        { "how-to-travel", "14830113" },
                        { "travel-itinerary", "14830116" },
                        { "60-days", "14809086" }
                    };

            this.PipelinePreStatusesForVideos =
                new Dictionary<string, List<string>>
                    {
                        {
                            "storytelling", new List<string> { "14806291", "14807485" }
                        },
                        { "how-to-travel", new List<string> { "14807122" } },
                        { "travel-itinerary", new List<string> { "14807128" } },
                        { "60-days", new List<string> { "14807134" } }
                    };
        }

        private static double UnixTicks(DateTime dt)
        {
            var d1 = new DateTime(1970, 1, 1);
            var d2 = dt.ToUniversalTime();
            var ts = new TimeSpan(d2.Ticks - d1.Ticks);
            return ts.TotalMilliseconds;
        }
        public HttpResponseWrapper<ContactsResponse> GetContactByEmail(string email)
        {
            var contactResponse = AsyncHelpers.RunSync(() => GetContactByEmailAsync(email));
            return contactResponse;
        }

        public async Task<HttpResponseWrapper<ContactsResponse>> GetContactByEmailAsync(string email)
        {
            email = HttpUtility.UrlEncode(email);
            var response = await this._client.GetAsync($"v2/json/contacts/list?query={email}");
            var contactsResponse = await response.Content.ReadAsAsync<HttpResponseWrapper<ContactsResponse>>();
            return contactsResponse;
        }

        public async Task<HttpStatusCode> AddLeadToContactAsync(string email, string visitorId, string leadName)
        {
            var contactResponse = await this.GetContactByEmailAsync(email);
            var addLeadResult = await AddLeadAsync(leadName, visitorId);
            var contact = contactResponse.Response.Contacts.FirstOrDefault();
            var addedLead = addLeadResult.Response.Leads.Add.FirstOrDefault();
            if (contact == null)
            {
                throw new Exception("Contact is null");
            }
            var leadsList = contact.LeadsIds.ToList();
            if (addedLead != null)
            {
                leadsList.Add(addedLead.Id);
            }

            contact.LeadsIds = leadsList.ToArray();

            var updateContactResponse = await this.UpdateContactAsync(contact);
            return updateContactResponse;
        }


        public async Task<HttpStatusCode> UpdateContactAsync(Contact contact)
        {
            contact.LastModifiedTime = UnixTicks(DateTime.Now).ToString(CultureInfo.InvariantCulture);
            contact.Id = null;

            var setContact = new AddOrUpdateWrapper<Contact>() { Update = new[] { contact } };


            var response = await this._client.PostAsJsonAsync(
                               "v2/json/contacts/set",
                               new { request = new { contacts = setContact } });

            return response.StatusCode;
        }

        public async Task<HttpResponseWrapper<AuthResponse>> AuthAsync(string login, string hash)
        {
            var response = await this._client.PostAsJsonAsync(
                               "auth.php?type=json",
                               new AuthRequest { Hash = hash, UserLogin = login });
            response.EnsureSuccessStatusCode();
            var res = await response.Content.ReadAsAsync<HttpResponseWrapper<AuthResponse>>();
            return res;
        }


        public HttpResponseWrapper<AuthResponse> Auth(string login, string hash)
        {
            var autResult = AsyncHelpers.RunSync(() => AuthAsync(login, hash));
            return autResult;
        }

        private async Task<HttpResponseWrapper<SetLeadsResponse>> GetLeadByEmail(string email)
        {
            email = HttpUtility.UrlEncode(email);
            var response = await this._client.GetAsync($"v2/json/leads/list?query={email}");
            response.EnsureSuccessStatusCode();
            var r = await response.Content.ReadAsAsync<HttpResponseWrapper<SetLeadsResponse>>();
            return r;
        }

        public async Task<HttpResponseWrapper<SetLeadsRequest>> AddLeadAsync(string name, string visitorId)
        {
            var newLead = new Lead { Name = name, VisitorId = visitorId };

            var setModel = new AddOrUpdateWrapper<Lead> { Add = new[] { newLead } };
            var setLeadsRequest = new SetLeadsRequest { Leads = setModel };
            var request = new HttpRequestWrapper<SetLeadsRequest> { Request = setLeadsRequest };

            var response = await this._client.PostAsJsonAsync(
                               "v2/json/leads/set",
                               new { request = new { leads = setModel } });
            var d = await response.Content.ReadAsAsync<HttpResponseWrapper<SetLeadsRequest>>();
            return d;
        }

        public async Task<dynamic> GetpipeLinesAsync()
        {
            var response = await this._client.GetAsync("v2/json/pipelines/list");
            response.EnsureSuccessStatusCode();
            var currentAccountResponse = await response.Content.ReadAsAsync<dynamic>();
            return currentAccountResponse;
        }

        public PipeLinesResponse GetpipeLines()
        {
            var result = AsyncHelpers.RunSync(this.GetpipeLinesAsync);
            return result;
        }

        public async Task<HttpResponseWrapper<AccountResponse>> GetCurrentAccountAsync()
        {
            var response = await this._client.GetAsync("v2/json/accounts/current");
            response.EnsureSuccessStatusCode();

            var currentAccountResponse = await response.Content.ReadAsAsync<HttpResponseWrapper<AccountResponse>>();
            return currentAccountResponse;
        }

        public HttpResponseWrapper<AccountResponse> GetCurrentAccount()
        {
            var result = AsyncHelpers.RunSync(this.GetCurrentAccountAsync);
            return result;
        }

        public dynamic AddLead(string name, string visitorid)
        {
            var addResult = AsyncHelpers.RunSync(() => this.AddLeadAsync(name, visitorid));
            return addResult;
        }

        public async Task<HttpStatusCode> UpdateVisitorIdForUserLead(string userEmail, string visitorId, string videoFriendlyUrl)
        {
            if (!this.PipelinePreStatusesForVideos.ContainsKey(videoFriendlyUrl))
            {
                throw new Exception();
            }
            var preStatusesForCurrent= this.PipelinePreStatusesForVideos[videoFriendlyUrl];

            var leadsResponse = await this.GetLeadByEmail(userEmail);
            if (leadsResponse?.Response?.Leads != null)
            {
                
                var leadsToUpdate = new List<Lead>();

                foreach (var lead in leadsResponse.Response.Leads)
                {
                    var newLastModifiedData = Convert.ToInt64(lead.LastModifiedTime) + 100;
                    if (preStatusesForCurrent.Contains(lead.StatusId))
                    {
                        lead.VisitorId = visitorId;
                        lead.StatusId = PipelineVideoStatuses[videoFriendlyUrl];
                        lead.LastModifiedTime = newLastModifiedData.ToString();
                        leadsToUpdate.Add(lead);
                    }
                }
                if (!leadsToUpdate.Any())
                {
                    throw new Exception();
                }
                var setModel = new AddOrUpdateWrapper<Lead> { Update = leadsToUpdate.ToArray() };
                var response = await this._client.PostAsJsonAsync(
                                   "v2/json/leads/set",
                                   new { request = new { leads = setModel } });
                var updateResult = await response.Content.ReadAsAsync<dynamic>();
                return response.StatusCode;
            }
            throw new Exception();
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
    }
}