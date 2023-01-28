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

    public class BedController : Controller
    {
        // GET: Case

        private readonly IWardRepository _wardRepository;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly IMapper _imapper;
        const int PageSize = 10;


        /// <summary>
        /// Initializes a new instance of the CaseController class.
        /// </summary>
        public BedController(IWardRepository wardRepository, IUnitOfWorkFactory unitOfWorkFactory, IMapper imapper)
        {
            _wardRepository = wardRepository;
            _unitOfWorkFactory = unitOfWorkFactory;
            _imapper = imapper;
        }


        public ActionResult List(int Id)
        {
            ViewBag.IdWard = Id;
            
            var Wardbeds = _wardRepository.FindById(Id, x => x.Beds);

            ViewBag.Message = Wardbeds.Ward_Name;
            return View();
        }
       
        public ActionResult GetListBed(int Id)
        {
            var Wardbeds = _wardRepository.FindById(Id, x=>x.Beds);

            ViewBag.Message = Wardbeds.Ward_Name;

            var Mpdata = new List<DisplayBed>();

            _imapper.Map(Wardbeds.Beds, Mpdata);
           
            return Json(new { data = Mpdata }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Create(int WardId)
        {
            ViewBag.WardId = WardId;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateAndEditBed createAndEditBed, int WardId)
        {

            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                try
                {
                    using (_unitOfWorkFactory.Create())
                    {
                        var wards = _wardRepository.FindById(WardId);
                        var bed = new Bed();

                        ViewBag.Message = wards.Ward_Name;

                        _imapper.Map(createAndEditBed, bed);                       
                         wards.Beds.Add(bed);
                       // return RedirectToAction("List", new { operatorId });
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


        public ActionResult Edit(int id, int WardId)
        {
            ViewBag.WardId = WardId;
            ViewBag.BedId = id;

            var wards = _wardRepository.FindById(WardId, x => x.Beds);
            if (wards == null)
            {
                return HttpNotFound();
            }
            var data = new CreateAndEditBed();
            _imapper.Map(wards.Beds.Single(x => x.Id == id), data);
            return View(data);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit(CreateAndEditBed createAndEditBed, int WardId)
        {
            ViewBag.WardId = WardId;

            if (ModelState.IsValid)
            {
                try
                {
                    using (_unitOfWorkFactory.Create())
                    {
                        var wardx = _wardRepository.FindById(WardId, x => x.Beds);
                        var bed = wardx.Beds.Single(x => x.Id == createAndEditBed.Id);
                        _imapper.Map(createAndEditBed, bed);
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

              
        public ActionResult Delete(int id , int WardId)
        {
            

            using (_unitOfWorkFactory.Create())
            {
               
                var wards = _wardRepository.FindById(WardId, x => x.Beds);
                var data = new DisplayBed();
                var bed = wards.Beds.Single(x => x.Id == id);
                _imapper.Map(wards.Beds.Single(x => x.Id == id), data);
                // wards.Beds.Remove(bed);
                return View (data);
            }
        }

        [HttpPost, ActionName("Delete")]
        // [ValidateAntiForgeryToken]
        // [HttpPost]
        public ActionResult Delete(DisplayBed displayBed, int wardId)
        {
            ViewBag.WardId = wardId;

            using (_unitOfWorkFactory.Create())
            {
                var wardx = _wardRepository.FindById(wardId, x => x.Beds);
                var bed = wardx.Beds.Single(x => x.Id == displayBed.Id);
                wardx.Beds.Remove(bed);
                return Json(new { success = true, message = "Removed Successfully" }, JsonRequestBehavior.AllowGet);
            }

        }
    }
}