﻿using Desktop.Entities;
using Desktop.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
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
        const string Url = "https://localhost:5001/api/v1/booking";
        List<CustomerEntity> Customers = new List<CustomerEntity>();
        List<string> CustomerNames = new List<string>();
        List<int> RoomIds = new List<int>();
        List<BookingStatus> Statuses = new List<BookingStatus>();
        List<BookingEntity> Bookings = new List<BookingEntity>();
        List<BookingEntity> BookingsQuery = new List<BookingEntity>();
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

            var api = Url;
            var response = await httpClient.GetStringAsync(api);
            var bookings = JsonConvert.DeserializeObject<List<BookingEntity>>(response);
            Bookings = bookings;
            Customers = Bookings.GroupBy(x => x.Customer.Name).Select(x => x.First()).Select(x => x.Customer).ToList();
            CustomerNames = Customers.OrderBy(x => x.Name).Select(x => x.Name).ToList();
            RoomIds = Bookings.OrderBy(x => x.RoomId).Select(x => x.RoomId).Distinct().ToList();
            Statuses = Bookings.Select(x => x.Status).Distinct().ToList();

            BookingsMenu.ItemsSource = Bookings;
            CustomerCombo.ItemsSource = CustomerNames;
            RoomCombo.ItemsSource = RoomIds;
            BookingsStatusCombo.ItemsSource = Statuses;
        }

        private void SearchCustomerButton_QuerySubmitted(SearchBox sender, SearchBoxQuerySubmittedEventArgs args)
        {
            var query = args.QueryText;
            var bookings = Bookings.Where(x => x.Customer.Name.Contains(query)).ToList();
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
                var bookings = Bookings.Where(x => x.Customer.Name.Contains(query)).ToList();
                Bookings = bookings;
                BookingsMenu.ItemsSource = BookingsQuery;
            }

        }

        private async void CheckinButton_Click(object sender, RoutedEventArgs e)
        {
            BookingEntity booking = (BookingEntity)BookingsMenu.SelectedItem;

            if (booking.BookingStart > DateTime.Now)
            {
                ContentDialog invalidCheckinDialog = new ContentDialog
                {
                    Title = "Invalid checkin",
                    Content = "Booking has not started yet.",
                    CloseButtonText = "Ok"
                };

                await invalidCheckinDialog.ShowAsync();

                return;
            }

            HttpClientHandler clientHandler = new HttpClientHandler();
            HttpClient httpClient = new HttpClient(clientHandler);
            httpClient.Timeout = TimeSpan.FromSeconds(30);

            var api = Url + "/checkin/?id=";
            var request = new HttpRequestMessage(new HttpMethod("PATCH"), api + booking.Id);
            var response = await httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
                GetBookings();
        }

        private async void CheckoutButton_Click(object sender, RoutedEventArgs e)
        {
            BookingEntity booking = (BookingEntity)BookingsMenu.SelectedItem;

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

            var api = Url + "/checkout/?id=";
            var request = new HttpRequestMessage(new HttpMethod("PATCH"), api + booking.Id);
            var response = await httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
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
                    ServiceTask serviceTask = new ServiceTask
                    {
                        roomId = booking.RoomId,
                        description = "Cleaning",
                        taskType = ServiceTaskType.Cleaning,
                        taskStatus = ServiceTaskStatus.New,
                        priority = ServiceTaskPriority.MEDIUM,
                        notes = "",
                    };

                    using (var client = new HttpClient())
                    {
                        api = "https://localhost:5001/api/v1/task";
                        string json = JsonConvert.SerializeObject(serviceTask, Formatting.Indented);
                        HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                        response = await client.PostAsync(api, content);

                        if (response.IsSuccessStatusCode)
                        {
                            ContentDialog serviceTaskGeneratedDialog = new ContentDialog
                            {
                                Title = "Service task successfully generated",
                                Content = String.Format("Service task generated for room {0}", serviceTask.roomId),
                                CloseButtonText = "Ok"
                            };
                            await serviceTaskGeneratedDialog.ShowAsync();
                        }
                    }
                } else
                {

                }
                GetBookings();
            }
        }

        private async void DeleteBookingButton_Click(object sender, RoutedEventArgs e)
        {
            BookingEntity booking = (BookingEntity)BookingsMenu.SelectedItem;
            if (booking == null)
            {
                ContentDialog invalidDeleteOptionDialog = new ContentDialog
                {
                    Title = "No booking selected",
                    Content = "Select a booking to delete.",
                    CloseButtonText = "Ok"
                };

                await invalidDeleteOptionDialog.ShowAsync();

                return;
            }

            HttpClientHandler clientHandler = new HttpClientHandler();
            HttpClient httpClient = new HttpClient(clientHandler);
            httpClient.Timeout = TimeSpan.FromSeconds(30);

            var api = Url + "/?id=";
            var request = new HttpRequestMessage(new HttpMethod("DELETE"), api + booking.Id);
            var response = await httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                ContentDialog deleteSuccessDialog = new ContentDialog
                {
                    Title = "Booking deleted successfully",
                    Content = String.Format("Booking {0} deleted successfully", booking.Id),
                    CloseButtonText = "Ok"
                };

                await deleteSuccessDialog.ShowAsync();

                GetBookings();
            }
        }
        private async void CancelBookingButton_Click(object sender, RoutedEventArgs e)
        {
            BookingEntity booking = (BookingEntity)BookingsMenu.SelectedItem;
            if (booking == null)
            {
                ContentDialog invalidCancelOptionDialog = new ContentDialog
                {
                    Title = "No booking selected",
                    Content = "Select a booking to cancel.",
                    CloseButtonText = "Ok"
                };

                await invalidCancelOptionDialog.ShowAsync();

                return;
            }

            HttpClientHandler clientHandler = new HttpClientHandler();
            HttpClient httpClient = new HttpClient(clientHandler);
            httpClient.Timeout = TimeSpan.FromSeconds(30);

            var api = Url + "/cancel/?id=";
            var request = new HttpRequestMessage(new HttpMethod("PATCH"), api + booking.Id);
            var response = await httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                ContentDialog cancelSuccessDialog = new ContentDialog
                {
                    Title = "Cancellation",
                    Content = String.Format("Booking {0} cancelled", booking.Id),
                    CloseButtonText = "Ok"
                };

                await cancelSuccessDialog.ShowAsync();
            }
            GetBookings();
        }

        private void CreateBookingButton_Click(object sender, RoutedEventArgs e)
        {
            BookingParameters parameters = new BookingParameters();
            //parameters.Customers = Bookings.ForEach(x => x.Customer)

            parameters.Customers = Customers;
            parameters.RoomIds = RoomIds;

            Frame.Navigate(typeof(BookingPage), parameters);
        }

        private void RoomCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox combo = (ComboBox)sender;

            if (combo.SelectedItem == null) return;

            var id = combo.SelectedItem;
            List<BookingEntity> bookings = new List<BookingEntity>();

            bookings = Bookings.Where(x => x.RoomId == (int)id).ToList();
            BookingsMenu.ItemsSource = bookings;

        }

        private void RoomComboClearButton_Click(object sender, RoutedEventArgs e)
        {
            RoomCombo.SelectedItem = null;
            BookingsMenu.ItemsSource = Bookings;
        }

        private void CustomerCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox combo = (ComboBox)sender;

            if (combo.SelectedItem == null) return;

            var id = combo.SelectedItem;
            List<BookingEntity> bookings = new List<BookingEntity>();

            bookings = Bookings.Where(x => x.Customer.Name == (string)id).ToList();
            BookingsMenu.ItemsSource = bookings;
        }

        private void CustomerComboClearButton_Click(object sender, RoutedEventArgs e)
        {
            CustomerCombo.SelectedItem = null;
            BookingsMenu.ItemsSource = Bookings;
        }

        private void BookingsStatusCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox combo = (ComboBox)sender;

            if (combo.SelectedItem == null)
            {
                return;
            }

            var status = combo.SelectedItem;
            List<BookingEntity> bookings = new List<BookingEntity>();

            bookings = Bookings.Where(x => x.Status == (BookingStatus)status).ToList();
            BookingsMenu.ItemsSource = bookings;
        }

        private void BookingsStatusComboClearButton_Click(object sender, RoutedEventArgs e)
        {
            BookingsStatusCombo.SelectedItem = null;
            BookingsMenu.ItemsSource = Bookings;
        }


    }

    public class NullItemToVisibility : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return value == null ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value != null ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}
