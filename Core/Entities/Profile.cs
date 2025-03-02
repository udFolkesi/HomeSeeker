using Core.Entities.Abstractions;
using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Profile: BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public RoleEnum Role { get; set; } = RoleEnum.Guest;
        public string PhoneNumber { get; set; }
        public User User { get; set; }
    }
}
