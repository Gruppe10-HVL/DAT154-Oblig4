using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;

using System.Runtime.CompilerServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FrontDesk.Models;
using Newtonsoft.Json;

namespace FrontDesk.Operations
{
    public class ApiClient
    {
        const string Customers = "/api/v1/customer";
        const string Rooms = "/api/v1/Room";
        const string Bookings = "/api/v1/Booking";

        private string baseUrl;
        private HttpClient client;

        public ApiClient()
        {
            baseUrl = "http://localhost:3000/api";
        }

        public async Task<Customer> GetCustomers()
        {
            HttpResponseMessage response = await client.GetAsync(baseUrl + Customers);
            
            return null;
        }
    }
}
