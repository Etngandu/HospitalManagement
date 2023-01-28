using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using HospitalManagement.Entities;
using HospitalManagement.Entities.Enums;

namespace ENBHospitalmanagementMvc.Models
{
    public class DisplayPatient_Drug_Treatment
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int DiagnoseId { get; set; }
        public int DrugId { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime DateCreated { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime DateModified { get; set; } 
        public string Dosage_administred { get; set; }
        public string Comments { get; set; }
        public string DrugName { get; set; }
    }
}