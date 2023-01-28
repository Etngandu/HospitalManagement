

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

    public class BedConfiguration : EntityTypeConfiguration<Bed>
    {
        // <summary>
        /// Initializes a new instance of the Staffmember class.
        /// </summary>
        /// 

        public BedConfiguration()
        {
            Property(x => x.Bed_Number).IsRequired().HasMaxLength(15);
            Property(x => x.Bed_Location).HasMaxLength(50);           

        }

    }
}
