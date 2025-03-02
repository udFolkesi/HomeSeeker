using Core.Entities.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Announcement: BaseEntity
    {
        public string PropertyType { get; set; }
        public DateTime PublishedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Status { get; set; }
        public int UserID { get; set; }
        public int CategoryID { get; set; }
    }
}
