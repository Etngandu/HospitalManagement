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
    public class Doctor_Assigned_to_PatientController : Controller
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
        public Doctor_Assigned_to_PatientController(IPatientsRepository patientsRepository, IStaffRepository staffRepository, IUnitOfWorkFactory unitOfWorkFactory, IMapper imapper)
        {
            _patientsRepository = patientsRepository;
            _staffRepository = staffRepository;
            _unitOfWorkFactory = unitOfWorkFactory;
            _imapper = imapper;
        }


        public ActionResult List(int Id)
        {
            ViewBag.Patient_Id = Id;

            var Patientdiags = _patientsRepository.FindById(Id, x => x.Doctor_Assigned_To_Patients);
                               
                               
            ViewBag.Message = Patientdiags.FullName;
            return View();
        }

        public ActionResult GetListDoc_Ass_to(int Id)
        {
            var Patientdr_ass = _patientsRepository.FindById(Id, x => x.Doctor_Assigned_To_Patients);
            var dr_ass=Patientdr_ass.Doctor_Assigned_To_Patients
                               .Join(_staffRepository.FindAll(),
                                drAss => drAss.Owner_StaffId,
                                stf => stf.Id,
                                (stud, stand) => new DisplayDoctor_Assigned_to_Patient
                                {
                                    Id = stud.Id,
                                    Patient_Id = (int)stud.Owner_PatientId,
                                    Date_Ass_from = stud.Date_Ass_from,
                                    Date_Ass_to = (DateTime)stud.Date_Ass_to,
                                    StaffName = stand.FullName

                                }); 
                               

            ViewBag.Message = Patientdr_ass.FullName;

            var Mpdata = new List<DisplayDoctor_Assigned_to_Patient>();

            _imapper.Map(dr_ass, Mpdata);

            return Json(new { data = Mpdata }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Create(int Patient_Id)
        {
            ViewBag.Patient_Id = Patient_Id;

            var createAndEditDoctor_Assigned_To_Patient = new CreateAndEditDoctor_Assigned_to_Patient()
            {

                ListStaff = _staffRepository.FindAll()
                             .Where(x=>x.Staff_Category_code==Staff_Category_Code.Doctors_medical_staff)
                        .Select(d => new SelectListItem
                        {
                            Text = d.Staff_first_name + " " + d.Staff_last_name,
                            Value = d.Id.ToString(),
                            Selected = false

                        }).Distinct().ToList()

            };


            return View(createAndEditDoctor_Assigned_To_Patient);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateAndEditDoctor_Assigned_to_Patient createAndEditDoctor_Assigned_To_Patient, int Patient_Id)
        {

            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                try
                {
                    using (_unitOfWorkFactory.Create())
                    {
                        var patient = _patientsRepository.FindById(Patient_Id);
                        var doctor_Assigned_To_Patient = new Doctor_Assigned_to_Patient();

                        ViewBag.Message = patient.FullName;

                        _imapper.Map(createAndEditDoctor_Assigned_To_Patient, doctor_Assigned_To_Patient);
                        patient.Doctor_Assigned_To_Patients.Add(doctor_Assigned_To_Patient);

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

            var patient = _patientsRepository.FindById(patient_Id, x => x.Doctor_Assigned_To_Patients);
            if (patient == null)
            {
                return HttpNotFound();
            }
            var data = new CreateAndEditDoctor_Assigned_to_Patient()
            {
                ListStaff = _staffRepository.FindAll()
                           .Where(x => x.Staff_Category_code == Staff_Category_Code.Doctors_medical_staff)
                        .Select(d => new SelectListItem
                        {
                            Text = d.Staff_first_name + " " + d.Staff_last_name,
                            Value = d.Id.ToString(),
                            Selected = true

                        }).Distinct().ToList()

            };
                _imapper.Map(patient.Doctor_Assigned_To_Patients.Single(x => x.Id == id), data);
            return View(data);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit(CreateAndEditDoctor_Assigned_to_Patient createAndEditDoctor_Assigned_To_Patient, int patient_Id)
        {
            ViewBag.Patient_Id = patient_Id;

            if (ModelState.IsValid)
            {
                try
                {
                    using (_unitOfWorkFactory.Create())
                    {
                        var patient = _patientsRepository.FindById(patient_Id, x => x.Doctor_Assigned_To_Patients);
                        var dc_Ass = patient.Doctor_Assigned_To_Patients.Single(x => x.Id == createAndEditDoctor_Assigned_To_Patient.Id);
                        _imapper.Map(createAndEditDoctor_Assigned_To_Patient, dc_Ass);
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


            using (_unitOfWorkFactory.Create())
            {

                var patient = _patientsRepository.FindById(patient_Id, x => x.Doctor_Assigned_To_Patients);
                var dr_ass = patient.Doctor_Assigned_To_Patients
                               .Join(_staffRepository.FindAll(),
                                drAss => drAss.Owner_StaffId,
                                stf => stf.Id,
                                (stud, stand) => new DisplayDoctor_Assigned_to_Patient
                                {
                                    Id = stud.Id,
                                    Patient_Id = (int)stud.Owner_PatientId,
                                    Date_Ass_from = stud.Date_Ass_from,
                                    Date_Ass_to = (DateTime)stud.Date_Ass_to,
                                    StaffName = stand.FullName

                                });


                var data = new DisplayDoctor_Assigned_to_Patient();
                var staffsgle= dr_ass.Single(x => x.Id == id);

                ViewBag.Message = patient.FullName;
                ViewBag.Staff = staffsgle.StaffName;

                _imapper.Map(patient.Doctor_Assigned_To_Patients.Single(x => x.Id == id), data);

                return View(data);
            }
        }

        [HttpPost, ActionName("Delete")]
        // [ValidateAntiForgeryToken]
        // [HttpPost]
        public ActionResult Delete(DisplayDoctor_Assigned_to_Patient displayDoctor_Assigned_to_patient, int Patient_Id)
        {
            ViewBag.Patient_Id = Patient_Id;

            using (_unitOfWorkFactory.Create())
            {
                var patient = _patientsRepository.FindById(Patient_Id, x => x.Doctor_Assigned_To_Patients);
                var dr_Ass = patient.Doctor_Assigned_To_Patients.Single(x => x.Id == displayDoctor_Assigned_to_patient.Id);
                patient.Doctor_Assigned_To_Patients.Remove(dr_Ass);
                return Json(new { success = true, message = "Removed Successfully" }, JsonRequestBehavior.AllowGet);
            }

        }
    }
}