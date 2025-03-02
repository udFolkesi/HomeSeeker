using Core.Entities.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class ObjectDescription: BaseEntity
    {
        public decimal Price { get; set; }
        public string Details { get; set; }
        public double SquareMeters { get; set; }
        public string Address { get; set; }
        public int Rooms { get; set; }
    }
}
