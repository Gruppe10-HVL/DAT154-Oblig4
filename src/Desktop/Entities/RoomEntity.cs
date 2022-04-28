using Desktop.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Entities
{
    public class RoomEntity
    {
        public int Id { get; set; }
        public int BedCount { get; set; }
        public int Size { get; set; }
        public RoomQuality Quality { get; set; }
    }
}
