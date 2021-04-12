using EducationProject.Contract.Common;
using EducationProject.Contract.RequestModel.Education;
using EducationProject.Core.Extensions;
using EducationProject.Service.IServices;
using FormHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationProject.UI.Controllers
{
    [Authorize(Roles = "Admin")]

    public class EducationController : Controller
    {
        private readonly IEducationService _educationService;
        private IWebHostEnvironment _env;
        public EducationController(IEducationService educationService, IWebHostEnvironment env)
        {
            _educationService = educationService;
            _env = env;
        }
        public IActionResult Index()
        {
            var list = _educationService.GetAllEducation();

            return View(list);
        }
        public IActionResult AddEducation()
        {
            ViewBag.TeacherInformationList = _educationService.GetAllTeacherInformation();
            ViewBag.CategoryList = _educationService.GetAllCategory();

            return View(new EducationModel());
        }

        [FormValidator]
        [HttpPost]
        public async Task<IActionResult> AddEducation(EducationModel model)
        {
            model.UserId = User.Identity.GetUserId();
            var education = await _educationService.CreateEducationAsync(model);

            if (education != null)
            {
                return FormResult.CreateSuccessResult("Added education", Url.Action("ByIdEducation", "Education", new { Id = education.Id }));
            }
            else
            {
                return FormResult.CreateErrorResult("An error occurred");
            }
        }
        public async Task<IActionResult> ByIdEducation(Guid Id)
        {
            ViewBag.EducationContentTypeList = _educationService.GetAllEducationContentType();

            var education = await _educationService.GetEducationByIdAsync(Id);
            return View(education);
        }
        public async Task<IActionResult> UpdateEducation(Guid Id)
        {
            ViewBag.TeacherInformationList = _educationService.GetAllTeacherInformation();
            ViewBag.CategoryList = _educationService.GetAllCategory();

            var education = await _educationService.GetEducationModelByIdAsync(Id);
            return View(education);

        }

        [FormValidator]
        [HttpPost]
        public async Task<IActionResult> UpdateEducation(UpdateEducationRequestModel model)
        {
            model.UserId = User.Identity.GetUserId();
            var education = await _educationService.UpdateEducationAsync(model);

            if (education != null)
            {
                return FormResult.CreateSuccessResult("Updated education", Url.Action("UpdateEducation", "Education", new { Id = education.Id }));
            }
            else
            {
                return FormResult.CreateErrorResult("An error occurred");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddEducationContent(AddEducationContentRequestModel model)
        {
            if (model is null)
            {
                return Json(new { failed = true, message = "Veri boş" });
            }

            if (model.File != null)
            {
                Guid g = Guid.NewGuid();
                var fileName = g.ToString() + model.File.FileName;
                var folderName = "wwwroot\\UploadFiles";
                var filePath = Path.Combine(_env.ContentRootPath, folderName, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    model.File.CopyTo(stream);
                }

                model.FilePath = fileName;

            }

            model.UserId = User.Identity.GetUserId();

            var educationContent = await _educationService.CreateEducationContentAsync(model);

            return PartialView("~/Views/Education/_PartialEducationContentList.cshtml", educationContent);


        }

        [HttpPost]
        public async Task<JsonResult> DeleteEducationContent(Guid EducationContentId)
        {
            if (EducationContentId == Guid.Empty)
            {

                return Json(new { failed = true, message = "Geçersiz id." });
            }

            var educationContent = await _educationService.DeleteEducationContentAsync(EducationContentId);

            if (educationContent != null)
            {

                return Json(new { failed = false, message = "Deleted education content." });
            }
            else
            {
                return Json(new { failed = true, message = "An error occurred" });
            }
        }

        [HttpPost]
        public async Task<JsonResult> DeleteEducation(Guid EducationId)
        {
            if (EducationId == Guid.Empty)
            {

                return Json(new { failed = true, message = "Geçersiz id." });
            }

            var educationContent = await _educationService.DeleteEducationAsync(EducationId);

            if (educationContent != null)
            {

                return Json(new { failed = false, message = "Deleted education." });
            }
            else
            {
                return Json(new { failed = true, message = "An error occurred" });
            }
        }

        public async Task<IActionResult> PreviewEducationContent(Guid Id)
        {
            if (Id == Guid.Empty)
            {
                return Json(new { failed = true, message = "Id is null" });
            }

            var ecucationContent =await _educationService.GetEducationContentByIdAsync(Id);
            if (ecucationContent != null)
            {
                return PartialView("~/Views/Education/_PartialPreviewEducationContent.cshtml", ecucationContent);
            }
            else
            {
                return Json(new { failed = true, message = "An error occured" });
            }


        }
    }
}
