using System;

namespace Facade
{
    public class Program
    {
        public static void Main()
        {
            Facade facade = new Facade(new Volkswagen(), new Porshe());

            facade.TurnOnCars();
        }
    }

    public class Facade
    {
        protected Volkswagen Volkswagen;
        protected Porshe Porshe;

        public Facade(Volkswagen volkswagen, Porshe porshe)
        {
            Volkswagen = volkswagen;
            Porshe = porshe;
        }

        public void TurnOnCars()
        {
            Console.WriteLine("Turn on motors: ");
            Volkswagen.TurnOnMotor();
            Porshe.TurnOnMotor();

            Console.WriteLine();

            Console.WriteLine("Ready to drive: ");
            Volkswagen.Drive();
            Porshe.Drive();
        }
    }

    public class Volkswagen
    {
        public void TurnOnMotor()
        {
            Console.WriteLine("Volkswagen: motor turn on");
        }

        public void Drive()
        {
            Console.WriteLine("Volkswagen: drive");
        }
    }

    public class Porshe
    {
        public void TurnOnMotor()
        {
            Console.WriteLine("Porshe: motor turn on");
        }

        public void Drive()
        {
            Console.WriteLine("Porshe: drive");
        }
    }
}
