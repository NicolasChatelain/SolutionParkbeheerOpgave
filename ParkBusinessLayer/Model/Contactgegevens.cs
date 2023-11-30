namespace ParkBusinessLayer.Model
{
    public class Contactgegevens
    {
        public Contactgegevens(string email, string tel, string adres)
        {
            Email = email;
            Phone = tel;
            Adres = adres;
        }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Adres { get; set; }

        public override string ToString()
        {
            return $"CONTACTGEGEVENS: \n\tEmail: {Email}\n\tGSM: {Phone}\n\tAdres: {Adres}";
        }
    }
}