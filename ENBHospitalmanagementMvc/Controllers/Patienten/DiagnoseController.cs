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

namespace ENBHospitalmanagementMvc.Controllers.Patienten
{
    public class DiagnoseController : Controller
    {
        // GET: Case

        private readonly IPatientsRepository _patientsRepository;
        private readonly IStaffRepository _staffRepository;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly IMapper _imapper;
        const int PageSize = 10;


        /// <summary>
        /// Initializes a new instance of the CaseController class.
        /// </summary>
        public DiagnoseController(IPatientsRepository patientsRepository,IStaffRepository staffRepository, IUnitOfWorkFactory unitOfWorkFactory, IMapper imapper)
        {
            _patientsRepository = patientsRepository;
            _staffRepository = staffRepository;
            _unitOfWorkFactory = unitOfWorkFactory;
            _imapper = imapper;
        }


        public ActionResult List(int Id)
        {
            ViewBag.Patient_Id = Id;

            var Patientdiags = _patientsRepository.FindById(Id, x => x.Diagnoses);

            ViewBag.Message = Patientdiags.FullName;
            return View();
        }

        public ActionResult GetListDiag(int Id)
        {
            var Patientdiags = _patientsRepository.FindById(Id, x => x.Diagnoses);

            ViewBag.Message = Patientdiags.FullName;

            var Mpdata = new List<DisplayDiagnose>();

            _imapper.Map(Patientdiags.Diagnoses, Mpdata);

            return Json(new { data = Mpdata }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Create(int Patient_Id)
        {
            ViewBag.Patient_Id = Patient_Id;
            var createAndEditDiagnose = new CreateAndEditDiagnose()
            {

                ListStaff = _staffRepository.FindAll()
                        .Select(d => new SelectListItem
                        {
                            Text = d.Staff_first_name + " " + d.Staff_last_name,
                            Value = d.Id.ToString(),
                            Selected=false

                        }).Distinct().ToList()

                        };       
            
            
            return View(createAndEditDiagnose);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateAndEditDiagnose createAndEditDiagnose, int Patient_Id)
        {

            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                try
                {
                    using (_unitOfWorkFactory.Create())
                    {
                        var patient = _patientsRepository.FindById(Patient_Id);
                        var diagnose = new Diagnose();

                        ViewBag.Message = patient.FullName;

                        _imapper.Map(createAndEditDiagnose, diagnose);
                        patient.Diagnoses.Add(diagnose);
                       
                    }
                }
                catch (ModelValidationException mvex)
                {
                    foreach (var error in mvex.ValidationErrors)
                    {
                        ModelState.AddModelError(error.MemberNames.FirstOrDefault() ?? "", error.ErrorMessage);
                    }
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


        public ActionResult Edit(int id, int Patient_Id)
        {
            ViewBag.Patient_Id = Patient_Id;
           // ViewBag.BedId = id;

            var patient = _patientsRepository.FindById(Patient_Id, x => x.Diagnoses);
            if (patient == null)
            {
                return HttpNotFound();
            }
            var data = new CreateAndEditDiagnose()
            {
                ListStaff = _staffRepository.FindAll()
                        .Select(d => new SelectListItem
                        {
                            Text = d.Staff_first_name + " " + d.Staff_last_name,
                            Value = d.Id.ToString(),
                            Selected = true

                        }).Distinct().ToList()

            };
            _imapper.Map(patient.Diagnoses.Single(x => x.Id == id), data);
            return View(data);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit(CreateAndEditDiagnose createAndEditDiagnose, int Patient_Id)
        {
            ViewBag.Patient_Id = Patient_Id;

            if (ModelState.IsValid)
            {
                try
                {
                    using (_unitOfWorkFactory.Create())
                    {
                        var patient = _patientsRepository.FindById(Patient_Id, x => x.Diagnoses);
                        var diag = patient.Diagnoses.Single(x => x.Id == createAndEditDiagnose.Id);
                        _imapper.Map(createAndEditDiagnose, diag);
                        // return RedirectToAction("List", new { createAndEditEmailAddress.OperatorId });
                    }
                }
                catch (ModelValidationException mvex)
                {
                    foreach (var error in mvex.ValidationErrors)
                    {
                        ModelState.AddModelError(error.MemberNames.FirstOrDefault() ?? "", error.ErrorMessage);
                    }
                }
                return Json(new { success = true, message = "Saved Successfully" }, JsonRequestBehavior.AllowGet);
            }
            return View();
        }


        public ActionResult Delete(int id, int Patient_Id)
        {


            using (_unitOfWorkFactory.Create())
            {

                var patient = _patientsRepository.FindById(Patient_Id, x => x.Diagnoses);
                var data = new DisplayDiagnose();
               
                _imapper.Map(patient.Diagnoses.Single(x => x.Id == id), data);
                
                return View(data);
            }
        }

        [HttpPost, ActionName("Delete")]
        // [ValidateAntiForgeryToken]
        // [HttpPost]
        public ActionResult Delete(DisplayBed displayBed, int Patient_Id)
        {
            ViewBag.Patient_Id = Patient_Id;

            using (_unitOfWorkFactory.Create())
            {
                var patient = _patientsRepository.FindById(Patient_Id, x => x.Diagnoses);
                var diag = patient.Diagnoses.Single(x => x.Id == displayBed.Id);
                patient.Diagnoses.Remove(diag);
                return Json(new { success = true, message = "Removed Successfully" }, JsonRequestBehavior.AllowGet);
            }

        }
    }
}