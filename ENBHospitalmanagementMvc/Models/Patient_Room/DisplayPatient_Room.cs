using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using HospitalManagement.Entities;
using HospitalManagement.Entities.Enums;
using System.ComponentModel;
using HospitalManagement.Entities.Collections;

namespace ENBHospitalmanagementMvc.Models
{
    public class DisplayPatient_Room
    {
        public int Id { get; set; }
        public int Patient_Id { get; set; }
        public string Room_Name { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime Date_stay_from { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime Date_depart_to { get; set; }
    }
}