using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using DAT154Oblig4.Domain.Entities;
using FrontDesk.Data;

namespace FrontDesk.Pages
{
    /// <summary>
    /// Interaction logic for RoomPage.xaml
    /// </summary>
    public partial class RoomPage : Page
    {

        private List<Room> rooms;
        RoomsClient roomsClient;
        public List<Room> Rooms
        {
            get
            {
                return rooms;
            }
            set { rooms = value; }
        }

        public RoomPage()
        {
            roomsClient = new RoomsClient();
            DataContext = this;
            InitializeComponent();
        }



        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            
            
            
        }

        private async void Page_Initialized(object sender, EventArgs e)
        {
            rooms = await roomsClient.GetRooms();
            rooms.ForEach(room =>
            {
                AddRoom(room);
            });
            ListViewRooms.ItemsSource = rooms;
            
        }

        private void AddRoom(Room room)
        {
            ListViewItem roomItem = new ListViewItem();
            roomItem.Content = "";
            ListViewRooms.Items.Add(roomItem);
        }
    }
}
