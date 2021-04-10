using EducationProject.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EducationProject.Core.Constants
{
    public static partial class Constants
    {
        public static Dictionary<Category, Guid> CategoryIds { get; set; } = new Dictionary<Category, Guid>();
        public enum Category
        {
            [Display(Description = "Online")] Online,
            [Display(Description = "Sınıf içi eğitim")] InClass,
            [Display(Description = "Kitap")] Book,
            [Display(Description = "Sunum")] Presentation,
            [Display(Description = "Makale")] Article,
            [Display(Description = "Mini proje")] Project

        }

        public static Dictionary<TeacherInformation, Guid> TeacherInformationIds { get; set; } = new Dictionary<TeacherInformation, Guid>();
        public enum TeacherInformation
        {
            [Display(Description = "İç Eğitmen")] InternalTrainer,
            [Display(Description = "Dış Eğitmen")] ExternalTrainer

        }

        public static Dictionary<EducationContentType, Guid> EducationContentTypeIds { get; set; } = new Dictionary<EducationContentType, Guid>();
        public enum EducationContentType
        {
            [Display(Description = "Video")] Video,
            [Display(Description = "Kitap")] Kitap

        }

        public static Dictionary<UserEducationStatus, Guid> UserEducationStatusIds { get; set; } = new Dictionary<UserEducationStatus, Guid>();
        public enum UserEducationStatus
        {
            [Display(Description = "Talep")] Request,
            [Display(Description = "Talep iptal")] RequestCancellation,
            [Display(Description = "Onay")] Approval,
            [Display(Description = "Red")] Rejection

        }

    }
}
