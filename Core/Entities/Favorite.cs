using Core.Entities.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Favorite: BaseEntity
    {
        public DateTime AddingTime { get; set; }
        public int AnnouncementID { get; set; }
        public int ProfileID { get; set; }

        public Profile Profile { get; set; }
        public Announcement Announcement { get; set; }
    }
}
