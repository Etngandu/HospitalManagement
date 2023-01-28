using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using HospitalManagement.Infrastructure;
using HospitalManagement.Entities;
using ENBHospitalmanagementMvc.Models;
using HospitalManagement.Repositories.EF;
using HospitalManagement.Entities.Repositories;

namespace ENBHospitalmanagementMvc.Controllers.Patienten
{
    public class DrugController : Controller
    
    {
        // GET: Case

        private readonly IDrugRepository _drugRepository;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly IMapper _imapper;
        const int PageSize = 10;


        /// <summary>
        /// Initializes a new instance of the CaseController class.
        /// </summary>
        public DrugController(IDrugRepository drugRepository, IUnitOfWorkFactory unitOfWorkFactory, IMapper imapper)
        {
            _drugRepository = drugRepository;
            _unitOfWorkFactory = unitOfWorkFactory;
            _imapper = imapper;
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetData()
        {
            IQueryable<Drug> allDrug = _drugRepository.FindAll();

            var Mpdata = _imapper.Map<List<DisplayDrug>>(allDrug).ToList();
            return Json(new { data = Mpdata }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateAndEditDrug createAndEditDrug)
        {

            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                try
                {
                    using (_unitOfWorkFactory.Create())
                    {
                        
                        Drug dbDrug = new Drug();

                        _imapper.Map(createAndEditDrug, dbDrug);
                        _drugRepository.Add(dbDrug);
                        // return RedirectToAction("Index");
                        // return Json(new { success = true, message = "Saved Successfully" }, JsonRequestBehavior.AllowGet);
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

       


        public ActionResult Edit(int id)
        {
            Drug dbDrug = _drugRepository.FindById(id);
            if (dbDrug == null)
            {
                return HttpNotFound();
            }
            var data = _imapper.Map<CreateAndEditDrug>(dbDrug);

            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CreateAndEditDrug createAndEditDrug)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (_unitOfWorkFactory.Create())
                    {
                        Drug dbDrugToUpdate = _drugRepository.FindById(createAndEditDrug.Id);
                        _imapper.Map(createAndEditDrug, dbDrugToUpdate, typeof(CreateAndEditDrug), typeof(Drug));
                        // return RedirectToAction("Index");
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

        public ActionResult Deletes(int id)
        {
            Drug dbDrug = _drugRepository.FindById(id);
            if (dbDrug == null)
            {
                return HttpNotFound();
            }
            var data = _imapper.Map<DisplayDrug>(dbDrug);

            return View(data);

        }

        [HttpPost, ActionName("Delete")]
        // [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {

            Drug dbDrug = _drugRepository.FindById(id);
            if (dbDrug == null)
            {
                return HttpNotFound();
            }

            var data = _imapper.Map<DisplayDrug>(dbDrug);
            using (_unitOfWorkFactory.Create())
            {
                _drugRepository.Remove(id);
            }
            return Json(new { success = true, message = "Removed Successfully" }, JsonRequestBehavior.AllowGet);
        }
    }
}
