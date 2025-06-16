using Core.Entities.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Image: BaseEntity
    {
        public string Url { get; set; }
        public bool IsMain { get; set; }

        public int AnnouncementId { get; set; }
        public Announcement Announcement { get; set; }
    }
}
