using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ENBHospitalmanagementMvc.Models
{
    public class CreateAndEditPatient_in_Ward
    {
        public int Id { get; set; }
        public int Patient_Id { get; set; }
        public int Ward_Id { get; set; }

       [Required]
        public DateTime? Date_from { get; set; }

        [Required]
        public DateTime? Date_to { get; set; }

        public List<SelectListItem> ListWard { get; set; }

        
    }
}