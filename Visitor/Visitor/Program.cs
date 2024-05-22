using System;

namespace Visitor
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
        private const string _porshe = "Porshe";
        private const string _volkswagen = "Volkswagen";

        private const string _standartCheck = "Standart";
        private const string _uniqueCheck = "Unique";

        public Client()
        {
            Input();
        }

        public void Input()
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine($"Select your car({_porshe}/{_volkswagen}):");
                string clientCar = Console.ReadLine();

                ICar car;

                if (clientCar == _porshe)
                    car = new Porshe();
                else if (clientCar == _volkswagen)
                    car = new Volkswagen();
                else
                    return;

                Console.WriteLine($"Select your check type({_standartCheck}/{_uniqueCheck}):");
                string check = Console.ReadLine();

                if (check == _standartCheck)
                    car.Accept(new StandartCarVisitor());
                else if (check == _uniqueCheck)
                    car.Accept(new UniqueCarVisitor());
                else
                    return;

                Console.ReadKey();
            }
        }
    }

    public interface ICar
    {
        public void Accept(IVisitor visitor);

        public void GetHasFirstAidKit();
    }

    public interface IVisitor
    {
        public void CheckPorshe(Porshe porshe);
        public void CheckVolkswagen(Volkswagen volkswagen);
    }

    public class StandartCarVisitor : IVisitor
    {
        public void CheckPorshe(Porshe porshe)
        {
            Console.WriteLine("Standart check:");

            porshe.GetHasFirstAidKit();
        }

        public void CheckVolkswagen(Volkswagen volkswagen)
        {
            Console.WriteLine("Standart check:");

            volkswagen.GetHasFirstAidKit();
        }
    }

    public class UniqueCarVisitor : IVisitor
    {
        public void CheckPorshe(Porshe porshe)
        {
            Console.WriteLine("Unique check:");

            porshe.GetHasFirstAidKit();
            porshe.GetHasFireExtinguisher();
        }

        public void CheckVolkswagen(Volkswagen volkswagen)
        {
            Console.WriteLine("Unique check:");

            volkswagen.GetHasFirstAidKit();
            volkswagen.GetHasReflectiveTape();
        }
    }

    public class Porshe : ICar
    {
        public void Accept(IVisitor visitor)
        {
            visitor.CheckPorshe(this);
        }

        public void GetHasFirstAidKit()
        {
            Console.WriteLine("Checking porshe to have first aid kit...");
        }

        public void GetHasFireExtinguisher()
        {
            Console.WriteLine("Checking porshe to have fire extinguisher...");
        }
    }

    public class Volkswagen : ICar
    {
        public void Accept(IVisitor visitor)
        {
            visitor.CheckVolkswagen(this);
        }

        public void GetHasFirstAidKit()
        {
            Console.WriteLine("Checking volkwagen to have first aid kit...");
        }

        public void GetHasReflectiveTape()
        {
            Console.WriteLine("Checking volkwagen to have reflective tape...");
        }
    }
}
