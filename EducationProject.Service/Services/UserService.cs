using EducationProject.Contract.RequestModel.User;
using EducationProject.Core.Entities;
using EducationProject.Data.DbContexts;
using EducationProject.Service.IServices;
using EducationProject.Service.IUnitOfWorks;
using EducationProject.Service.Service;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationProject.Service.Services
{
    public class UserService : BaseService<UserEducation> ,IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        public UserService(EducationDbContext db, IUnitOfWork uow, UserManager<User> userManager, RoleManager<Role> roleManager):base(db,uow)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<User> GetAdminUser()
        {
            var user = await _userManager.FindByNameAsync("admin");

            if (user != null)
            {
                return user;
            }
            return null;
        }
        public async Task<int> AddRoles()
        {

            List<string> roleName = new List<string>() { "Admin", "User"};
            List<string> roleDescription = new List<string>() { "Admin", "Kullanıcı" };

            if (!_roleManager.Roles.Any())
            {
                for (int i = 0; i < roleName.Count(); i++)
                {
                    Role role = new Role();
                    role.Name = roleName[i];
                    role.Description = roleDescription[i];
                    IdentityResult result = await _roleManager.CreateAsync(role);

                    if (result.Succeeded)
                    {
                        continue;
                    }

                }
            }

            return 1;

        }
    }
}
