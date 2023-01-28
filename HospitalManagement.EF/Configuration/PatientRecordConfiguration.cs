
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

    public class PatientRecordConfiguration : EntityTypeConfiguration<Patient_Record>
    {
        // <summary>
        /// Initializes a new instance of the Staffmember class.
        /// </summary>
        /// 

        public PatientRecordConfiguration()
        {

            Property(x => x.Medical_Condition).IsRequired().HasMaxLength(80);
            Property(x => x.Other_Details).HasMaxLength(90);           

        }

    }
}
