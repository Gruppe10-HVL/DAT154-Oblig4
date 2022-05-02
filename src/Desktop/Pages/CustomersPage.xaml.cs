using Desktop.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Desktop.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CustomersPage : Page
    {
        const string Url = "https://localhost:5001/api/v1/customer";
        public List<CustomerEntity> Customers = new List<CustomerEntity>();
        public CustomersPage()
        {
            this.InitializeComponent();
            GetCustomers();
        }

        async void GetCustomers()
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            HttpClient client = new HttpClient(clientHandler);
            client.Timeout = TimeSpan.FromSeconds(30);

            var api = Url;
            var response = await client.GetStringAsync(api);
            var customers = JsonConvert.DeserializeObject<List<CustomerEntity>>(response);
            Customers = customers;
            CustomersMenu.ItemsSource = Customers;
        }
    }
}
