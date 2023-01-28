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
    public class Patient_Drug_TreatmentController : Controller
    {
        // GET: Case

        private readonly IPatientsRepository _patientsRepository;
        private readonly IDrugRepository _drugRepository;
        private readonly IStaffRepository _staffRepository;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly IMapper _imapper;
        const int PageSize = 10;


        /// <summary>
        /// Initializes a new instance of the CaseController class.
        /// </summary>
        public Patient_Drug_TreatmentController(IPatientsRepository patientsRepository, IStaffRepository staffRepository, 
                                      IDrugRepository drugRepository ,IUnitOfWorkFactory unitOfWorkFactory, IMapper imapper)
        {
            _patientsRepository = patientsRepository;
            _staffRepository = staffRepository;
            _drugRepository = drugRepository;
            _unitOfWorkFactory = unitOfWorkFactory;
            _imapper = imapper;
        }


        public ActionResult List(int diag_Id, int patient_Id)
        {
            ViewBag.Patient_Id = patient_Id;
            ViewBag.Diag_Id = diag_Id;

            var Patientdiags = _patientsRepository.FindById(patient_Id, x => x.Patient_Drug_Treatments);
            var Pdrtrt = Patientdiags.Patient_Drug_Treatments.Where(x => x.Owner_DiagnoseId == diag_Id);

            var Ptdiag = _patientsRepository.FindById(patient_Id, x => x.Diagnoses);
            var diag = Ptdiag.Diagnoses.Single(x => x.Id == diag_Id);

            ViewBag.Message = Patientdiags.FullName;
            ViewBag.Diagnose = diag.Diagnose_details;
            return View();
        }

        public ActionResult GetListPatDrgTrt(int diag_Id, int patient_Id)
        {
            var Patientdrgt = _patientsRepository.FindById(patient_Id, x => x.Patient_Drug_Treatments);
            var Pdrtrt = Patientdrgt.Patient_Drug_Treatments.Where(x => x.Owner_DiagnoseId == diag_Id)
                         .Join(_drugRepository.FindAll(),
                          ptdrg=>ptdrg.Owner_DrugId,
                          drg=>drg.Id,
                          (stud, stand)=> new DisplayPatient_Drug_Treatment
                          {
                             Id=stud.Id,
                             PatientId=(int)stud.Owner_PatientId,
                             DiagnoseId=stud.Owner_DiagnoseId,
                             DrugId=stud.Owner_DrugId,
                             Dosage_administred=stud.Dosage_administred,
                             DateCreated=stud.DateCreated,
                             DateModified=stud.DateModified,
                             Comments=stud.Comments,
                             DrugName=stand.Drug_name

                          });
                 
           

            ViewBag.Message = Patientdrgt.FullName;

            var Mpdata = new List<DisplayPatient_Drug_Treatment>();

            _imapper.Map(Pdrtrt, Mpdata);

            return Json(new { data = Mpdata }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Create(int patient_Id, int diag_Id)
        {
            ViewBag.Patient_Id = patient_Id;
            ViewBag.Diag_Id = diag_Id;
            var createandEditPatient_Drug_Treatment = new CreateAndEditPatient_Drug_Treatment()
            {

                ListDrug = _drugRepository.FindAll()
                        .Select(d => new SelectListItem
                        {
                            Text = d.Drug_name,
                            Value = d.Id.ToString(),
                            Selected = false

                        }).Distinct().ToList()

            };


            return View(createandEditPatient_Drug_Treatment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateAndEditPatient_Drug_Treatment createAndEditPatient_Drug_Treatment, 
                                                int PatientId, int DiagnoseId)
        {

            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                try
                {
                    using (_unitOfWorkFactory.Create())
                    {
                        var patient = _patientsRepository.FindById(PatientId, x=>x.Diagnoses);
                        var pt_diag = patient.Diagnoses.Single(x => x.Id == DiagnoseId);

                        


                        var patient_Drug_Treatment = new Patient_Drug_Treatment();

                        

                        _imapper.Map(createAndEditPatient_Drug_Treatment, patient_Drug_Treatment);
                        
                        pt_diag.Patient_Drug_Treatments.Add(patient_Drug_Treatment);

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

        public ActionResult Details(int id, int patient_Id, int diag_Id)
        {
            var patient = _patientsRepository.FindById(patient_Id, x => x.Patient_Drug_Treatments);
            var PtdrgTrt = patient.Patient_Drug_Treatments.Where(x => x.Id == diag_Id);

            var Ptdiag = _patientsRepository.FindById(patient_Id, x => x.Diagnoses);
            var diag = Ptdiag.Diagnoses.Single(x => x.Id == diag_Id);

            ViewBag.Message = patient.FullName;
            ViewBag.Diagnose = diag.Diagnose_details;

            if (patient == null)
            {
                return HttpNotFound();
            }

            var data = new DisplayPatient_Drug_Treatment();

            _imapper.Map(PtdrgTrt.Single(x => x.Id == id),data);

            return View(data);
        }


        public ActionResult Edit(int id, int patient_Id, int diag_Id)
        {
            ViewBag.Patient_Id = patient_Id;
            ViewBag.Diag_Id = diag_Id;

            var patient = _patientsRepository.FindById(patient_Id, x => x.Patient_Drug_Treatments);
            var Pdrtrt = patient.Patient_Drug_Treatments.Where(x => x.Owner_DiagnoseId == diag_Id);

            if (patient == null)
            {
                return HttpNotFound();
            }
            var data = new CreateAndEditPatient_Drug_Treatment()
            {

                ListDrug = _drugRepository.FindAll()
                        .Select(d => new SelectListItem
                        {
                            Text = d.Drug_name,
                            Value = d.Id.ToString(),
                            Selected = false

                        }).Distinct().ToList()

            };
            _imapper.Map(Pdrtrt.Single(x => x.Id == id), data);
            return View(data);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit(CreateAndEditPatient_Drug_Treatment createAndEditPatient_Drug_Treatment, int PatientId, int DiagnoseId)
        {
            ViewBag.Patient_Id = PatientId;
            ViewBag.Diag_Id = DiagnoseId;

            if (ModelState.IsValid)
            {
                try
                {
                    using (_unitOfWorkFactory.Create())
                    {
                        var patient = _patientsRepository.FindById(PatientId, x => x.Patient_Drug_Treatments);
                        var patdiag = patient.Patient_Drug_Treatments.Where(x => x.Owner_DiagnoseId == DiagnoseId);

                        _imapper.Map(createAndEditPatient_Drug_Treatment, patdiag.Single(x=>x.Id==createAndEditPatient_Drug_Treatment.Id));
                        
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


        public ActionResult Delete(int id, int patient_Id, int diag_Id)
        {

            var patient = _patientsRepository.FindById(patient_Id, x => x.Patient_Drug_Treatments);
            var PtdrgTrt = patient.Patient_Drug_Treatments.Where(x => x.Owner_DiagnoseId == diag_Id);

            var Ptdiag = _patientsRepository.FindById(patient_Id, x => x.Diagnoses);
            var diag = Ptdiag.Diagnoses.Single(x => x.Id == diag_Id);

            var ptdtl = PtdrgTrt.Single(x => x.Id == id);

            var drg = _drugRepository.FindById(ptdtl.Owner_DrugId);

            ViewBag.Message = patient.FullName;
            ViewBag.Diagnose = diag.Diagnose_details;
            ViewBag.Drug = drg.Drug_name;

            if (patient == null)
            {
                return HttpNotFound();
            }

            var data = new DisplayPatient_Drug_Treatment();

            _imapper.Map(PtdrgTrt.Single(x => x.Id == id), data);

            return View(data);
        }

        [HttpPost, ActionName("Delete")]
        // [ValidateAntiForgeryToken]
        // [HttpPost]
        public ActionResult Delete(DisplayPatient_Drug_Treatment displayPatient_Drug_Treatment, int patient_Id, int diag_Id)
        {
            ViewBag.Patient_Id = patient_Id;

            using (_unitOfWorkFactory.Create())
            {
                var patient = _patientsRepository.FindById(patient_Id, x => x.Patient_Drug_Treatments);
                var PtdrgTrt = patient.Patient_Drug_Treatments.Where(x => x.Owner_DiagnoseId == diag_Id);

                var pttrmt= PtdrgTrt.Single(x => x.Id == displayPatient_Drug_Treatment.Id);

                
                patient.Patient_Drug_Treatments.Remove(pttrmt);
                return Json(new { success = true, message = "Removed Successfully" }, JsonRequestBehavior.AllowGet);
            }

        }
    }
}
