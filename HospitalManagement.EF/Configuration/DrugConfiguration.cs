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

    public class DrugConfiguration : EntityTypeConfiguration<Drug>
    {
        // <summary>
        /// Initializes a new instance of the Staffmember class.
        /// </summary>
        /// 

        public DrugConfiguration()
        {

            Property(x => x.Drug_name).IsRequired().HasMaxLength(60);
            Property(x => x.Drug_Description).HasMaxLength(90);
            Property(x => x.Drug_Other_details).HasMaxLength(120);

        }

    }
}
