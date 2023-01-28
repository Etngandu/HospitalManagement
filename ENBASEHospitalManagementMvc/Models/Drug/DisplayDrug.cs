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
    public class DisplayDrug
    {
        public int Id { get; set; }
        public Ref_Drug_Category Ref_Drug_Category { get; set; }  
        public string Drug_name { get; set; }  
        public string Drug_Description { get; set; }  
        public decimal Drug_cost { get; set; }        
        public string Drug_Other_details { get; set; }        
        public Decimal Drug_Cost_unit { get; set; }        
        public DateTime DateCreated { get; set; }        
        public DateTime DateModified { get; set; }

    }
}