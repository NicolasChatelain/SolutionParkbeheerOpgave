﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkDataLayer.Model
{
    public class ParkEF
    {
        public ParkEF(string iD, string name, string location)
        {
            ID = iD;
            Name = name;
            Location = location;
        }

        public string ID {  get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public List<HuisEF> Houses { get; set; }
    }
}
