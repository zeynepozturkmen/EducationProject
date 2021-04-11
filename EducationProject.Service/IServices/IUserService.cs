using EducationProject.Core.Entities;
using EducationProject.Service.IService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EducationProject.Service.IServices
{
    public interface IUserService : IBaseService<UserEducation>
    {
        Task<User> GetAdminUser();
        Task<int> AddRoles();
    }
}
