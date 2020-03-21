using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SerapisMedicalAPI.Model.AppointmentModel
{
    public class AppointmentDto
    {
        public ObjectId AppointmentId { get; set; }

        public DateTime DateBooked { get; set; }

        public DateTime TimeBooked { get; set; }
    }
}
