using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using HospitalManagement.Entities;
using HospitalManagement.Entities.Enums;
using System.ComponentModel;
using System.Web.UI.WebControls;
using System.Web.Mvc;

namespace ENBHospitalmanagementMvc.Models
{
    public class CreateAndEditDiagnose: IValidatableObject
    {
        

        public int Id { get; set; }
        public int Patient_Id { get; set; }
        public int Staff_Id { get; set; }
        public string Diagnose_details { get; set; }

        public List<SelectListItem> ListStaff { get; set; }

       
        public DateTime DateCreated { get; set; }

        
        public DateTime DateModified { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (String.IsNullOrEmpty(Diagnose_details))
            {
                yield return new ValidationResult("Diagnose_details can't be Null.", new[] { "Diagnose_details" });
            }
        }
    }
}