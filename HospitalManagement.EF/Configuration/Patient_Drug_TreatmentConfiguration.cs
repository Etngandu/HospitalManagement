using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using HospitalManagement.Entities;

namespace HospitalManagement.EF.Configuration
{

    /// <summary>
    /// Configures the behavior for a Patient_Drug_Treatment in the model and the database.
    /// </summary>
    /// 
    public class Patient_Drug_TreatmentConfiguration: EntityTypeConfiguration<Patient_Drug_Treatment>
    {
        // <summary>
        /// Initializes a new instance of the Patient_Drug_TreatmentConfiguration class.
        /// </summary>
        /// 

        public Patient_Drug_TreatmentConfiguration()
        {
            Property(x => x.Dosage_administred).HasMaxLength(90);
            Property(x => x.Comments).HasMaxLength(200);
        }

    }
}
