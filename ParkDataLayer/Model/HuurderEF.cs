using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkDataLayer.Model
{
    public class HuurderEF
    {
        public HuurderEF()
        {
            
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public int ContactgegevensID { get; set; }
        public ContactgegevensEF Contactgegevens { get; set; }
        public ICollection<HuurcontractEF> Contracts { get; set; }
    }
}
