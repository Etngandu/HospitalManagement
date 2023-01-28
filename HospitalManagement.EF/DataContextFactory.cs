using HospitalManagement.Infrastructure.DataContextStorage; 


namespace HospitalManagement.EF
{
  /// <summary>
    /// Manages instances of the DbHospitalManagementContext and stores them in an appropriate storage container.
  /// </summary>
  public static class DataContextFactory
  {
        /// <summary>
        /// Clears out the current DbHospitalManagementContext.
        /// </summary>
        public static void Clear()
    {
        var dataContextStorageContainer = DataContextStorageFactory<HospitalManagementContext>.CreateStorageContainer();
          dataContextStorageContainer.Clear();
    }

        /// <summary>
        /// Retrieves an instance of HospitalManagementContext from the appropriate storage container or
        /// creates a new instance and stores that in a container.
        /// </summary>
        /// <returns>An instance of ContactManagerContext.</returns>
        public static HospitalManagementContext GetDataContext()
    {
        var dataContextStorageContainer = DataContextStorageFactory<HospitalManagementContext>.CreateStorageContainer();
        var DbHospitalManagementContext = dataContextStorageContainer.GetDataContext();

        if (DbHospitalManagementContext == null)
      {
                DbHospitalManagementContext = new HospitalManagementContext();
          dataContextStorageContainer.Store(DbHospitalManagementContext);
      }
        return DbHospitalManagementContext;
    }
  }
}
