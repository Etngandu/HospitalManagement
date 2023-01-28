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

    public class WardConfiguration : EntityTypeConfiguration<Ward>
    {
        // <summary>
        /// Initializes a new instance of the Staffmember class.
        /// </summary>
        /// 

        public WardConfiguration()
        {          
           
            Property(x => x.Ward_Name).IsRequired().HasMaxLength(40);
            Property(x => x.Ward_Location).HasMaxLength(40);
            Property(x => x.Ward_description).HasMaxLength(80);           

        }

    }
}
