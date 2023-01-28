using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using HospitalManagement.Entities;
using ENBHospitalmanagementMvc.Models;

namespace ENBHospitalmanagementMvc.App_Start
{
    public class HospitalManagementProfile: Profile
    {

        public HospitalManagementProfile()
        {
            #region Patient 
            CreateMap<Patient, DisplayPatient>();

            CreateMap<CreateAndEditPatient, Patient>()
              .ForMember(d => d.DateCreated, t => t.Ignore())
              .ForMember(d => d.DateModified, t => t.Ignore());
            CreateMap<Patient, CreateAndEditPatient>();
            #endregion            

            #region Staff 
            CreateMap<Staff, DisplayStaff>();

            CreateMap<CreateAndEditStaff, Staff>()
              .ForMember(d => d.DateCreated, t => t.Ignore())
              .ForMember(d => d.DateModified, t => t.Ignore());
            CreateMap<Staff, CreateAndEditStaff>();
            #endregion

            #region Ward 
            CreateMap<Ward, DisplayWard>();

            CreateMap<CreateAndEditWard, Ward>();
              //.ForMember(d => d.DateCreated, t => t.Ignore())
              //.ForMember(d => d.DateModified, t => t.Ignore());
            CreateMap<Ward, CreateAndEditWard>();
            #endregion


            #region Bed 
            CreateMap<Bed, DisplayBed>()
             .ForMember(d => d.WardId, t => t.MapFrom(y => y.OwnerId));
            CreateMap<Bed, CreateAndEditBed>()
             .ForMember(d => d.WardId, t => t.MapFrom(y => y.OwnerId));
            CreateMap<CreateAndEditBed, Bed>()
            .ForMember(d => d.OwnerId, t => t.Ignore())
            .ForMember(d => d.Owner, t => t.Ignore());


            #endregion

            #region Drug 
            CreateMap<Drug, DisplayDrug>();

            CreateMap<CreateAndEditDrug, Drug>();
            //.ForMember(d => d.DateCreated, t => t.Ignore())
            //.ForMember(d => d.DateModified, t => t.Ignore());
            CreateMap<Drug, CreateAndEditDrug>();
            #endregion

            #region Diagnose 
            CreateMap<Diagnose, DisplayDiagnose>()
            .ForMember(d => d.Patient_Id, t => t.MapFrom(y => y.Owner_PatientId))
            .ForMember(d => d.Staff_Id, t => t.MapFrom(y => y.Owner_StaffId));
            CreateMap<CreateAndEditDiagnose, Diagnose>()
              .ForMember(d => d.DateCreated, t => t.Ignore())
              .ForMember(d => d.DateModified, t => t.Ignore())
              .ForMember(d => d.Owner_PatientId, t => t.MapFrom(y => y.Patient_Id))
              .ForMember(d => d.Owner_StaffId, t => t.MapFrom(y => y.Staff_Id))
              .ReverseMap() ;
            //CreateMap<Diagnose, CreateAndEditDiagnose>();
            //.ForMember(d => d.Patient_Id, t => t.MapFrom(y => y.Owner_PatientId))
            //.ForMember(d => d.Staff_Id, t => t.MapFrom(y => y.Owner_StaffId))
            //.ReverseMap();
            #endregion

            #region Patient_Drug_Treatment 
            CreateMap<Patient_Drug_Treatment, DisplayPatient_Drug_Treatment>()
            .ForMember(d => d.PatientId, t => t.MapFrom(y => y.Owner_PatientId))
            .ForMember(d => d.DiagnoseId, t => t.MapFrom(y => y.Owner_DiagnoseId))
            .ForMember(d => d.DrugId, t => t.MapFrom(y => y.Owner_DrugId));
            CreateMap<CreateAndEditPatient_Drug_Treatment, Patient_Drug_Treatment>()
              .ForMember(d => d.DateCreated, t => t.Ignore())
              .ForMember(d => d.DateModified, t => t.Ignore())
              .ForMember(d => d.Owner_PatientId, t => t.MapFrom(y => y.PatientId))
              .ForMember(d => d.Owner_DiagnoseId, t => t.MapFrom(y => y.DiagnoseId))
              .ForMember(d => d.Owner_DrugId, t => t.MapFrom(y => y.DrugId))
              .ForMember(d=>d.Owner_Drug,t=>t.Ignore())
              .ForMember(d => d.Owner_Diagnose, t => t.Ignore())
              .ReverseMap();
            CreateMap<DisplayPatient_Drug_Treatment, Patient_Drug_Treatment>()
            .ForMember(d => d.Owner_Diagnose, t => t.Ignore())
            .ForMember(d => d.Owner_Drug, t => t.Ignore());
            #endregion

            #region Patient_in_Bed 
            CreateMap<Patient_in_Bed, DisplayPatient_in_Bed>()
            .ForMember(d => d.Patient_Id, t => t.MapFrom(y => y.Owner_PatientId))
            .ForMember(d => d.Ward_Id, t => t.MapFrom(y => y.Owner_WardId))
            .ForMember(d => d.Bed_Id, t => t.MapFrom(y => y.Owner_BedId));
            CreateMap<CreateAndEditPatient_in_Bed, Patient_in_Bed>()              
              .ForMember(d => d.Owner_PatientId, t => t.MapFrom(y => y.Patient_Id))
              .ForMember(d => d.Owner_BedId, t => t.MapFrom(y => y.Bed_Id))
              .ForMember(d => d.Owner_WardId, t => t.MapFrom(y => y.Ward_Id))
              .ForMember(d => d.Owner_Bed, t => t.Ignore())
              .ForMember(d => d.Owner_Patient, t => t.Ignore())
              .ReverseMap();
            CreateMap<DisplayPatient_in_Bed, Patient_in_Bed>()
            .ForMember(d => d.Owner_Patient, t => t.Ignore())
            .ForMember(d => d.Owner_Bed, t => t.Ignore())
            .ForMember(d => d.Owner_Ward, t => t.Ignore());
            #endregion

            #region Doctor_Assigned_to_Patient 
            CreateMap<Doctor_Assigned_to_Patient, DisplayDoctor_Assigned_to_Patient>()
            .ForMember(d => d.Patient_Id, t => t.MapFrom(y => y.Owner_PatientId))
            .ForMember(d => d.Staff_Id, t => t.MapFrom(y => y.Owner_StaffId));
            CreateMap<CreateAndEditDoctor_Assigned_to_Patient, Doctor_Assigned_to_Patient>()              
              .ForMember(d => d.Owner_PatientId, t => t.MapFrom(y => y.Patient_Id))
              .ForMember(d => d.Owner_StaffId, t => t.MapFrom(y => y.Staff_Id))
              .ForMember(d=>d.Owner_Staff,t=>t.Ignore())
              .ReverseMap();
            CreateMap<DisplayDoctor_Assigned_to_Patient, Doctor_Assigned_to_Patient>()
            .ForMember(d => d.Owner_Patient, t => t.Ignore())
            .ForMember(d => d.Owner_Staff, t => t.Ignore());
            #endregion

            #region Patient_in_Ward 
            CreateMap<Patient_in_Ward, DisplayPatient_in_Ward>()
            .ForMember(d => d.Patient_Id, t => t.MapFrom(y => y.Owner_PatientId))
            .ForMember(d => d.Ward_Id, t => t.MapFrom(y => y.Owner_WardId));
            CreateMap<CreateAndEditPatient_in_Ward, Patient_in_Ward>()
              .ForMember(d => d.Owner_PatientId, t => t.MapFrom(y => y.Patient_Id))
              .ForMember(d => d.Owner_WardId, t => t.MapFrom(y => y.Ward_Id))             
              .ReverseMap();
            CreateMap<DisplayPatient_in_Ward, Patient_in_Ward>()
            .ForMember(d => d.Owner_Patient, t => t.Ignore())
            .ForMember(d => d.Owner_Ward, t => t.Ignore());
            #endregion

            #region Patien_inRoom 
            CreateMap<Patient_Room, DisplayPatient_Room>()
            .ForMember(d => d.Patient_Id, t => t.MapFrom(y => y.Owner_PatientId))
            .ForMember(d => d.Id, t => t.MapFrom(y => y.Id));
            CreateMap<CreateAndEditPatient_Room, Patient_Room>()              
              .ForMember(d => d.Owner_PatientId, t => t.MapFrom(y => y.Patient_Id))            
              .ReverseMap();
            //CreateMap<Patient_Room, CreateAndEditPatient_Room>()
            //.ForMember(d => d.Patient_Id, t => t.MapFrom(y => y.Owner_PatientId));           
            #endregion


        }
    }
}