using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkDataLayer.Model
{
    public class ContactgegevensEF
    {
        public ContactgegevensEF(string email, string phone, string address)
        {
            Email = email;
            Phone = phone;
            Address = address;
        }

        public ContactgegevensEF()
        {
            
        }

        public int ID { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }
}
