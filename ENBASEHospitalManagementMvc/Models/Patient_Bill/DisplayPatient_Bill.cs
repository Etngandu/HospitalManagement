using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using HospitalManagement.Entities;
using HospitalManagement.Entities.Enums;
using System.ComponentModel;
using HospitalManagement.Entities.Collections;

namespace ENBASEHospitalManagementMvc.Models
{
    public class DisplayPatient_Bill
    {
        public int Id { get; set; }        
        public DateTime Date_bill_paid { get; set; }     
        public decimal Total_amount_due { get; set; }     
        public string Other_bill_details { get; set; }
        public Patient_Bill_Items Patient_Bill_Items { get; set; }        
        public DateTime DateCreated { get; set; }       
        public DateTime DateModified { get; set; }


    }
}