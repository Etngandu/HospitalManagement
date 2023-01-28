using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using HospitalManagement.Infrastructure;
using HospitalManagement.Entities;
using ENBHospitalmanagementMvc.Models;
using HospitalManagement.EF;
using HospitalManagement.Entities.Repositories;
using HospitalManagement.Entities.Enums;

namespace ENBHospitalmanagementMvc.Controllers.Admission
{
    public class Patient_in_RoomController : Controller
    {
        // GET: Case

        private readonly IPatientsRepository _patientsRepository;
        private readonly IStaffRepository _staffRepository;
        private readonly IWardRepository _wardRepository;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly IMapper _imapper;
        const int PageSize = 10;


        /// <summary>
        /// Initializes a new instance of the CaseController class.
        /// </summary>
        public Patient_in_RoomController(IPatientsRepository patientsRepository, IStaffRepository staffRepository,
                  IWardRepository wardRepository, IUnitOfWorkFactory unitOfWorkFactory, IMapper imapper)
        {
            _patientsRepository = patientsRepository;
            _staffRepository = staffRepository;
            _wardRepository = wardRepository;
            _unitOfWorkFactory = unitOfWorkFactory;
            _imapper = imapper;
        }


        public ActionResult List(int patient_Id)
        {
            ViewBag.Patient_Id = patient_Id;
           

            var patient = _patientsRepository.FindById(patient_Id, x => x.Patient_Rooms);
            


            ViewBag.Message = patient.FullName;
            return View();
        }

        public ActionResult GetListPatient_in_Room(int patient_Id)
        {
            var patient = _patientsRepository.FindById(patient_Id, x => x.Patient_Rooms);
            var past_rms = patient.Patient_Rooms;
                        

            ViewBag.Message = patient.FullName;

            var Mpdata = new List<DisplayPatient_Room>();

            _imapper.Map(past_rms, Mpdata);

            return Json(new { data = Mpdata }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Create(int patient_Id)
        {
            ViewBag.Patient_Id = patient_Id;

           // var createAndEditPatient_Room = new CreateAndEditPatient_Room();            


            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateAndEditPatient_Room createAndEditPatient_Room, int patient_Id)
        {

            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                try
                {
                    using (_unitOfWorkFactory.Create())
                    {
                        var patient = _patientsRepository.FindById(patient_Id);
                        var patient_Room = new Patient_Room();

                        ViewBag.Message = patient.FullName;

                        _imapper.Map(createAndEditPatient_Room, patient_Room);
                        patient.Patient_Rooms.Add(patient_Room);                       

                    }
                }
                catch (ModelValidationException mvex)
                {
                    foreach (var error in mvex.ValidationErrors)
                    {
                        ModelState.AddModelError(error.MemberNames.FirstOrDefault() ?? "", error.ErrorMessage);
                    }
                    return Json(new { success = false, message = mvex.Message }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { success = true, message = "Saved Successfully" }, JsonRequestBehavior.AllowGet);
            }
            return View();           

        }

        //    public ActionResult Details(int id)
        //    {
        //        Case dbCase = _caseRepository.FindById(id);

        //        ViewBag.Message = dbCase.CaseTitle;

        //        if (dbCase == null)
        //        {
        //            return HttpNotFound();
        //        }

        //        var data = _imapper.Map<DisplayCase>(dbCase);

        //        return View(data);
        //    }


        public ActionResult Edit(int id, int patient_Id)
        {
            ViewBag.Patient_Id = patient_Id;
            // ViewBag.BedId = id;

            var patient = _patientsRepository.FindById(patient_Id, x => x.Patient_Rooms);
            if (patient == null)
            {
                return HttpNotFound();
            }
            var data = new CreateAndEditPatient_Room();
            
            _imapper.Map(patient.Patient_Rooms.Single(x => x.Id == id), data);
            return View(data);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit(CreateAndEditPatient_Room createAndEditPatient_Room, int patient_Id)
        {
            ViewBag.Patient_Id = patient_Id;

            if (ModelState.IsValid)
            {
                try
                {
                    using (_unitOfWorkFactory.Create())
                    {
                        var patient = _patientsRepository.FindById(patient_Id, x => x.Patient_Rooms);
                        var pt_rms = patient.Patient_Rooms.Single(x => x.Id == createAndEditPatient_Room.Id);
                        _imapper.Map(createAndEditPatient_Room, pt_rms);                        
                    }
                }
                catch (ModelValidationException mvex)
                {
                    foreach (var error in mvex.ValidationErrors)
                    {
                        ModelState.AddModelError(error.MemberNames.FirstOrDefault() ?? "", error.ErrorMessage);
                    }
                    return Json(new { success = false, message = mvex.Message }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { success = true, message = "Saved Successfully" }, JsonRequestBehavior.AllowGet);
            }
            return View();
        }


        public ActionResult Delete(int id, int patient_Id)
        {


            using (_unitOfWorkFactory.Create())
            {

                var patient = _patientsRepository.FindById(patient_Id, x => x.Patient_Rooms); 

                var data = new DisplayPatient_Room();               

                ViewBag.Message = patient.FullName;
               

                _imapper.Map(patient.Patient_Rooms.Single(x => x.Id == id), data);

                return View(data);
            }
        }

        [HttpPost, ActionName("Delete")]
        // [ValidateAntiForgeryToken]
        // [HttpPost]
        public ActionResult Delete(DisplayPatient_Room displayPatient_Room, int patient_Id)
        {
            ViewBag.Patient_Id = patient_Id;

            using (_unitOfWorkFactory.Create())
            {
                var patient = _patientsRepository.FindById(patient_Id, x => x.Patient_Rooms);
                var pt_rms = patient.Patient_Rooms.Single(x => x.Id == displayPatient_Room.Id);
                patient.Patient_Rooms.Remove(pt_rms);
                return Json(new { success = true, message = "Removed Successfully" }, JsonRequestBehavior.AllowGet);
            }

        }
    }
}