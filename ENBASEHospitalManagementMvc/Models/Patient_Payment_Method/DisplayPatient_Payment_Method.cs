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
    public class DisplayPatient_Payment_Method
    {
        public int id { get; set; }
        public Payment_Method_Code payment_method_code { get; set; }        
        public string payment_method_details { get; set; }
    }
}