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
    public class Patient_in_BedConfiguration: EntityTypeConfiguration<Patient_in_Bed>
    {
        // <summary>
        /// Initializes a new instance of the Staffmember class.
        /// </summary>
        /// 

        public Patient_in_BedConfiguration()
        {
            Property(x => x.Date_from).IsRequired();
            Property(x => x.Date_to).HasColumnName("Date_to");

        }
    }
}
