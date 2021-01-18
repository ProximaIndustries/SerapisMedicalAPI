using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SerapisMedicalAPI.Model.AppointmentModel
{
    public class BookedAppointment
    {
        public string BookedpatientId { get; set; }

        public Session AppointmentSession { get; set; }
    }
}
