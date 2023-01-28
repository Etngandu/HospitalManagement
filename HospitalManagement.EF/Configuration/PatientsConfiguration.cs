using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using HospitalManagement.Entities;

namespace HospitalManagement.Repositories.EF.Configuration
{
    /// <summary>
    /// Configures the behavior for a person in the model and the database.
    /// </summary>
    /// 
 
    public  class PatientConfiguration:EntityTypeConfiguration<Patient>
    {
        // <summary>
        /// Initializes a new instance of the Patient class.
        /// </summary>
        /// 
        public PatientConfiguration()
        {

            Property(x => x.Patient_first_name).IsRequired().HasMaxLength(25);           
            Property(x => x.Patient_last_name).IsRequired().HasMaxLength(40);


            Property(x => x.Outpatient_yn).HasMaxLength(25);
            Property(x => x.Hospital_Number).HasMaxLength(25);
            Property(x => x.nhs_number).HasMaxLength(25);
            Property(x => x.Gender).HasColumnName("Gender");
            Property(x => x.Height).HasMaxLength(25);
            Property(x => x.Weight).HasMaxLength(25);
            Property(x => x.Next_of_kin).HasMaxLength(25);
            Property(x => x.Home_phone).HasMaxLength(25);
            Property(x => x.Work_phone).HasMaxLength(25);
            Property(x => x.Cell_Mobile_phone).HasMaxLength(25);
            Property(x => x.Other_patient_details).HasMaxLength(250);
            
        }

    }
}
