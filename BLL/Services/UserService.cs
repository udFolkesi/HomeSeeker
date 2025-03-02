using Core.Entities;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BLL.Services
{
    public class UserService
    {
        private readonly PasswordHasher<User> _passwordHasher = new PasswordHasher<User>();

        public User RegisterUser(RegisterViewModel model)
        {
            var user = new User
            {
                Email = model.Email,
                PasswordHash = _passwordHasher.HashPassword(null, model.Password),
                Profile = new Profile()
            };

            // Save to DB (using Entity Framework, for example)
            return user;
        }
    }
}
