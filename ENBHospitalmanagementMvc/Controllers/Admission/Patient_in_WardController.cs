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
    public class Patient_in_WardController : Controller
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
        public Patient_in_WardController(IPatientsRepository patientsRepository, IStaffRepository staffRepository,
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
           // ViewBag.Ward_Id = ward_Id;

            var patient = _patientsRepository.FindById(patient_Id, x => x.Patient_In_Wards);
            //var pat_ward = patient.Patient_In_Wards.Where(x => x.Owner_WardId == ward_Id);


            ViewBag.Message = patient.FullName;
            return View();
        }

        public ActionResult GetListPatient_in_Ward(int patient_Id)
        {
            var patient = _patientsRepository.FindById(patient_Id, x => x.Patient_In_Wards);
            var past_wrd = patient.Patient_In_Wards
                        .Join(_wardRepository.FindAll(),
                         ptwrd => ptwrd.Owner_WardId,
                         wrd => wrd.Id,
                         (stud, stand) => new DisplayPatient_in_Ward
                         {
                             Id=stud.Id,
                             Date_from=stud.Date_from,
                             Date_to=stud.Date_to,
                             Wardname=stand.Ward_Name,
                             Ward_Id=stand.Id
                         }

                         );

            ViewBag.Message = patient.FullName;

            var Mpdata = new List<DisplayPatient_in_Ward>();

            _imapper.Map(past_wrd, Mpdata);

            return Json(new { data = Mpdata }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Create(int patient_Id)
        {
            ViewBag.Patient_Id = patient_Id;            
            var createAndEditPatient_In_Ward = new CreateAndEditPatient_in_Ward()
            {
                
                ListWard = _wardRepository.FindAll()                             
                        .Select(d => new SelectListItem
                        {
                            Text = d.Ward_Name ,
                            Value = d.Id.ToString(),
                            Selected = true

                        }).Distinct().ToList()    

            };


            return View(createAndEditPatient_In_Ward);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateAndEditPatient_in_Ward createAndEditPatient_In_Ward, int patient_Id)
        {
            ViewBag.Patient_Id = patient_Id;
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                try
                {
                    using (_unitOfWorkFactory.Create())
                    {
                        var patient = _patientsRepository.FindById(patient_Id);
                        var patient_In_Ward = new Patient_in_Ward();

                        ViewBag.Message = patient.FullName;

                        _imapper.Map(createAndEditPatient_In_Ward, patient_In_Ward);
                        patient.Patient_In_Wards.Add(patient_In_Ward);

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

            var patient = _patientsRepository.FindById(patient_Id, x => x.Patient_In_Wards);
            if (patient == null)
            {
                return HttpNotFound();
            }
            var data = new CreateAndEditPatient_in_Ward()
            {
                ListWard = _wardRepository.FindAll()
                        .Select(d => new SelectListItem
                        {
                            Text = d.Ward_Name,
                            Value = d.Id.ToString(),
                            Selected = true

                        }).Distinct().ToList()

            };
            _imapper.Map(patient.Patient_In_Wards.Single(x => x.Id == id), data);
            return View(data);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit(CreateAndEditPatient_in_Ward createAndEditPatient_in_ward, int patient_Id)
        {
            ViewBag.Patient_Id = patient_Id;

            if (ModelState.IsValid)
            {
                try
                {
                    using (_unitOfWorkFactory.Create())
                    {
                        var patient = _patientsRepository.FindById(patient_Id, x => x.Patient_In_Wards);
                        var pt_wrd = patient.Patient_In_Wards.Single(x => x.Id == createAndEditPatient_in_ward.Id);
                        _imapper.Map(createAndEditPatient_in_ward, pt_wrd);
                        // return RedirectToAction("List", new { createAndEditEmailAddress.OperatorId });
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

                var patient = _patientsRepository.FindById(patient_Id, x => x.Patient_In_Wards);
                var pt_wrd = patient.Patient_In_Wards
                               .Join(_wardRepository.FindAll(),
                                 ptw => ptw.Owner_WardId,
                                 wrd => wrd.Id,
                                (stud, stand) => new DisplayPatient_in_Ward
                                {
                                    Id = stud.Id,
                                    Patient_Id = (int)stud.Owner_PatientId,
                                    Date_from = stud.Date_from,
                                    Date_to = stud.Date_to,
                                    Wardname=stand.Ward_Name

                                }) ;


                var data = new DisplayPatient_in_Ward();
                var wrdsgle = pt_wrd.Single(x => x.Id == id);

                ViewBag.Message = patient.FullName;
                ViewBag.Ward = wrdsgle.Wardname;

                _imapper.Map(patient.Patient_In_Wards.Single(x => x.Id == id), data);

                return View(data);
            
        }

        [HttpPost, ActionName("Delete")]
        // [ValidateAntiForgeryToken]
        // [HttpPost]
        public ActionResult Delete(DisplayPatient_in_Ward displayPatient_In_ward, int Patient_Id)
        {
            ViewBag.Patient_Id = Patient_Id;

            using (_unitOfWorkFactory.Create())
            {
                var patient = _patientsRepository.FindById(Patient_Id, x => x.Patient_In_Wards);
                var pt_wrd = patient.Patient_In_Wards.Single(x => x.Id == displayPatient_In_ward.Id);
                patient.Patient_In_Wards.Remove(pt_wrd);
                return Json(new { success = true, message = "Removed Successfully" }, JsonRequestBehavior.AllowGet);
            }

        }
    }
}