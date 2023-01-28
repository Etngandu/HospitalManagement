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
    public class DisplayWard
    {
        public int Id { get; set; }
        public string Ward_Name { get; set; }        
        public string Ward_Location { get; set; }       
        public string Ward_description { get; set; }
    }
}