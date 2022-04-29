using Desktop.Entities;
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
    public sealed partial class BookingPage : Page
    {

        const string Url = "https://localhost:5001/api/v1/booking";
        const string RoomUrl = "https://localhost:5001/api/v1/room";
        private List<CustomerEntity> Customers = new List<CustomerEntity>();
        private List<RoomEntity> Rooms = new List<RoomEntity>();
        private List<RoomEntity> RoomsQuery = new List<RoomEntity>();
        private List<int> RoomIds = new List<int>();

        public BookingPage()
        {
            this.InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            BookingStartDatePicker.Date = DateTime.Now;
            BookingEndDatePicker.Date = DateTime.Now.AddDays(1);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                BookingParameters parameters = e.Parameter as BookingParameters;
                Customers = parameters.Customers;
                RoomIds = parameters.RoomIds;
            }

            HttpClientHandler clientHandler = new HttpClientHandler();
            HttpClient client = new HttpClient(clientHandler);
            client.Timeout = TimeSpan.FromSeconds(30);

            var api = "https://localhost:5001/api/v1/room/";
            RoomIds.ForEach(async i =>
            {
                var response = await client.GetStringAsync(api + i);
                RoomEntity room = JsonConvert.DeserializeObject<RoomEntity>(response);
                Rooms.Add(room);
            });
        }

        private async void CreateBookingButton_Click(object sender, RoutedEventArgs e)
        {
            int CustomerId;
            int RoomId;

            if (CustomerCombo.SelectedItem != null)
            {
                CustomerId = Customers.Where(x => x.Name == ((CustomerEntity)(CustomerCombo.SelectedItem)).Name).Select(x => x.Id).FirstOrDefault();
            }
            else
            {
                CreateInvalidInputFlyOutOnElement(CustomerCombo);
                return;
            }

            if (RoomCombo.SelectedItem != null)
            {
                RoomId = (int)RoomCombo.SelectedItem;
            }
            else
            {
                CreateInvalidInputFlyOutOnElement(RoomCombo);
                return;
            }

            DateTime StartDate = BookingStartDatePicker.Date.Value != null ? BookingStartDatePicker.Date.Value.DateTime.Date : DateTime.Now.Date;
            DateTime EndDate = BookingEndDatePicker.Date.Value != null ? BookingEndDatePicker.Date.Value.DateTime.Date : DateTime.MaxValue.Date;

            Booking booking = new Booking
            {
                customerId = CustomerId,
                roomId = RoomId,
                startDate = StartDate,
                endDate = EndDate,
            };

            HttpClientHandler clientHandler = new HttpClientHandler();
            HttpClient client = new HttpClient(clientHandler);
            client.Timeout = TimeSpan.FromSeconds(30);

            var api = Url;
            string json = JsonConvert.SerializeObject(booking, Formatting.Indented);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(api, content);

            if (response.IsSuccessStatusCode)
            {
                String Name = Customers.Where(x => x.Id == CustomerId).Select(x => x.Name).FirstOrDefault();
                ContentDialog successDialog = new ContentDialog
                {
                    Title = "Booking successful",
                    Content = String.Format("Room {0} booked for {1} from {2} to {3}.", RoomId, Name, StartDate, EndDate),
                    CloseButtonText = "Ok"
                };
                await successDialog.ShowAsync();
                Frame.Navigate(typeof(BookingsPage));
            }
        }

        private async void StartDatePicker_DateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
        {
            DateTime StartDate = sender.Date.Value.DateTime.Date;
            DateTime EndDate = BookingEndDatePicker.Date == null ? sender.Date.Value.DateTime.AddMonths(1) 
                : BookingEndDatePicker.Date.Value.DateTime < StartDate ? sender.Date.Value.DateTime.AddMonths(1) 
                : BookingEndDatePicker.Date.Value.DateTime.Date;

            HttpClientHandler clientHandler = new HttpClientHandler();
            HttpClient client = new HttpClient(clientHandler);
            client.Timeout = TimeSpan.FromSeconds(30);

            var api = RoomUrl;
            var query = String.Format("/available?from={0}&to={1}", StartDate, EndDate);
            var response = await client.GetStringAsync(api + query);
            Rooms = JsonConvert.DeserializeObject<List<RoomEntity>>(response);
        }

        private async void EndDatePicker_DateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
        {
            if (sender.Date.Value.DateTime == null) return;
            DateTime StartDate = BookingStartDatePicker.Date.Value.DateTime.Date;
            DateTime EndDate = sender.Date.Value.DateTime.Date;

            HttpClientHandler clientHandler = new HttpClientHandler();
            HttpClient client = new HttpClient(clientHandler);
            client.Timeout = TimeSpan.FromSeconds(30);

            var api = RoomUrl;
            var query = String.Format("/available?from={0}&to={1}", StartDate, EndDate);
            var response = await client.GetStringAsync(api + query);
            Rooms = JsonConvert.DeserializeObject<List<RoomEntity>>(response);
        }

        private void CreateInvalidInputFlyOutOnElement(FrameworkElement element)
        {
            Flyout invalidInputFlyout = new Flyout
            {
                Content = new TextBox
                {
                    Text = "Field must have a value"
                }
            };

            invalidInputFlyout.ShowAt(element);
        }
    }
}
