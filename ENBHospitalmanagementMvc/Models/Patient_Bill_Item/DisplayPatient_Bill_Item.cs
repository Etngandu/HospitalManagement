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
    public class DisplayPatient_Bill_Item
    {
        public int Id  { get; set; }
        public double quantity { get; set; }
        public decimal totalcost { get; set; }

    }
}