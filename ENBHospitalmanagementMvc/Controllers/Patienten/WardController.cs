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
    public class WardController : Controller
    {
        // GET: Case

        private readonly IWardRepository _wardRepository;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly IMapper _imapper;
        const int PageSize = 10;


        /// <summary>
        /// Initializes a new instance of the CaseController class.
        /// </summary>
        public WardController(IWardRepository wardRepository, IUnitOfWorkFactory unitOfWorkFactory, IMapper imapper)
        {
            _wardRepository = wardRepository;
            _unitOfWorkFactory = unitOfWorkFactory;
            _imapper = imapper;
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetData()
        {
            IQueryable<Ward> allWard = _wardRepository.FindAll();

            var Mpdata = _imapper.Map<List<DisplayWard>>(allWard).ToList();
            return Json(new { data = Mpdata }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateAndEditWard createAndEditWard)
        {

            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                try
                {
                    using (_unitOfWorkFactory.Create())
                    {
                        Ward dbWard = new Ward();

                        _imapper.Map(createAndEditWard, dbWard);
                        _wardRepository.Add(dbWard);
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

        
        public ActionResult Edit(int id)
        {
            Ward dbWard = _wardRepository.FindById(id);
            if (dbWard == null)
            {
                return HttpNotFound();
            }
            var data = _imapper.Map<CreateAndEditWard>(dbWard);

            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CreateAndEditWard createAndEditWard)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (_unitOfWorkFactory.Create())
                    {
                        Ward dbWardToUpdate = _wardRepository.FindById(createAndEditWard.Id);
                        _imapper.Map(createAndEditWard, dbWardToUpdate, typeof(CreateAndEditWard), typeof(Ward));                      
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
            Ward dbWard = _wardRepository.FindById(id);
            if (dbWard == null)
            {
                return HttpNotFound();
            }
            var data = _imapper.Map<DisplayWard>(dbWard);

            return View(data);
            
        }

        [HttpPost, ActionName("Delete")]
       // [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {

            Ward dbWard = _wardRepository.FindById(id);
            if (dbWard == null)
            {
                return HttpNotFound();
            }

            var data = _imapper.Map<DisplayWard>(dbWard);
            using (_unitOfWorkFactory.Create())
            {
                _wardRepository.Remove(id);
            }
            return Json(new { success = true, message = "Removed Successfully" }, JsonRequestBehavior.AllowGet);
        }
    }
}