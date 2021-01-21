using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SerapisMedicalAPI.Model.AppointmentModel
{
    public class BookedAppointment
    {
        [BsonElement("bookedpatientid")]
        public string BookedpatientId { get; set; }

        [BsonElement("appointmentsession")]
        public Session AppointmentSession { get; set; } 
    }
}
