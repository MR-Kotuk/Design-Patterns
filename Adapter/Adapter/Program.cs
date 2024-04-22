using System;

namespace Adapter
{
    public class Program
    {
        public static void Main()
        {
            new Client();
        }
    }

    public class Client
    {
        private const string _usa = "USA";
        private const string _europe = "Europe";

        public Client()
        {
            Input();
        }

        private void Input()
        {
            Console.WriteLine($"Select country({_usa} or {_europe}): ");

            string country = Console.ReadLine();

            IAdapter adapter;

            if (country == _usa)
                adapter = new Adapter(new USA());
            else if (country == _europe)
                adapter = new Adapter(new Europe());
            else
                return;

            Console.Write(adapter.GetCountryCharger());
        }
    }

    public interface ICountry
    {
        public string MyCharger();
    }

    public interface IAdapter
    {
        public string GetCountryCharger();
    }

    public class Adapter : IAdapter
    {
        private readonly ICountry _country;

        public Adapter(ICountry country)
        {
            _country = country;
        }

        public string GetCountryCharger()
        {
            return _country.MyCharger();
        }
    }

    public class USA : ICountry
    {
        public string MyCharger()
        {
            return "USA Charger";
        }
    }

    public class Europe : ICountry
    {
        public string MyCharger()
        {
            return "Europee Charger";
        }
    }
}
