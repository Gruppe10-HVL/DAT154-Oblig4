using Desktop.Entities;
using Desktop.Enums;
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

        private async void CheckinButton_Click(object sender, RoutedEventArgs e)
        {
            BookingDto booking = (BookingDto)BookingsMenu.SelectedItem;

            HttpClientHandler clientHandler = new HttpClientHandler();
            HttpClient httpClient = new HttpClient(clientHandler);
            httpClient.Timeout = TimeSpan.FromSeconds(30);

            var api = "https://localhost:5001/api/v1/booking/checkin";
            var request = new HttpRequestMessage(new HttpMethod("PATCH"), api + "/?id=" + booking.Id);
            var response = await httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
                GetBookings();
        }

        private async void CheckoutButton_Click(object sender, RoutedEventArgs e)
        {
            BookingDto booking = (BookingDto)BookingsMenu.SelectedItem;

            if (!booking.Status.Equals(BookingStatus.CheckedIn))
            {
                Flyout flyout = new Flyout
                {
                    Content = new TextBlock
                    {
                        Text = "Booking is not checked in."
                    }
                };

                flyout.ShowAt((FrameworkElement)sender);

                return;
            }

            HttpClientHandler clientHandler = new HttpClientHandler();
            HttpClient httpClient = new HttpClient(clientHandler);
            httpClient.Timeout = TimeSpan.FromSeconds(30);

            var api = "https://localhost:5001/api/v1/booking/checkout";
            var request = new HttpRequestMessage(new HttpMethod("PATCH"), api + "/?id=" + booking.Id);
            var response = await httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                // TODO: Send ServiceTask
                ContentDialog createServiceTaskDialog = new ContentDialog
                {
                    Title = "Service Task",
                    Content = "Start cleaning task for this room?",
                    PrimaryButtonText = "Yes",
                    CloseButtonText = "No"
                };

                ContentDialogResult result = await createServiceTaskDialog.ShowAsync();

                if (result == ContentDialogResult.Primary)
                {
                    Console.WriteLine("Here");
                } else
                {

                }

                GetBookings();
            }
                
        }
    }
}
