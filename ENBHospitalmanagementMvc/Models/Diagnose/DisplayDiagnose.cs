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
    public class DisplayDiagnose
    {

        public int Id { get; set; }
        public int Patient_Id { get; set; }
        public int Staff_Id { get; set; }
        public string Diagnose_details { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime DateCreated { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime DateModified { get; set; }

    }
}