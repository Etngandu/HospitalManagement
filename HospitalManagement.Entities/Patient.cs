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
    public class Patient : DomainEntity<int>, IDateTracking
    {

          #region Constructors

        ///// <summary>
        ///// Initializes a new instance of the Person class.
        ///// </summary>
        public Patient()
        {            
            Addresses = new Addresses();
            Patient_Rooms = new Patient_Rooms();
            Patient_Records = new Patient_Records();
            Patient_Bills = new Patient_Bills();
            Patient_Payment_Methods = new Patient_Payment_Methods();
            Diagnoses = new Diagnoses();
            Patient_Drug_Treatments = new Patient_Drug_Treatments();
            Doctor_Assigned_To_Patients = new Doctor_Assigned_to_Patients();
            Patient_In_Beds = new Patient_in_Beds();
            Patient_In_Wards = new Patient_in_Wards();

        }

        #endregion


        #region "Public Properties"

        

        /// <summary>
        /// Gets or sets the first name of this person.
        /// </summary>
        [Required]
        public string Patient_first_name { get; set; }       

        /// <summary>
        /// Gets or sets Patient_last_name.
        /// </summary>
        [Required]
        public string Patient_last_name { get; set; }

        
        /// <summary>
        /// Gets or sets the fheight of this patient.
        /// </summary>
        
        public string Height { get; set; }

        /// <summary>
        /// Gets or sets the first name of this person.
        /// </summary>
        [Required]
        public string Outpatient_yn { get; set; }

        /// <summary>
        /// Gets or sets the last name of this person.
        /// </summary>
        [Required]
        public string Hospital_Number { get; set; }

        /// <summary>
        /// Gets or sets the first name of this person.
        /// </summary>
        
        public string nhs_number { get; set; }

        /// <summary>
        /// Gets or sets the last name of this person.
        /// </summary>
        
        public Gender Gender { get; set; }

        /// <summary>
        /// Gets or sets the PublicatieDatum of the Gastenboek.
        /// </summary>
        public DateTime Date_Of_Birth
        {
            get;
            set;
        }


        /// <summary>
        /// Gets or sets the weight of this patient.
        /// </summary>
        
        public string Weight { get; set; }

        /// <summary>
        /// Gets or set Next of kin of this patient.
        /// </summary>
       
        public string Next_of_kin { get; set; }

        /// <summary>
        /// Gets or sets the Home_phone of this patient.
        /// </summary>
       
        public string Home_phone { get; set; }

        /// <summary>
        /// Gets or sets the work phone of this patient.
        /// </summary>
        
        public string Work_phone { get; set; }

        /// <summary>
        /// Gets or sets the Cell_Mobile_phone of this patient.
        /// </summary>
       
        public string Cell_Mobile_phone { get; set; }

        /// <summary>
        /// Gets or sets Other_patient_details.
        /// </summary>
        
        public string Other_patient_details { get; set; }


        /// <summary>
        /// Gets the full name of this patient.
        /// </summary>
        public string FullName
        {
            get
            {
                string temp = Patient_first_name ?? string.Empty;
                
                    if (!string.IsNullOrEmpty(Patient_last_name))
                    {
                        if (temp.Length > 0)
                        {
                            temp += " ";
                        }
                        temp += Patient_last_name;
                    }
                    else
                    {

                        temp += Patient_last_name;
                    }
                
                return temp;
            }
        }


        /// <summary>
        /// Gets or sets the date and time the object was created.
        /// </summary>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// Gets or sets the date and time the object was last modified.
        /// </summary>
        public DateTime DateModified { get; set; }


        /// <summary>
        /// Gets or sets one to many relationship between Patient and addres
        /// </summary>

        public Addresses Addresses { get; set; }

        /// <summary>
        /// Gets or sets one to many relationship between Patient and Patient_Rooms
        /// </summary>

        public Patient_Rooms Patient_Rooms { get; set; }

        /// <summary>
        /// Gets or sets one to many relationship between Patient and Payment_methods
        /// </summary>

        public Patient_Payment_Methods Patient_Payment_Methods { get; set; }

        /// <summary>
        /// Gets or sets many to many relationship between Patient and addres
        /// </summary>

        public Patient_Bills Patient_Bills { get; set; }

        /// <summary>
        /// Gets or sets many to many relationship between Patient and addres
        /// </summary>

        public Patient_Records Patient_Records { get; set; }

        /// <summary>
        /// Gets or sets many to many relationship between Patient and addres
        /// </summary>

        public Diagnoses Diagnoses { get; set; }

        /// <summary>
        /// Gets or sets one to many relationship between Patient and Patient_Drug_Treatment
        /// </summary>

        public Patient_Drug_Treatments Patient_Drug_Treatments { get; set; }

        /// <summary>
        /// Gets or sets one to many relationship between Patient and Doctor_Assigned_to_Patients
        /// </summary>

        public Doctor_Assigned_to_Patients Doctor_Assigned_To_Patients { get; set; }

        /// <summary>
        /// Gets or sets one to many relationship between Patient and Patient_In_Beds
        /// </summary>
        /// 

        public Patient_in_Beds Patient_In_Beds { get; set; }

        /// <summary>
        /// Gets or sets one to many relationship between Patient and Patient_In_Wards
        /// </summary>
        /// 

        public Patient_in_Wards Patient_In_Wards { get; set; }

        #endregion


        #region Methods

        /// <summary>
        /// Validates this object. It validates dependencies between properties and also calls Validate on child collections;
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns>A IEnumerable of ValidationResult. The IEnumerable is empty when the object is in a valid state.</returns>
        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            //if (Type == PersonType.None)
            //{
            //    yield return new ValidationResult("Type can't be None.", new[] { "Type" });
            //}

            if (Date_Of_Birth < DateTime.Now.AddYears(Constants.MaxAgePerson * -1))
            {
                yield return new ValidationResult("Invalid range for DateOfBirth; must be between today and 130 years ago.", new[] { "DateOfBirth" });
            }
            if (Date_Of_Birth > DateTime.Now)
            {
                yield return new ValidationResult("Invalid range for DateOfBirth; must be between today and 130 years ago.", new[] { "DateOfBirth" });
            }

            foreach (var result in Addresses.Validate())
            {
                yield return result;
            }

            foreach (var result in Patient_Bills.Validate())
            {
                yield return result;
            }

            foreach (var result in Patient_Records.Validate())
            {
                yield return result;
            }

            foreach (var result in Patient_Rooms.Validate())
            {
                yield return result;
            }

            foreach (var result in Patient_Payment_Methods.Validate())
            {
                yield return result;
            }
        }
        #endregion

    }
}
