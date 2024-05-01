using System.Collections.Generic;
using System.Linq;
using System;

namespace Flyweight
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
        public Client()
        {
            Input();
        }

        public void Input()
        {
            Console.Write("Write your name: ");
            string owner = Console.ReadLine();

            Console.Write("Write your car number: ");
            string number = Console.ReadLine();

            Console.Write("Write your car company: ");
            string company = Console.ReadLine();

            FlyweightFactory factory = new FlyweightFactory(new CarsDatabase());

            Console.WriteLine($"Database:\n{factory.GetListFlyweight()}");

            Console.ReadKey();
            Console.Clear();

            Police police = new Police();

            police.AddCarToDatabase(factory, new Car
            {
                Owner = owner,
                Number = number,
                Company = company
            });

            Console.ReadKey();
            Console.Clear();

            Console.WriteLine(factory.GetListFlyweight());
        }
    }

    public class CarsDatabase
    {
        public List<string> Companies = new List<string>();

        public CarsDatabase()
        {
            Companies.AddRange(new[]
            {
                "Chevrolet", "Mercedes", "BMW",
                "Tesla", "Ford", "Toyota",
                "Honda", "Subaru", "Jeep"
            });
        }
    }

    public class Police
    {
        public void AddCarToDatabase(FlyweightFactory factory, Car car)
        {
            Console.Clear();
            Console.WriteLine("Adding car to database...\n");

            Flyweight flyweight = factory.GetFlyweight(new Car { Company = car.Company });

            Console.WriteLine(flyweight.GetState(car));
        }
    }

    public class Car
    {
        public string Owner;
        public string Number;
        public string Company;

        public string GetInfo()
        {
            return $"Owner: {Owner}\nNumber: {Number}\nCompany: {Company}";
        }
    }

    public class FlyweightFactory
    {
        private List<Tuple<Flyweight, string>> _flyweights = new List<Tuple<Flyweight, string>>();

        private string _space = " ";

        public FlyweightFactory(CarsDatabase carsDatabase)
        {
            foreach (string carCompanies in carsDatabase.Companies)
            {
                Car car = new Car { Company = carCompanies };
                _flyweights.Add(new Tuple<Flyweight, string>(new Flyweight(car), GetInfo(car)));
            }
        }

        public Flyweight GetFlyweight(Car sharedState)
        {
            string info = GetInfo(sharedState);

            if (_flyweights.Where(i => i.Item2 == info).Count() == 0)
            {
                _flyweights.Add(new Tuple<Flyweight, string>(new Flyweight(sharedState), info));
                Console.WriteLine($"FlyweightFactory: Can't find a flyweight, creating new one: {info}");
            }

            return _flyweights.Where(i => i.Item2 == info).FirstOrDefault().Item1;
        }

        public string GetInfo(Car key)
        {
            List<string> elements = new List<string>();

            elements.Add(key.Company);

            if (key.Owner != null && key.Number != null)
            {
                elements.Add(key.Owner);
                elements.Add(key.Number);
            }

            string result = string.Empty;

            for (int i = 0; i < elements.Count; i++)
            {
                if (i != 0)
                    result += _space;

                result += elements[i];
            }

            return result;
        }

        public string GetListFlyweight()
        {
            string list = string.Empty;

            foreach (var flyweight in _flyweights)
                list += $"{flyweight.Item2}\n";

            return list;
        }
    }

    public class Flyweight
    {
        private Car _sharedState;

        public Flyweight(Car car)
        {
            _sharedState = car;
        }

        public string GetState(Car uniqueState)
        {
            return $"Shared state:\n{_sharedState.GetInfo()}\n \nUnique state:\n{uniqueState.GetInfo()}";
        }
    }
}
