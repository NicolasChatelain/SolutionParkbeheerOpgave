using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkDataLayer.Model
{
    public class HuisEF
    {
        public int ID { get; set; }
        public string Street { get; set; }
        public int Nr { get; set; }
        public bool IsRentable {  get; set; }
        public ParkEF ParkEF { get; set; }

    }
}
