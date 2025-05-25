using Core.Entities.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class ObjectDescription
    {
        [Key, ForeignKey("AnnouncementId")]
        public int AnnouncementId { get; set; }
        public decimal Price { get; set; }
        public string Details { get; set; }
        public double SquareMeters { get; set; }
        public string Address { get; set; } = null!;
        public int Rooms { get; set; }

        public Profile Profile { get; set; } = null!;
    }
}
