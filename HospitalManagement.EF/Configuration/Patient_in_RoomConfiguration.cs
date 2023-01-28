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
    /// Configures the behavior for a Patient_in_Room in the model and the database.
    /// </summary>
    /// 
    public class Patient_in_RoomConfiguration:EntityTypeConfiguration<Patient_Room>
    {

        // <summary>
        /// Initializes a new instance of the Patient_in_Room class.
        /// </summary>
        /// 

        public Patient_in_RoomConfiguration()
        {
            Property(x => x.Room_Name).HasMaxLength(60);

        }
    }
}
