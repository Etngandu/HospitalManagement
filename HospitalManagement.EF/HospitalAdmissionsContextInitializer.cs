using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace HospitalManagement.EF
{
    /// <summary>
    /// Used to initialize the HospitalManagementContext.
    /// </summary>
   public static class HospitalManagementContextInitializer
    {
        /// <summary>
        /// Sets the IDatabaseInitializer for the application.
        /// </summary>
        /// <param name="dropDatabaseIfModelChanges">When true, uses the MyDropCreateDatabaseIfModelChanges to recreate the database when necessary.
        /// Otherwise, database initialization is disabled by passing null to the SetInitializer method.
        /// </param>

        public static void Init(bool dropDatabaseIfModelChanges)
        {
            if (dropDatabaseIfModelChanges)
            {
                Database.SetInitializer(new MyDropCreateDatabaseIfModelChanges());
                using (var db = new HospitalManagementContext())
                {
                    db.Database.Initialize(false);
                }
            }
            else
            {
                Database.SetInitializer<HospitalManagementContext>(null);
            }
        }
    }
}
