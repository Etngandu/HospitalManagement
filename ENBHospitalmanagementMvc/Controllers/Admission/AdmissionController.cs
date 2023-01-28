using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using HospitalManagement.Infrastructure;
using HospitalManagement.Entities;
using ENBHospitalmanagementMvc.Models;
using HospitalManagement.Repositories.EF;
using HospitalManagement.Entities.Repositories;

namespace ENBHospitalManagement.Mvc.Controllers.Admission
{
    public class AdmissionController : Controller
    {
        // GET: Case

        private readonly IPatientsRepository _patientsRepository;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly IMapper _imapper;
        const int PageSize = 10;


        /// <summary>
        /// Initializes a new instance of the CaseController class.
        /// </summary>
        public AdmissionController(IPatientsRepository patientsRepository, IUnitOfWorkFactory unitOfWorkFactory, IMapper imapper)
        {
            _patientsRepository = patientsRepository;
            _unitOfWorkFactory = unitOfWorkFactory;
            _imapper = imapper;
        }
        public ActionResult GetData()
        {
            IQueryable<Patient> allPatient = _patientsRepository.FindAll();

            var Mpdata = _imapper.Map<List<DisplayPatient>>(allPatient).ToList();


            return Json(new { data = Mpdata }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Index()
        {
            return View();
        }



        public ActionResult Detail(int id)
        {
            Patient dbPatient = _patientsRepository.FindById(id);

            ViewBag.Message = dbPatient.FullName;

            if (dbPatient == null)
            {
                return HttpNotFound();
            }

            var data = _imapper.Map<DisplayPatient>(dbPatient);

            return View(data);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateAndEditPatient createAndEditPatient)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                try
                {
                    using (_unitOfWorkFactory.Create())
                    {
                        Patient dbPatient = new Patient();

                        _imapper.Map(createAndEditPatient, dbPatient);
                        _patientsRepository.Add(dbPatient);
                        return RedirectToAction("Index");
                    }
                }
                catch (ModelValidationException mvex)
                {
                    foreach (var error in mvex.ValidationErrors)
                    {
                        ModelState.AddModelError(error.MemberNames.FirstOrDefault() ?? "", error.ErrorMessage);
                    }
                }
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

        //    public ActionResult Create()
        //    {
        //        return View();
        //    }

        //    [HttpPost]
        //    [ValidateAntiForgeryToken]
        //    public ActionResult Create(CreateAndEditCase createAndEditCase)
        //    {
        //        var errors = ModelState.Values.SelectMany(v => v.Errors);
        //        if (ModelState.IsValid)
        //        {
        //            try
        //            {
        //                using (_unitOfWorkFactory.Create())
        //                {
        //                    Case dbCase = new Case();

        //                    _imapper.Map(createAndEditCase, dbCase);
        //                    _caseRepository.Add(dbCase);
        //                    return RedirectToAction("Index");
        //                }
        //            }
        //            catch (ModelValidationException mvex)
        //            {
        //                foreach (var error in mvex.ValidationErrors)
        //                {
        //                    ModelState.AddModelError(error.MemberNames.FirstOrDefault() ?? "", error.ErrorMessage);
        //                }
        //            }
        //        }
        //        return View();
        //    }

        //    public ActionResult Edit(int id)
        //    {
        //        Case dbCase = _caseRepository.FindById(id);
        //        if (dbCase == null)
        //        {
        //            return HttpNotFound();
        //        }
        //        var data = _imapper.Map<CreateAndEditCase>(dbCase);

        //        return View(data);
        //    }

        //    [HttpPost]
        //    [ValidateAntiForgeryToken]
        //    public ActionResult Edit(CreateAndEditCase createAndEditCase)
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            try
        //            {
        //                using (_unitOfWorkFactory.Create())
        //                {
        //                    Case dbCaseToUpdate = _caseRepository.FindById(createAndEditCase.Id);
        //                    _imapper.Map(createAndEditCase, dbCaseToUpdate, typeof(CreateAndEditCase), typeof(Case));
        //                    return RedirectToAction("Index");
        //                }
        //            }
        //            catch (ModelValidationException mvex)
        //            {
        //                foreach (var error in mvex.ValidationErrors)
        //                {
        //                    ModelState.AddModelError(error.MemberNames.FirstOrDefault() ?? "", error.ErrorMessage);
        //                }
        //            }
        //        }
        //        return View();
        //    }

        //    public ActionResult Delete(int id)
        //    {
        //        Case dbCase = _caseRepository.FindById(id);
        //        if (dbCase == null)
        //        {
        //            return HttpNotFound();
        //        }
        //        var data = _imapper.Map<DisplayCase>(dbCase);
        //        return View(data);
        //    }

        //    [HttpPost, ActionName("Delete")]
        //    [ValidateAntiForgeryToken]
        //    public ActionResult DeleteConfirmed(int id)
        //    {
        //        using (_unitOfWorkFactory.Create())
        //        {
        //            _caseRepository.Remove(id);
        //        }
        //        return RedirectToAction("Index");
        //    }
    }
}
