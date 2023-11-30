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
        public HuisEF Huis {  get; set; }
        public HuurderEF Huurder { get; set; }
        public HuurperiodeEF Huurperiode { get; set;}

    }
}
