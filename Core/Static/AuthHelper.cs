using Core.Entities;
using System.Security.Claims;

namespace Core.Static
{
    public class AuthHelper
    {
        public static ClaimsIdentity Authenticate(User user)
        {
            var announcementsIds = new List<int>();
            //var coursesNames = new List<string>();

            foreach (var announcement in user.Profile.Announcements)
            {
                announcementsIds.Add(announcement.Id);
                //coursesNames.Add(course.Name);
            }

            var claims = new List<Claim>
            {
                new Claim("Email", user.Email),
                new Claim("Role", user.Profile.Role.ToString()),
                new Claim("Id", user.Id.ToString()),
                new Claim("AnnouncementsIds", string.Join(',', announcementsIds)),
                //new Claim("CoursesNames", string.Join(',', coursesNames))
            };

            return new ClaimsIdentity(claims, "ApplicationCookie",
                ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
        }
    }
}
