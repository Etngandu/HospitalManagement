using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using HospitalManagement.Entities;
using HospitalManagement.Entities.Enums;
using System.ComponentModel;

namespace ENBHospitalmanagementMvc.Models
{
    public class DisplayPatient
    {
        public int Id { get; set; }

        [DisplayName("Outpatient yn")]
        public string Outpatient_yn { get; set; }

        [DisplayName("Hospital Number")]
        public string Hospital_Number { get; set; }

        [DisplayName("Hospital Number")]
        public string nhs_number { get; set; }
       
        public Gender Gender { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]

        [DisplayName("Date Of Birth")]
        public DateTime Date_Of_Birth { get;   set; }
        public string Fullname { get; set; }

        [DisplayName("Patient first name")]
        public string Patient_first_name { get; set; }

        [DisplayName("Patient last name")]
        public string Patient_last_name { get; set; }        
        public string Height { get; set; }
        public string Weight { get; set; }

        [DisplayName("Next of kin")]
        public string Next_of_kin { get; set; }

        [DisplayName("Home phone")]
        public string Home_phone { get; set; }

        [DisplayName("Work phone")]
        public string Work_phone { get; set; }

        [DisplayName("Cell_Mobile_phone")]
        public string Cell_Mobile_phone { get; set; }

        [DisplayName("Other_patient_details")]
        public string Other_patient_details { get; set; }

    }
}