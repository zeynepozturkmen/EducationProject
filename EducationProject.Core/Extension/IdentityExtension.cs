using System;
using System.Security.Claims;
using System.Security.Principal;


namespace EducationProject.Core.Extensions
{
    public static class IdentityExtensions
    {
        public static Guid GetUserId(this IIdentity identity)
        {
            ClaimsIdentity claimsIdentity = identity as ClaimsIdentity;
            var currentUserID = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (currentUserID == null)
                return Guid.Empty;

            return Guid.Parse(currentUserID);
        }
    }
}
