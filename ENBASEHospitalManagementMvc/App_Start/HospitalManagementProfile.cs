using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using HospitalManagement.Entities;
using ENBASEHospitalManagementMvc.Models;


namespace ENBASEHospitalManagementMvc.App_Start
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


        }
    }
}