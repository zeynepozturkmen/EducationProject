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
        public async Task<IActionResult> Index()
        {
            var list = await _educationService.GetAllEducationAsync();

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
                var reqModel = new ByIdRequestModel();
                reqModel.Id = education.Id;

                return FormResult.CreateSuccessResult("Added education", Url.Action("ByIdEducation", "Education", new { model = reqModel }));
            }
            else
            {
                return FormResult.CreateErrorResult("An error occurred");
            }
        }

        public async Task<IActionResult> ByIdEducation(ByIdRequestModel model)
        {
            ViewBag.EducationContentTypeList = _educationService.GetAllEducationContentType();

            var education = await _educationService.GetEducationByIdAsync(model);
            return View(education);
        }


        [HttpPost]
        public async Task<IActionResult> AddEducationContent(AddEducationContentRequestModel model)
        {
            if (model is null)
            {
                return Json(new { failed = true, message = "Veri boş" });
            }


            Guid g = Guid.NewGuid();
            var fileName = g.ToString() + model.File.FileName;
            var folderName = "wwwroot\\UploadFiles";
            var filePath = Path.Combine(_env.ContentRootPath, folderName, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                model.File.CopyTo(stream);
            }


            var result = new StringBuilder();
            using (var reader = new StreamReader(model.File.OpenReadStream()))
            {
                while (reader.Peek() >= 0)
                    result.AppendLine(await reader.ReadLineAsync());
            }
            var x= result.ToString();

            model.FilePath = fileName;

            var educationContent = await _educationService.CreateEducationContentAsync(model);

            return PartialView("~/Views/DriverDocument/_PartialDriverDocumentList.cshtml", educationContent);


        }

    }
}
