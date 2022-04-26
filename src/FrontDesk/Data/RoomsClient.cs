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
    public class RoomsClient
    {
        private string BaseUrl = "http://localhost:5001";
        private string Url = "/api/v1/room";

        public RoomsClient() { }

        public async Task<Room> GetRoomById(int Id)
        {
            using (var client = new HttpClient())
            {
                //client.BaseAddress = new Uri(BaseUrl);
                //client.DefaultRequestHeaders.Accept.Clear();
                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                
                var response = await client.GetStringAsync("https://localhost:5001/api/v1/room" + "/" + Id).ConfigureAwait(false);

                if (true)
                {
                    Room room = JsonConvert.DeserializeObject<Room>(response);
                    return room;
                }
                return null;
            }

        }

        public async Task<List<Room>> GetRooms()
        {
            using (var client = new HttpClient())
            {
                //client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.GetAsync("https://localhost:5001/api/v1/room").ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    List<int> roomIds = JsonConvert.DeserializeObject<List<int>>(json);

                    List<Room> rooms = new List<Room>();

                    roomIds.ForEach(async Id =>
                    {
                        Room room = await GetRoomById(Id);
                        rooms.Add(room);
                    });
                    
                    return rooms;
                }
                return null;
            }
        }
    }
}
