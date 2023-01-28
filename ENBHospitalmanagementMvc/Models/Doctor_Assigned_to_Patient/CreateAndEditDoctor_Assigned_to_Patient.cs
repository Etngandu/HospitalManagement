using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ENBHospitalmanagementMvc.Models
{
    public class CreateAndEditDoctor_Assigned_to_Patient
    {
        public int Id { get; set; }
        public int Patient_Id { get; set; }
        public int Staff_Id { get; set; }

       [Required]
       [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? Date_Ass_from { get; set; }

       [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? Date_Ass_to { get; set; }

        public List<SelectListItem> ListStaff { get; set; }

       
    }
}