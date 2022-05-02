using Desktop.Entities;
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
    public sealed partial class RoomsPage : Page
    {
        const string URL = "https://localhost:5001/api/v1/room";
        const string ServiceUrl = "https://localhost:5001/api/v1/task";
        public List<RoomEntity> Rooms = new List<RoomEntity>();
        public List<int> RoomIds = new List<int>();
        public List<string> TaskStatuses = new List<string> { "New", "Started", "Finished" };
        public List<string> TaskTypes = new List<string> { "Cleaning", "Service", "Maintenance" };
        public List<string> TaskPriorities = new List<string> { "LOW", "MEDIUM", "HIGH" };


        public RoomsPage()
        {
            this.InitializeComponent();
            GetRooms();
        }

        private async void GetRooms()
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            HttpClient client = new HttpClient(clientHandler);
            client.Timeout = TimeSpan.FromSeconds(30);

            var response = await client.GetStringAsync(URL);
            var rooms = JsonConvert.DeserializeObject<List<RoomEntity>>(response);
            Rooms = rooms;
            RoomIds = rooms.Select(x => x.Id).Distinct().ToList();
            RoomIdCombo.ItemsSource = RoomIds;
            RoomsMenu.ItemsSource = Rooms;
        }

        private async void SubmitServiceTaskButton_Click(object sender, RoutedEventArgs e)
        {
            int RoomId;
            String Description, Notes;
            ServiceTaskType TaskType;
            ServiceTaskStatus TaskStatus;
            ServiceTaskPriority Priority;

            if (RoomIdCombo.SelectedItem != null)
            {
                RoomId = (int) RoomIdCombo.SelectedItem;
            } else
            {
                CreateInvalidInputFlyOutOnElement(RoomIdCombo);
                return;
            }

            Description = String.IsNullOrEmpty(DescriptionInput.Text) ? "" : DescriptionInput.Text;
            Notes = String.IsNullOrEmpty(NotesInput.Text) ? "" : NotesInput.Text;

            if (TaskTypeCombo.SelectedItem != null)
            {
                TaskType = (ServiceTaskType)TaskTypeCombo.SelectedIndex;
            } 
            else
            {
                CreateInvalidInputFlyOutOnElement(TaskTypeCombo);
                return;
            }

            if (TaskStatusCombo.SelectedItem != null)
            {
                TaskStatus = (ServiceTaskStatus)TaskStatusCombo.SelectedIndex;
            }
            else
            {
                CreateInvalidInputFlyOutOnElement(TaskStatusCombo);
                return;
            }

            if (TaskPriorityCombo.SelectedItem != null)
            {
                Priority = (ServiceTaskPriority)TaskPriorityCombo.SelectedIndex;
            }
            else
            {
                CreateInvalidInputFlyOutOnElement(TaskTypeCombo);
                return;
            }

            ServiceTask ServiceTask = new ServiceTask
            {
                roomId = RoomId,
                description = Description,
                taskType = TaskType,
                taskStatus = TaskStatus,
                priority = Priority,
                notes = Notes
            };



            HttpClientHandler clientHandler = new HttpClientHandler();
            HttpClient client = new HttpClient(clientHandler);
            client.Timeout = TimeSpan.FromSeconds(30);

            var api = ServiceUrl;
            string json = JsonConvert.SerializeObject(ServiceTask, Formatting.Indented);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(api, content);

            if (response.IsSuccessStatusCode)
            {
                ContentDialog serviceTaskGeneratedDialog = new ContentDialog
                {
                    Title = "Service task succesfully generated",
                    Content = String.Format("{0} task generated for room {1}", TaskType, RoomId),
                    CloseButtonText = "Ok",
                };

                await serviceTaskGeneratedDialog.ShowAsync();
            }
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

        private async void RoomsMenu_ItemClick(object sender, ItemClickEventArgs e)
        {
            RoomEntity room = (RoomEntity)e.ClickedItem;
            int roomId = room.Id;

            HttpClientHandler clientHandler = new HttpClientHandler();
            HttpClient client = new HttpClient(clientHandler);
            client.Timeout = TimeSpan.FromSeconds(30);

            var api = ServiceUrl + "/room/";
            var response = await client.GetStringAsync(api + roomId);
            var tasks = JsonConvert.DeserializeObject<List<ServiceTaskEntity>>(response);
            var notes = tasks.Where(x => x.TaskStatus == 0).Select(x => x.Notes);
            ListView notesList = new ListView();
            notesList.ItemsSource = notes;

            ContentDialog notesDialog = new ContentDialog
            {
                Title = String.Format("Room {0} notes", roomId),
                Content = notesList,
                CloseButtonText = "Ok",
            };

            await notesDialog.ShowAsync();
        }
    }
}
