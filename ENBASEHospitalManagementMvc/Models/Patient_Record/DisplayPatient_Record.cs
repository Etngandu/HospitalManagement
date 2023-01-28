using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using HospitalManagement.Entities;
using HospitalManagement.Entities.Enums;
using System.ComponentModel;


namespace ENBASEHospitalManagementMvc.Models
{
    public class DisplayPatient_Record
    {
        public int Id { get; set; }       
        public int Staff_Id { get; set; }
        public int Patient_Id { get; set; }
        public DateTime Admisssion_datetime { get; set; }       
        public string Medical_Condition { get; set; } 
        public string Other_Details { get; set; }       
        public DateTime DateCreated { get; set; }       
        public DateTime DateModified { get; set; }

    }
}