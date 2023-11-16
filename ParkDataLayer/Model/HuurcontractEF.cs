using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkDataLayer.Model
{
    public class HuurcontractEF
    {
        public string ID {  get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int AantalDagenVerhuurd {  get; set; }
        public HuisEF HuisEF { get; set; }
        public HuurderEF HuurderEF { get; set;}
    }
}
