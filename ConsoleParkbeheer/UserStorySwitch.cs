using Microsoft.IdentityModel.Protocols.OpenIdConnect.Configuration;
using ParkBusinessLayer.Beheerders;
using ParkBusinessLayer.Model;

namespace ConsoleParkbeheer
{
    internal class UserStorySwitch
    {
        private BeheerContracten _beheerContracten;
        private BeheerHuizen _beheerHuizen;
        private BeheerHuurders _beheerHuurders;

        public UserStorySwitch(BeheerContracten beheercontracten, BeheerHuizen beheerHuizen, BeheerHuurders beheerHuurders)
        {
            _beheerContracten = beheercontracten;
            _beheerHuizen = beheerHuizen;
            _beheerHuurders = beheerHuurders;
        }


        public void Run()
        {
            while (true)
            {
                try
                {
                    int menuoption = Menu();

                    switch (menuoption)
                    {
                        case 1:
                            Huizen();
                            break;
                        case 2:
                            Huurders();
                            break;
                        case 3:
                            Contracten();
                            break;
                    }
                }
                catch (Exception ex)
                {
                    PrintError(ex);
                }

            }

        }

        private int Menu()
        {
            while (true)
            {
                Console.WriteLine("1) Huizen");
                Console.WriteLine("2) Huurders");
                Console.WriteLine("3) Contracten");

                int choice = int.Parse(Console.ReadLine());

                if (choice >= 1 || choice <= 3)
                {
                    Console.Clear();
                    return choice;
                }
            }
        }

        private void Huizen()
        {

            Console.WriteLine("1) Get huizen");
            Console.WriteLine("2) Archiveer");
            Console.WriteLine("3) Nieuw huis");

            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    GetHuis();
                    break;
                case 2:
                    RemoveHuis();
                    break;
                case 3:
                    (string, int, string, string, string) huisinfo = AddHuis();
                    _beheerHuizen.VoegNieuwHuisToe(huisinfo.Item1, huisinfo.Item2, new Park(huisinfo.Item3, huisinfo.Item4, huisinfo.Item5));
                    break;

            }
        }

        private void GetHuis()
        {
            Console.WriteLine("huis id: ");
            int houseid = int.Parse(Console.ReadLine());
            Huis huis = _beheerHuizen.GeefHuis(houseid);

            Console.WriteLine(huis);
            foreach (var item in huis.Huurcontracten())
            {
                PrintLine();
                Console.WriteLine(item.Huurder);
                Console.WriteLine(item.Huurperiode);
                PrintLine();

            }
        }
        private void RemoveHuis()
        {
            Console.Write("huis id: ");
            int choice = int.Parse(Console.ReadLine());

            Huis huis = _beheerHuizen.GeefHuis(choice);
            _beheerHuizen.ArchiveerHuis(huis);
        }
        private (string, int, string, string, string) AddHuis()
        {
            Console.Write("Straat: ");
            string straat = Console.ReadLine();
            Console.Write("Nummer: ");
            int nummer = int.Parse(Console.ReadLine());
            Console.Write("Park id: ");
            string id = Console.ReadLine();
            Console.Write("parknaam: ");
            string naam = Console.ReadLine();
            Console.Write("park locatie: ");
            string locatie = Console.ReadLine();

            return (straat, nummer, id, naam, locatie);
        }

        private void Huurders()
        {
            Console.WriteLine("1) Get huurders");
            Console.WriteLine("2) nieuwe huurder");

            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    GetHuurders();
                    break;
                case 2:
                    (string, Contactgegevens) huurderinfo = AddHuurder();
                    _beheerHuurders.VoegNieuweHuurderToe(huurderinfo.Item1, huurderinfo.Item2);
                    break;
            }
        }

        private void GetHuurders()
        {
            Console.Clear();
            Console.WriteLine("1) by id (1 huurder)");
            Console.WriteLine("2) by name (all with same name)");

            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Console.Write("huurder ID: ");
                    int id = int.Parse(Console.ReadLine());
                    Huurder huurder = _beheerHuurders.GeefHuurder(id);
                    PrintLine();
                    Console.WriteLine(huurder);
                    PrintLine();

                    break;

                case 2:
                    Console.WriteLine("Name: ");
                    string name = Console.ReadLine();
                    List<Huurder> huurders = _beheerHuurders.GeefHuurders(name);
                    foreach (var item in huurders)
                    {
                        PrintLine();
                        Console.WriteLine(item);
                        PrintLine();
                    }
                    break;

            }
        }
        private (string, Contactgegevens) AddHuurder()
        {
            Console.Write("Naam: ");
            string naam = Console.ReadLine();
            Console.Write("email: ");
            string email = Console.ReadLine();
            Console.Write("gsm: ");
            string gsm = Console.ReadLine();
            Console.Write("adres: ");
            string adres = Console.ReadLine();

            Contactgegevens contact = new(email, gsm, adres);

            return (naam, contact);
        }


        private void Contracten()
        {
            Console.WriteLine("1) Get contracten");
            Console.WriteLine("2) Maak contract");
            Console.WriteLine("3) Annuleer contract");

            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    GetContracten();
                    break;
                case 2:
                    MaakContract();
                    break;
                case 3:
                    AnnuleerContract();
                    break;
            }
        }

        private void GetContracten()
        {
            Console.WriteLine("1) contract by id");
            Console.WriteLine("2) contracts by period");
            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    Console.Write("contract ID: ");
                    string id = Console.ReadLine();
                    Huurcontract contract = _beheerContracten.GeefContract(id);
                    PrintLine();
                    Console.WriteLine(contract.Huis);
                    Console.WriteLine(contract.Huurder);
                    Console.WriteLine(contract.Huurperiode);
                    PrintLine();
                    break;
                case "2":
                    Console.WriteLine("BeginDatum: ");
                    DateTime start = DateTime.Parse(Console.ReadLine());
                    Console.WriteLine("Einddatum: ");
                    string enddate = Console.ReadLine();
                    List<Huurcontract> list;


                    if (enddate == string.Empty)
                    {
                        list = _beheerContracten.GeefContracten(start, null);
                    }
                    else
                    {
                        DateTime end = DateTime.Parse(enddate);
                        list = _beheerContracten.GeefContracten(start, end);
                    }

                    if (list.Count == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("No contracts found");
                        Console.ForegroundColor = ConsoleColor.Green;
                        break;
                    }

                    foreach (Huurcontract c in list)
                    {
                        PrintLine();
                        Console.WriteLine(c.Huis);
                        Console.WriteLine(c.Huurder);
                        Console.WriteLine(c.Huurperiode);
                        PrintLine();
                    }

                    break;
            }
        }
        private void MaakContract()
        {
            Console.Write("Contract ID: ");
            string id = Console.ReadLine();

            Console.Write("huurperiode startdatum: ");
            DateTime start = DateTime.Parse(Console.ReadLine());

            Console.Write("Aantal dagen: ");
            int aantaldagen = int.Parse(Console.ReadLine());

            Console.WriteLine("huurder: ");
            Console.WriteLine("1) Bestaande huurder");
            Console.WriteLine("2) Nieuwe huurder");
            int huurderchoice = int.Parse(Console.ReadLine());

            Huurder huurder = null;

            switch (huurderchoice)
            {

                case 1:
                    Console.Write("huurder id: ");
                    int huurderid = int.Parse(Console.ReadLine());
                    huurder = _beheerHuurders.GeefHuurder(huurderid);
                    break;
                case 2:
                    (string, Contactgegevens) huurderinfo = AddHuurder();
                    huurder = new(huurderinfo.Item1, huurderinfo.Item2);
                    break;
            }

            Huis huis = null;
            bool houseNotValid = true;
            while (houseNotValid)
            {
                Console.WriteLine("huis: ");
                Console.WriteLine("1) Bestaand huis");
                Console.WriteLine("2) Nieuw huis");
                int huischoice = int.Parse(Console.ReadLine());


                switch (huischoice)
                {
                    case 1:
                        Console.Write("huis id:");
                        int huisid = int.Parse(Console.ReadLine());

                        huis = _beheerHuizen.GeefHuis(huisid);
                        if (huis is not null && huis.Actief)
                        {
                            houseNotValid = false;
                        }
                        else
                        {
                            SpecialMessage("dit huis is niet verhuurbaar");
                        }
                        break;
                    case 2:
                        (string, int, string, string, string) huisinfo = AddHuis();
                        huis = new(huisinfo.Item1, huisinfo.Item2, new Park(huisinfo.Item3, huisinfo.Item4, huisinfo.Item5));
                        houseNotValid = false;
                        break;
                }
            }


            _beheerContracten.MaakContract(id, new Huurperiode(start, aantaldagen), huurder, huis);
            SpecialMessage("added contract");

        }

        private void AnnuleerContract()
        {
            Console.Write("contract id: ");
            string id = Console.ReadLine();

            Huurcontract contract = _beheerContracten.GeefContract(id);
            _beheerContracten.AnnuleerContract(contract);
            SpecialMessage("cancelled");
        }



        private void PrintLine()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(new String('_', 20), ConsoleColor.Blue);
            Console.ForegroundColor = ConsoleColor.Green;
        }

        private void PrintError(Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("ERROR: ");
            Console.WriteLine(ex.Message);
            Console.ForegroundColor = ConsoleColor.Green;
        }

        private void SpecialMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.Green;
        }

    }
}
