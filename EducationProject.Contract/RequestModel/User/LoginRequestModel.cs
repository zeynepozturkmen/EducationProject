using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EducationProject.Contract.RequestModel.User
{
    public class LoginRequestModel
    {
        public string Email {get; set; }
        public string Password { get; set; }

        public string UserName { get; set; }

        [Display(Name = "Beni Hatırla")]
        public bool Persistent { get; set; }
        public bool Lock { get; set; }
    }
}
