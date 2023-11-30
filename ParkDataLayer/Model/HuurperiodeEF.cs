using System;

namespace ParkDataLayer.Model
{
    public class HuurperiodeEF
    {
        public int ID {  get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int AantalDagenVerhuurd { get; set; }
        public string contractID { get; set; }
    }
}
