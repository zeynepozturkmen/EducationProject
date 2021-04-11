using System;
using System.Collections.Generic;
using System.Text;

namespace EducationProject.Contract.RequestModel.User
{
    public class UserModel
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string UserRole { get; set; }
        public string Tc { get; set; }
        public string FullName { get; set; }
        public Guid RoleId { get; set; } = new Guid();
    }
}
