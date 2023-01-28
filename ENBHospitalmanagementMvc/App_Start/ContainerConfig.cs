using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HospitalManagement.Infrastructure;
using HospitalManagement.Entities;
using HospitalManagement.Entities.Repositories;
using AutoMapper;
using ENBHospitalmanagementMvc.App_Start;
using System.Web.Mvc;
using Autofac.Integration.Mvc;
using System.Reflection;
using HospitalManagement.EF;
using HospitalManagement.EF.Repositories;

namespace ENBHospitalmanagementMvc.App_Start
{
    public static class ContainerConfig
    {
        public static void Configure()
        {
            var builder = new ContainerBuilder();

           // builder.RegisterControllers(Assembly.GetExecutingAssembly())‌​;
            builder.RegisterType<PatientsRepository>().As<IPatientsRepository>();
            builder.RegisterType<StaffRepository>().As<IStaffRepository>();
            builder.RegisterType<WardRepository>().As<IWardRepository>();
            builder.RegisterType<DrugRepository>().As<IDrugRepository>();
            builder.RegisterType<EFUnitOfWorkFactory>().As<IUnitOfWorkFactory>();
          
            //  builder.

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<HospitalManagementProfile>();
                              
            });

            var dataAccess = Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(dataAccess);
            //.Where(t => t.Name.EndsWith("Repository"))
            //.AsImplementedInterfaces();


            //Create a mapper that will be used by the DI container
            var mapper = config.CreateMapper();
            builder.RegisterInstance(mapper).As<IMapper>();

            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));


        }
                
    }
}