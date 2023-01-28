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
    public class DisplayBed
    {
        
        public int Id { get; set; }
        public int WardId { get; set; }
        public string Bed_Number { get; set; }
        public string Bed_Location { get; set; }
    }
}