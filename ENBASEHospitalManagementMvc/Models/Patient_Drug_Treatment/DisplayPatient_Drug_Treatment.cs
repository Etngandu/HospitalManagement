using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using HospitalManagement.Entities;
using HospitalManagement.Entities.Enums;

namespace ENBASEHospitalManagementMvc.Models
{
    public class DisplayPatient_Drug_Treatment
    {
        public int Id { get; set; }
        public int Owner_Patient { get; set; }
        public int Owner_Diagnose { get; set; }
        public int Owner_Drug { get; set; }
        public DateTime DateCreated { get; set; }        
        public DateTime DateModified { get; set; } 
        public string Dosage_administred { get; set; }
        public string Comments { get; set; }
    }
}