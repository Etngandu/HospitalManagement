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
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.IO;

namespace ENBHospitalmanagementMvc.Controllers.Patienten
{
    public class StaffController : Controller
    {


        private readonly IStaffRepository _staffRepository;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly IMapper _imapper;
        const int PageSize = 10;


        /// <summary>
        /// Initializes a new instance of the CaseController class.
        /// </summary>
        public StaffController(IStaffRepository staffRepository, IUnitOfWorkFactory unitOfWorkFactory, IMapper imapper)
        {
            _staffRepository = staffRepository;
            _unitOfWorkFactory = unitOfWorkFactory;
            _imapper = imapper;
        }

        public ActionResult GetData()
        {
            IQueryable<Staff> allStaff = _staffRepository.FindAll();

           

            var Mpdata = _imapper.Map<List<DisplayStaff>>(allStaff).ToList();


            return Json(new { data = Mpdata }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Index()
        {
            return View();
        }



        public ActionResult Detail(int id)
        {
            Staff dbStaff = _staffRepository.FindById(id);

            ViewBag.Message = dbStaff.FullName;

            if (dbStaff == null)
            {
                return HttpNotFound();
            }

            var data = _imapper.Map<DisplayStaff>(dbStaff);

            return View(data);
        }

        public ActionResult Create()
        {
            CreateAndEditStaff createAndEditStaff = new CreateAndEditStaff();
            return View(createAndEditStaff);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateAndEditStaff createAndEditStaff)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                try
                {
                    using (_unitOfWorkFactory.Create())
                    {
                        Staff dbStaff = new Staff();

                        createAndEditStaff.ImagePath = ImageUpl(createAndEditStaff);

                        _imapper.Map(createAndEditStaff, dbStaff);
                        _staffRepository.Add(dbStaff);                       
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

        public string ImageUpl(CreateAndEditStaff createAndEditStaff)
        {
            if (createAndEditStaff.ImageUpload != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(createAndEditStaff.ImageUpload.FileName);
                string extension = Path.GetExtension(createAndEditStaff.ImageUpload.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                createAndEditStaff.ImagePath = "~/Appfiles/Images/" + fileName;
                createAndEditStaff.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Appfiles/Images/"), fileName));
            }

            return createAndEditStaff.ImagePath;
        }

        public ActionResult Edit(int id)
        {
            Staff dbStaff = _staffRepository.FindById(id);
            ViewBag.Path = dbStaff.ImagePath;
            if (dbStaff == null)
            {
                return HttpNotFound();
            }
            var data = _imapper.Map<CreateAndEditStaff>(dbStaff);

            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CreateAndEditStaff createAndEditStaff)
        {
           
            if (ModelState.IsValid)
            {
                try
                {
                    using (_unitOfWorkFactory.Create())
                    {
                         Staff dbStaffToUpdate = _staffRepository.FindById(createAndEditStaff.Id);
                         createAndEditStaff.ImagePath = ImageUpl(createAndEditStaff);
                        _imapper.Map(createAndEditStaff, dbStaffToUpdate, typeof(CreateAndEditStaff), typeof(Staff));
                        
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

        public ActionResult Delete(int id)
        {
            Staff dbStaff = _staffRepository.FindById(id);
            if (dbStaff == null)
            {
                return HttpNotFound();
            }
            var data = _imapper.Map<DisplayStaff>(dbStaff);
            return View(data);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            using (_unitOfWorkFactory.Create())
            {
                _staffRepository.Remove(id);
            }
            return RedirectToAction("Index");
        }
    }
}
