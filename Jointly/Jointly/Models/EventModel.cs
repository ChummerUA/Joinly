using System;
using System.Collections.Generic;
using System.Text;

namespace Jointly.Models
{
    public class EventModel
    {
        public int ID { get; set; }

        public UserModel Author { get; set; }

        public string Name { get; set; }

        public DateTime CreationTime { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string Description { get; set; }

        public string StartTime { get; set; }

        public string EndTime { get; set; }
    }
}
