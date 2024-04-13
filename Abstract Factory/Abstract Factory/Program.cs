using System;

namespace AbstractFactory
{
    public class Program
    {
        public static void Main(string[] args)
        {
            new Client();
        }
    }

    public class Client
    {
        private const string _chair = "Chair";
        private const string _table = "Table";
        private const string _sofa = "Sofa";

        private const string _modernStyle = "Modern";
        private const string _victorianStyle = "Victorian";

        private const string _furnitureActionOnSit = "OnSit";
        private const string _furnitureActionHasLegs = "HasLegs";

        public Client()
        {
            InputClient();
        }

        private void InputClient()
        {
            Console.WriteLine($"Select style of furniture {_modernStyle} or {_victorianStyle}:");
            string furnitureStyle = Console.ReadLine();

            Console.WriteLine($"Select type of furniture ({_chair} {_table}, {_sofa}):");
            string furnitureType = Console.ReadLine();

            Console.WriteLine($"Select furniture action {_furnitureActionOnSit} or {_furnitureActionHasLegs}:");
            string furnitureAction = Console.ReadLine();

            if (furnitureStyle == _modernStyle)
            {
                ModernFactory modernFactory = new ModernFactory();
                CreateFurniture(modernFactory, furnitureType, furnitureAction);
            }
            else if (furnitureStyle == _victorianStyle)
            {
                VictorianFactory victorianFactory = new VictorianFactory();
                CreateFurniture(victorianFactory, furnitureType, furnitureAction);
            }
            else
                Console.WriteLine("I don't know this style of furniture");
        }

        private void CreateFurniture(IFurnitureFactory furnitureFactory, string furnitureType, string furnitureAction)
        {
            if (furnitureType == _chair)
            {
                var chair = furnitureFactory.CreateChair();
                CallAction(chair, furnitureAction);
            }
            else if (furnitureType == _table)
            {
                var table = furnitureFactory.CreateTable();
                CallAction(table, furnitureAction);
            }
            else if (furnitureType == _sofa)
            {
                var sofa = furnitureFactory.CreateSofa();
                CallAction(sofa, furnitureAction);
            }
            else
                Console.WriteLine("I don't know this type of furniture");
        }

        private void CallAction(IFurniture furniture, string furnitureAction)
        {
            if (furnitureAction == _furnitureActionOnSit)
                furniture.OnSit();
            else if (furnitureAction == _furnitureActionHasLegs)
                furniture.HasLegs();
            else
                Console.WriteLine("I don't know this action");
        }
    }

    public interface IFurnitureFactory
    {
        public IChair CreateChair();

        public ITable CreateTable();

        public ISofa CreateSofa();
    }

    public class VictorianFactory : IFurnitureFactory
    {
        public IChair CreateChair()
        {
            return new VictorianChair();
        }

        public ITable CreateTable()
        {
            return new VictorianTable();
        }

        public ISofa CreateSofa()
        {
            return new VictorianSofa();
        }
    }

    public class ModernFactory : IFurnitureFactory
    {
        public IChair CreateChair()
        {
            return new ModernChair();
        }

        public ITable CreateTable()
        {
            return new ModernTable();
        }

        public ISofa CreateSofa()
        {
            return new ModernSofa();
        }
    }

    public interface IFurniture
    {
        public void OnSit();

        public void HasLegs();
    }

    public interface IChair : IFurniture { }

    public interface ITable : IFurniture { }

    public interface ISofa : IFurniture {}

    public class VictorianChair : IChair
    {
        public void OnSit()
        {
            Console.WriteLine("Victorian chair sit");
        }

        public void HasLegs()
        {
            Console.WriteLine("Victorian chair has legs");
        }
    }

    public class VictorianTable : ITable
    {
        public void OnSit()
        {
            Console.WriteLine("Victorian table sit");
        }

        public void HasLegs()
        {
            Console.WriteLine("Victorian table has legs");
        }
    }

    public class VictorianSofa : ISofa
    {
        public void OnSit()
        {
            Console.WriteLine("Victorian sofa sit");
        }

        public void HasLegs()
        {
            Console.WriteLine("Victorian sofa has legs");
        }
    }

    public class ModernChair : IChair
    {
        public void OnSit()
        {
            Console.WriteLine("Modern chair sit");
        }

        public void HasLegs()
        {
            Console.WriteLine("Modern chair has legs");
        }
    }

    public class ModernTable : ITable
    {
        public void OnSit()
        {
            Console.WriteLine("Modern table sit");
        }

        public void HasLegs()
        {
            Console.WriteLine("Modern table has legs");
        }
    }

    public class ModernSofa : ISofa
    {
        public void OnSit()
        {
            Console.WriteLine("Modern sofa sit");
        }

        public void HasLegs()
        {
            Console.WriteLine("Modern sofa has legs");
        }
    }
}
