using EducationProject.Contract.RequestModel.User;
using EducationProject.Core.Constants;
using EducationProject.Core.Entities;
using EducationProject.Service.IServices;
using FormHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationProject.UI.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IUserService _userService;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IUserService userService, RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _userService = userService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Login()
        {
            var roleCount = await _userService.AddRoles();
            var user = await _userService.GetAdminUser();


            if (user is null)
            {
                User newUser = new User
                {
                    UserName = "admin",
                    FullName = "Admin",
                    Email = "ozgur.bassullu@yaz.com.tr",
                    EmailConfirmed = true,
                };

                IdentityResult result = await _userManager.CreateAsync(newUser, "12345");
                var model = new UserModel();
                model.Email = newUser.Email;

                if (result.Succeeded)
                {
                    var role = await _roleManager.FindByNameAsync("Admin");

                    if (role != null)
                    {
                        await _userManager.AddToRoleAsync(newUser, role.Name);
                    }
                }
            }
            var resModel = new LoginRequestModel();
            return View(resModel);

        }

        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [FormValidator]
        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestModel model)
        {

            User user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                //İlgili kullanıcıya dair önceden oluşturulmuş bir Cookie varsa siliyoruz.
                await _signInManager.SignOutAsync();

                Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(user, model.Password, model.Persistent, model.Lock);

                //Lock:belirli bir sürede (mesela 5 dk) kullanıcı yanlıs girerse hesabı bloklasın  mı(true,false)
                if (result.Succeeded)
                {

                    return FormResult.CreateSuccessResult("Success", Url.Action("Index", "Home"));

                }
                else if (result.IsNotAllowed)
                {
                    return FormResult.CreateErrorResult("Email is not activated");
                }
            }

            return FormResult.CreateErrorResult("Email or password is wrong");

        }

        [AllowAnonymous]
        [FormValidator]
        [HttpPost]
        public async Task<IActionResult> CreateAccount(LoginRequestModel model)
        {

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null || user.IsDeleted)
            {
                User newUser = new User
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FullName = model.Email,
                    EmailConfirmed=true
                };

                IdentityResult result = await _userManager.CreateAsync(newUser, model.Password);
                if (result.Succeeded)
                {

                    var role = await _roleManager.FindByNameAsync(Constants.UserType.User.ToString());

                    if (role != null)
                    {
                        await _userManager.AddToRoleAsync(newUser, role.Name);
                    }


                    return FormResult.CreateSuccessResult("Added user");


                }
                else
                {
                    return FormResult.CreateErrorResult("An error occurred");
                }

            }

            return FormResult.CreateErrorResult("There is such a user");

        }

        public IActionResult AccessDenied()
        {

            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");

        }

    }
}
