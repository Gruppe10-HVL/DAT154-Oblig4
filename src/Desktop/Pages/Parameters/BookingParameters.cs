using Desktop.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Pages
{
    public class BookingParameters
    {
        public List<CustomerEntity> Customers { get; set; }
        public List<int> RoomIds { get; set; } 
        public BookingParameters() { }

    }
}
