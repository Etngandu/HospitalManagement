using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using HospitalManagement.Entities;
using HospitalManagement.Entities.Enums;

namespace ENBHospitalmanagementMvc.Models
{
    public class CreateAndEditAddres: IValidatableObject
    {
        public int Patient_Id { get; private set; }
        public int Staff_Id { get; private set; }
        public string Name_street { get; private set; }
        public string Number_building { get; private set; }
        public string Area_locality { get; private set; }
        public string City { get; private set; }
        public string Zip_postcode { get; private set; }
        public string State_province { get; private set; }
        public string Country { get; private set; }
        public string Other_adresses_details { get; private set; }       
        public ContactType ContactType { get; private set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (ContactType == ContactType.None)
            {
                yield return new ValidationResult("ContactType can't be None.", new[] { "ContactType" });
            }
        }


    }
}