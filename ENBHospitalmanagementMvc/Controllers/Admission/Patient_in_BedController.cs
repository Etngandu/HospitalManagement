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
    public class Patient_in_BedController : Controller
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
        public Patient_in_BedController(IPatientsRepository patientsRepository, IStaffRepository staffRepository, 
                  IWardRepository wardRepository ,IUnitOfWorkFactory unitOfWorkFactory, IMapper imapper)
        {
            _patientsRepository = patientsRepository;
            _staffRepository = staffRepository;
            _wardRepository = wardRepository;
            _unitOfWorkFactory = unitOfWorkFactory;
            _imapper = imapper;
        }


        public ActionResult List(int ward_Id,int patient_Id)
        {
            ViewBag.Patient_Id = patient_Id;
            ViewBag.Ward_Id = ward_Id;

            var Patient = _patientsRepository.FindById(patient_Id, x => x.Patient_In_Beds);
            var Ptbds = Patient.Patient_In_Beds.Where(x => x.Owner_WardId == ward_Id);
            var ward = _wardRepository.FindById(ward_Id);       


            ViewBag.Message = Patient.FullName;
            ViewBag.Ward = ward.Ward_Name;
            return View();
        }

        public ActionResult GetListPatient_in_bed(int ward_Id, int patient_Id)
        {
            var patient = _patientsRepository.FindById(patient_Id, x => x.Patient_In_Beds,v=>v.Patient_In_Wards);
            var ward_bed = _wardRepository.FindById(ward_Id, x => x.Beds);

            var patbed = patient.Patient_In_Beds.Where(x => x.Owner_WardId == ward_Id)  
                       
                        .Join(ward_bed.Beds,
                        ptbd => ptbd.Owner_BedId,
                        bd => bd.Id,
                        (stud, stand) => new DisplayPatient_in_Bed
                        {
                           Id=stud.Id,
                           Patient_Id = (int)stud.Owner_PatientId,
                           Bed_Id =stud.Owner_BedId,
                           Ward_Id=stud.Owner_BedId,
                           Date_from=stud.Date_from,
                           Date_to=stud.Date_to,
                           BedNumber=stand.Bed_Number
                        }

                        );

            ViewBag.Message = patient.FullName;

            var Mpdata = new List<DisplayPatient_in_Bed>();

            _imapper.Map(patbed, Mpdata);

            return Json(new { data = Mpdata }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Create(int patient_Id, int ward_Id)
        {
            ViewBag.Patient_Id = patient_Id;
            ViewBag.Ward_Id = ward_Id;

            var wrd_bds = _wardRepository.FindById(ward_Id, x => x.Beds);

            var createAndEditPatient_In_Bed = new CreateAndEditPatient_in_Bed()
            {               
                 ListBed= wrd_bds.Beds
                        .Select(d => new SelectListItem
                        {
                            Text = d.Bed_Number ,
                            Value = d.Id.ToString(),
                            Selected = false

                        }).Distinct().ToList()
                 
                

            };


            return View(createAndEditPatient_In_Bed);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateAndEditPatient_in_Bed createAndEditPatient_in_Bed,
                                     int patient_Id, int ward_Id)
        {
            ViewBag.Patient_Id = patient_Id;
            ViewBag.Ward_Id = ward_Id;

            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                try
                {
                    using (_unitOfWorkFactory.Create())
                    {
                        var patient = _patientsRepository.FindById(patient_Id, x => x.Patient_In_Wards);
                        var ptwrd = patient.Patient_In_Wards.Single(x => x.Owner_WardId == ward_Id);



                        var patient_In_Bed = new Patient_in_Bed();


                        _imapper.Map(createAndEditPatient_in_Bed, patient_In_Bed);
                       // patient.Patient_In_Beds.Add(patient_In_Bed);
                         ptwrd.Patient_In_Beds.Add(patient_In_Bed);

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


        public ActionResult Edit(int id,int ward_Id, int patient_Id)
        {
            ViewBag.Patient_Id = patient_Id;
            ViewBag.Ward_Id = ward_Id;

            var patient = _patientsRepository.FindById(patient_Id,y=>y.Patient_In_Beds);
           // var ptwrd = patient.Patient_In_Wards.Where(x => x.Owner_WardId == ward_Id);
            var pt_bds = patient.Patient_In_Beds.Where(x => x.Owner_WardId == ward_Id);
            if (patient == null)
            {
                return HttpNotFound();
            }

            var wrd_bds = _wardRepository.FindById(ward_Id, x => x.Beds);
            var data = new CreateAndEditPatient_in_Bed()            
            {
                ListBed = wrd_bds.Beds
                        .Select(d => new SelectListItem
                        {
                            Text = d.Bed_Number,
                            Value = d.Id.ToString(),
                            Selected = true

                        }).Distinct().ToList()

            };
            _imapper.Map(pt_bds.Single(x => x.Id ==id), data);
            return View(data);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit(CreateAndEditPatient_in_Bed createAndEditPatient_In_Bed,
                          int patient_Id, int ward_Id)
        {
            ViewBag.Patient_Id = patient_Id;
            ViewBag.Ward_Id = ward_Id;

            if (ModelState.IsValid)
            {
                try
                {
                    using (_unitOfWorkFactory.Create())
                    {
                        var patient = _patientsRepository.FindById(patient_Id, x => x.Patient_In_Beds);
                        var patbd = patient.Patient_In_Beds.Where(x => x.Owner_WardId == ward_Id);

                        _imapper.Map(createAndEditPatient_In_Bed, patbd.Single(x => x.Id == createAndEditPatient_In_Bed.Id));

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


        public ActionResult Delete(int id, int patient_Id, int ward_Id)
        {


            using (_unitOfWorkFactory.Create())
            {

                var patient = _patientsRepository.FindById(patient_Id, x => x.Patient_In_Beds);

                var ward_bed = _wardRepository.FindById(ward_Id, x => x.Beds);

                var patbed = patient.Patient_In_Beds.Where(x => x.Owner_WardId == ward_Id)

                            .Join(ward_bed.Beds,
                            ptbd => ptbd.Owner_BedId,
                            bd => bd.Id,
                            (stud, stand) => new DisplayPatient_in_Bed
                            {
                                Id = stud.Id,
                                Patient_Id = (int)stud.Owner_PatientId,
                                Bed_Id = stud.Owner_BedId,
                                Ward_Id = stud.Owner_BedId,
                                Date_from = stud.Date_from,
                                Date_to = stud.Date_to,
                                BedNumber = stand.Bed_Number
                            }

                            );


                var data = new DisplayPatient_in_Bed();
                
                var ptbdsgle = patbed.Single(x => x.Id == id);

                ViewBag.Message = patient.FullName;
                ViewBag.Staff = ptbdsgle.BedNumber;

                _imapper.Map(patbed.Single(x => x.Id == id), data);

                return View(data);
            }
        }

        [HttpPost, ActionName("Delete")]
        // [ValidateAntiForgeryToken]
        // [HttpPost]
        public ActionResult Delete(DisplayPatient_in_Bed displayPatient_In_Bed, int patient_Id, int ward_Id)
        {
            ViewBag.Patient_Id = patient_Id;
            using (_unitOfWorkFactory.Create())
            {
                var patient = _patientsRepository.FindById(patient_Id, x => x.Patient_In_Wards);
                var ptbd = patient.Patient_In_Beds.Where(x => x.Owner_WardId == ward_Id);

                var ptbdsgl = ptbd.Single(x => x.Id == displayPatient_In_Bed.Id);

                patient.Patient_In_Beds.Remove(ptbdsgl);
                return Json(new { success = true, message = "Removed Successfully" }, JsonRequestBehavior.AllowGet);
            }

        }
    }
}