using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ENBHospitalmanagementMvc.Models
{
    public class DisplayPatient_in_Bed
    {

        public int Id { get; set; }
        public int Patient_Id { get; set; }
        public int Ward_Id { get; set; }
        public int Bed_Id { get; set; }

        public string BedNumber { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime Date_from { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime Date_to { get; set; }
    }
}