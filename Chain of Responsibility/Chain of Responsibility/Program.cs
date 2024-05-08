using System;

namespace ChainOfResponsibility
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
        public Client()
        {
            Input();
        }

        private void Input()
        {
            Console.Write("Write petrol number: ");

            string petrol = Console.ReadLine();

            IHandler volkswagenGolf = new VolkswagenGolf();
            IHandler fordEscape = new FordEscape();
            IHandler porshe911 = new Porsche911();

            volkswagenGolf.SetNext(fordEscape).SetNext(porshe911);

            Console.Clear();
            Console.WriteLine("Chain: ");

            TryPetrol(volkswagenGolf, petrol);

            Console.ReadKey();
            Console.Clear();

            Console.WriteLine("Subchain: ");

            TryPetrol(fordEscape, petrol);

            Console.ReadKey();
        }

        private void TryPetrol(IHandler handler, string request)
        {
            var result = handler.Handle(request);

            if (result != null)
                Console.WriteLine(result);
            else
                Console.WriteLine($"Noone of this car-chain has petrol num: {request}");
        }
    }

    public interface IHandler
    {
        public IHandler SetNext(IHandler handler);

        public object Handle(object request);
    }

    public abstract class AbstractHandler : IHandler
    {
        private IHandler _nextHandler;

        public IHandler SetNext(IHandler handler)
        {
            _nextHandler = handler;

            return _nextHandler;
        }

        public virtual object Handle(object request)
        {
            if (_nextHandler != null)
                return _nextHandler.Handle(request);
            else
                return null;
        }
    }

    public class VolkswagenGolf : AbstractHandler
    {
        public override object Handle(object request)
        {
            if ((request as string) == "92")
                return $"Volkswagen Golf: my petrol is {request}";
            else
                return base.Handle(request);
        }
    }

    public class FordEscape : AbstractHandler
    {
        public override object Handle(object request)
        {
            if ((request as string) == "95")
                return $"Ford Escape: my petrol is {request}";
            else
                return base.Handle(request);
        }
    }

    public class Porsche911 : AbstractHandler
    {
        public override object Handle(object request)
        {
            if ((request as string) == "97")
                return $"Porsche 911: my petrol is {request}";
            else
                return base.Handle(request);
        }
    }
}
