using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using HospitalManagement.Entities.Collections;
using HospitalManagement.Infrastructure;
using HospitalManagement.Entities.Enums;
using HospitalManagement.Entities;
namespace HospitalManagement.Entities
{
    /// <summary>
    /// Represents Adresses in the system.
    /// </summary>
    public class Address : DomainEntity<int>
    {

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Address class.
        /// The constructor is marked private because we want other consuming code to use the overloaded constructor.
        /// However, EF still needs a parameterless constructor.
        /// </summary>
        private Address() { }

        /// <summary>
        /// Initializes a new instance of the Address class.
        /// </summary>
        /// <param name="name_street">The name_street of this address.</param>
        /// <param name="number_building">The number_building of this address.</param>
        /// <param name="area_locality">The area-locality of this address.</param>
        /// <param name="city">The city of this address.</param>
        /// <param name="zip_postcode">The zip postcode of this address.</param>
        /// <param name="state_province">The state or province of this address.</param>
        /// <param name="country">The country of this address.</param>
        /// <param name="other_adresses_details">The other details of this address.</param>
        /// <param name="contactType">The type of this address.</param>
        public Address(string name_street, string number_building, string area_locality, string city,
              string zip_postcode,string state_province, string country,string other_adresses_details, ContactType contactType)
        {
            Name_street = name_street;
            Number_building = number_building;
            Area_locality = area_locality;
            City = city;
            Zip_postcode = zip_postcode;
            State_province = state_province;
            Country = country;
            Other_adresses_details = other_adresses_details;
            ContactType = contactType;
        }

        #endregion

        #region "Public Properties"

        
        /// <summary>
        /// Gets or sets the last name of this person.
        /// </summary>
        
        public string Name_street { get; private set; }

        /// <summary>
        /// Gets or sets the first name of this person.
        /// </summary>
        
        public string Number_building { get; private set; }


        /// <summary>
        /// Gets or sets the first name of this person.
        /// </summary>
        
        public string Area_locality { get; private set; }

        /// <summary>
        /// Gets or sets the last name of this person.
        /// </summary>
        
        public string City { get; private set; }

        /// <summary>
        /// Gets or sets the first name of this person.
        /// </summary>
       
        public string Zip_postcode { get; private set; }

        /// <summary>
        /// Gets or sets Patient_middle_name
        /// </summary>
        [Required]
        public string State_province { get; private set; }        

        /// <summary>
        /// Gets or sets the fheight of this patient.
        /// </summary>
        [Required]
        public string Country { get; private set; }
       

        /// <summary>
        /// Gets or sets Other_adresses_details.
        /// </summary>
        [Required]
        public string Other_adresses_details { get;  private set; }


        /// <summary>
        /// Gets or sets the owner Patient.
        /// </summary>
        [Owner("")]

        public Patient Owner_Patient { get; private set; }

        /// <summary>
        /// Gets or sets the owner Staff.
        /// </summary>
        [Owner("")]
        public Staff Owner_Staff { get; private set; }


        /// <summary>
        /// Gets the contact type of this address. 
        /// </summary>
        public ContactType ContactType { get; private set; }

        /// <summary>
        /// Determines if this address can be considered to represent a "null" value.
        /// </summary>
        // <returns>True when all four properties of the address contain null; false otherwise. 
        public bool IsNull
        {
            get
            {
                return (string.IsNullOrEmpty(Name_street) && string.IsNullOrEmpty(Area_locality) && string.IsNullOrEmpty(City) && string.IsNullOrEmpty(Zip_postcode)
                  && string.IsNullOrEmpty(State_province) && string.IsNullOrEmpty(Country));
            }
        }


       

        #endregion


        #region Methods

        /// <summary>
        /// Validates this object. 
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns>A IEnumerable of ValidationResult. The IEnumerable is empty when the object is in a valid state.</returns>
        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!IsNull)
            {

                if (ContactType == ContactType.None)
                {
                    yield return new ValidationResult("ContactType can't be None.", new[] { "ContactType" });
                }
                if (string.IsNullOrEmpty(Name_street))
                {
                    yield return new ValidationResult("name_street can't be null or empty", new[] { "Name_street" });
                }
                if (string.IsNullOrEmpty(Area_locality))
                {
                    yield return new ValidationResult("area_locality can't be null or empty", new[] { "Area_locality" });
                }
                if (string.IsNullOrEmpty(City))
                {
                    yield return new ValidationResult("City can't be null or empty", new[] { "City" });
                }
                if (string.IsNullOrEmpty(Zip_postcode))
                {
                    yield return new ValidationResult("zip_postcode can't be null or empty", new[] { "Zip_postcode" });
                }
                if (string.IsNullOrEmpty(State_province))
                {
                    yield return new ValidationResult("zip_postcode can't be null or empty", new[] { "State_province" });
                }
                if (string.IsNullOrEmpty(Country))
                {
                    yield return new ValidationResult("Country can't be null or empty", new[] { "Country" });
                }
            }
        }
        #endregion


    }
}
