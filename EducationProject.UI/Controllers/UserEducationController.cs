using EducationProject.Contract.Common;
using EducationProject.Contract.ResponseModel.Education;
using EducationProject.Core.Extensions;
using EducationProject.Service.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationProject.UI.Controllers
{
    [Authorize(Roles = "User")]
    public class UserEducationController : Controller
    {
        private readonly IUserEducationService _userEducationService;
        public UserEducationController(IUserEducationService userEducationService)
        {
            _userEducationService = userEducationService;
        }
        public IActionResult Index()
        {

            var list = _userEducationService.GetAllEducation();

            return View(list);
        }
        public async Task<IActionResult> BuyEducation(ByIdRequestModel model)
        {
            if (model.Id == Guid.Empty)
            {

                return Json(new { failed = true, message = "Null educationId." });
            }

            model.UserId = User.Identity.GetUserId();

            var education = await _userEducationService.BuyEducation(model);

            if (education != null)
            {

                return Json(new { failed = false, message = "Added education" });
            }
            else
            {
                return Json(new { failed = true, message = "An error occurred" });
            }

        }
        public IActionResult EducationList()
        {
            var model = new ByIdRequestModel();
            model.Id = User.Identity.GetUserId();

            var list = _userEducationService.GetAllEducationByUserId(model);

            ViewBag.CompletedEducationList = _userEducationService.GetAllCompletedEducationByUserId(model);

            return View(list);
        }
        public async Task<IActionResult> StartEducationByUserEducationId(ByIdRequestModel model)
        {
            model.UserId = User.Identity.GetUserId();

            var education = await _userEducationService.StartEducationByEducationId(model);

            return View("EducationContent", education);
        }
        public async Task<IActionResult> NextEducationContent(Guid Id, Guid UserEducationId)
        {
            var education = await _userEducationService.NextByEducationContentId(Id, UserEducationId);
            return View("EducationContent", education);
        }

        public IActionResult EducationContent(EducationContentResponseModel model)
        {

            return View(model);
        }

        public async Task<IActionResult> ContinueEducationByUserEducationId(ByIdRequestModel model)
        {

            var education = await _userEducationService.ContinueEducationByUserEducationId(model);

            return View("EducationContent", education);
        }

        public async Task<IActionResult> BackEducationContent(Guid Id, Guid UserEducationId)
        {
            var education = await _userEducationService.BackByEducationContentId(Id,UserEducationId);

            return View("EducationContent", education);
        }

        public async Task<IActionResult> CompletedEducation(ByIdRequestModel model)
        {
            await _userEducationService.CompletedEducation(model);

            return RedirectToAction("EducationList");
        }

        public async Task<IActionResult> TrainingEducation(ByIdRequestModel model)
        {
            await _userEducationService.TrainingEducation(model);

            return RedirectToAction("EducationList");
        }

        


    }
}
