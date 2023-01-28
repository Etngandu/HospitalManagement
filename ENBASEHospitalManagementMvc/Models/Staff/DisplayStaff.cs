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
    public class DisplayStaff
    {

        public int Id { get; set; }
        public Staff_Category_Code Staff_Category_code { get; set; }    
        public Gender Gender { get; set; }      
        public Staff_JobTitle Staff_job_title { get; set; }    
        public string Staff_first_name { get; set; }        
        public string EmailAddressText { get; set; }   
        public string Staff_last_name { get; set; }

        [DisplayFormat(DataFormatString = "{0: MM/dd/yyyy}")]
        [DisplayName("Date of birth")]
        public DateTime Staff_birth_date    { get; set; }
        public Address Addresses { get; set; } 
        public string Other_staff_details { get; set; }

    }
}