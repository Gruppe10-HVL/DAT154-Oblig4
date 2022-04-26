using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DAT154Oblig4.Domain.Enums;

namespace FrontDesk.Models
{
    public class Room
    {
        public int Id { get; set; }
        public int BedCount { get; set; }
        public int Size { get; set; }
        public RoomQuality Quality { get; set; }

        public Room() { }
    }
}
