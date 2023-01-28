using HospitalManagement.Entities;
using HospitalManagement.Entities.Enums;
using System;
using System.Data.Entity;

namespace HospitalManagement.EF
{
    /// <summary>
    /// A custom implementation of HospitalManagementContext that creates a new contact person with address details.
    /// </summary>
    public class MyDropCreateDatabaseIfModelChanges : DropCreateDatabaseIfModelChanges<HospitalManagementContext>
  {
    /// <summary>
    /// Creates a new Patient and Staff.
    /// </summary>
    /// <param name="context">The context to which the new seed data is added.</param>
        protected override void Seed(HospitalManagementContext context)
    {
            var patient = new Patient
            {

            Patient_first_name = "John",            
            Patient_last_name = "Down",
            Outpatient_yn = "XXX",
            Hospital_Number = "QVBH254",
            nhs_number="BMX2548XCV",
            Gender = Gender.Male,
            Height = "190",
            Weight = "90",
            Next_of_kin = "",
            Home_phone = "098936Y4647",
            Work_phone = "09854327",
            Cell_Mobile_phone = "367895432",
            Date_Of_Birth= new DateTime(1988,6,12),
            Other_patient_details = "The context to which the new seed data is added.",

        };
         context.Patients.Add(patient);

            var staff = new Staff
            {

                Staff_first_name="Etienne",
                EmailAddressText="etngandu@mail.et",
                Staff_last_name="Ngandu",
                Staff_Category_code=Staff_Category_Code.Doctors_medical_staff,
                Staff_job_title=Staff_JobTitle.residents,               
                Staff_birth_date = new DateTime(1982, 8, 13),
                Gender=Gender.Male,
                ImagePath="imagepath",
                Other_staff_details= "A custom implementation of ContactManagerContext that creates a new contact person with address details."


            };
          context.Staffs.Add(staff);
        }







        //private static Address CreateAddress()
        //{
        //    return new Address("Street", "City", "ZipCode", "Country", ContactType.Business);
        //}
    }
}
