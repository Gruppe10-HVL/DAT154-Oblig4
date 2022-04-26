using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

using DAT154Oblig4.Domain.Entities;

namespace FrontDesk.Data
{
    public class CustomersClient
    {
        private string BaseUrl = "localhost:3000";
        private string Url = "/api/v1/customer";

        public CustomersClient() { }

        public async Task<List<Customer>> GetCustomers()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(Url);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    List<Customer> customers = JsonConvert.DeserializeObject<List<Customer>>(json);
                    return customers;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
