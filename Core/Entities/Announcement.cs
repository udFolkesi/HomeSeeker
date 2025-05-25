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
        //public string Title { get; set; }
        public string PropertyType { get; set; }
        public DateTime PublishedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Status { get; set; }
        public int ProfileID { get; set; }
        public int CategoryID { get; set; }

        public ICollection<Image> Images { get; set; } = new List<Image>();
        public ObjectDescription Description { get; set; }
        public Category Category { get; set; }
    }
}
