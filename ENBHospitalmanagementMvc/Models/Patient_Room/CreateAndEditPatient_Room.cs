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
    public class CreateAndEditPatient_Room
    {
        public int Id { get; set; }
        public int Patient_Id { get; set; }

        [Required]
        [DisplayName("Room Name")]
        public string Room_Name { get; set; }
        public DateTime Date_stay_from { get; set; }
       
        public DateTime Date_depart_to { get; set; }
        
    }
}