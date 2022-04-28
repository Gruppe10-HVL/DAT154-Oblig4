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
    public sealed partial class BookingsPage : Page
    {

        List<BookingDto> Bookings = new List<BookingDto>();
        List<BookingDto> BookingsQuery = new List<BookingDto>();
        public BookingsPage()
        {
            this.InitializeComponent();
            GetBookings();
        }

        async void GetBookings()
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            HttpClient httpClient = new HttpClient(clientHandler);
            httpClient.Timeout = TimeSpan.FromSeconds(30);

            var api = "https://localhost:5001/api/v1/booking";
            var response = await httpClient.GetStringAsync(api);
            var bookings = JsonConvert.DeserializeObject<List<BookingDto>>(response);
            Bookings = bookings;
            BookingsMenu.ItemsSource = Bookings;
        }

        private void SearchCustomerButton_QuerySubmitted(SearchBox sender, SearchBoxQuerySubmittedEventArgs args)
        {
            var query = args.QueryText;
            var bookings = Bookings.Where(x => x.CustomerName.Contains(query)).ToList();
            Bookings = bookings;
            BookingsMenu.ItemsSource = BookingsQuery;
        }

        private void SearchCustomerButton_QueryChanged(SearchBox sender, SearchBoxQueryChangedEventArgs args)
        {

            var query = args.QueryText;

            if (query == String.Empty)
            {
                BookingsMenu.ItemsSource = Bookings;
            } else
            {
                var bookings = Bookings.Where(x => x.CustomerName.Contains(query)).ToList();
                Bookings = bookings;
                BookingsMenu.ItemsSource = BookingsQuery;
            }

        }
    }
}
