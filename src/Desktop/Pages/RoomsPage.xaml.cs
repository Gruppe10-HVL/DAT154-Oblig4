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
    public sealed partial class RoomsPage : Page
    {
        const string URL = "https://localhost:5001/api/v1/room";
        public List<RoomEntity> Rooms = new List<RoomEntity>();

        public RoomsPage()
        {
            this.InitializeComponent();
            StartDatePicker.SelectedDate = DateTime.Now;
            GetRooms();
        }

        private async void GetRooms()
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            HttpClient httpClient = new HttpClient(clientHandler);
            httpClient.Timeout = TimeSpan.FromSeconds(30);

            var response = await httpClient.GetStringAsync(URL);
            var rooms = JsonConvert.DeserializeObject<List<RoomEntity>>(response);
            Rooms = rooms;
            RoomsMenu.ItemsSource = Rooms;
        }

        private async void StartDatePicker_SelectedDateChanged(DatePicker sender, DatePickerSelectedValueChangedEventArgs args)
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            HttpClient httpClient = new HttpClient(clientHandler);
            httpClient.Timeout = TimeSpan.FromSeconds(30);

            var response = await httpClient.GetStringAsync(URL);
            var rooms = JsonConvert.DeserializeObject<List<RoomEntity>>(response);
            Rooms = rooms;
            RoomsMenu.ItemsSource = Rooms;
        }

        private async void EndDatePicker_SelectedDateChanged(DatePicker sender, DatePickerSelectedValueChangedEventArgs args)
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            HttpClient httpClient = new HttpClient(clientHandler);
            httpClient.Timeout = TimeSpan.FromSeconds(30);

            var response = await httpClient.GetStringAsync(URL);
            var rooms = JsonConvert.DeserializeObject<List<RoomEntity>>(response);
            Rooms = rooms;
            RoomsMenu.ItemsSource = Rooms;
        }

        private void RoomsMenu_ItemClick(object sender, ItemClickEventArgs e)
        {
            RoomEntity room = e.ClickedItem as RoomEntity;
            Frame.Navigate(typeof(BookingPage), room);
        }
    }
}
