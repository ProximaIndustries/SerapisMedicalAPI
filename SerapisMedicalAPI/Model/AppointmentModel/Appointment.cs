﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SerapisMedicalAPI.Model.AppointmentModel
{
    public class Appointment
    {
        //Using int for now, need to use mongoId
        public int BookingId { get; set; }
        public int LineNumber { get; set; }
        public string DateBooked { get; set; }
        public string TimeBooked { get; set; }
        public bool HasSeenGP { get; set; }
        public TimeSpan Duration { get; set; }

        public bool HasBeenToThisPractice { get; set; }
    }
}
