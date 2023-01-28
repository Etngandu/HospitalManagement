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
 
  public  class StaffConfiguration: EntityTypeConfiguration<Staff>
    {
        // <summary>
        /// Initializes a new instance of the Staffmember class.
        /// </summary>
        /// 

      public StaffConfiguration()
      {

          Property(x=> x.Staff_Category_code).HasColumnName("Staff_Category_code");
          Property(x=> x.Gender).HasColumnName("Gender");
          Property(x => x.Staff_job_title).HasColumnName("Staff_job_title");
          Property(x => x.Staff_first_name).IsRequired().HasMaxLength(25);
          Property(x => x.EmailAddressText).HasMaxLength(50);
          Property(x => x.Staff_last_name).IsRequired().HasMaxLength(40);             
          Property(x => x.Other_staff_details).HasMaxLength(250);    

      }

    }
}
