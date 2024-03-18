using System.Collections.Generic;
using System;

namespace Observer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Animations animations = new Animations();
            Sounds sounds = new Sounds();

            Player player = new Player(animations, sounds);

            Console.WriteLine("Choose player state: \n 1. Move          2. Jump          3. Crouch");

            int state;

            bool isState = Int32.TryParse(Console.ReadLine(), out state);

            if (isState && state < 4 && state > 0)
            {
                if (state == 1)
                    player.Move();
                else if (state == 2)
                    player.Jump();
                else if (state == 3)
                    player.Crouch();
            }
            else
                Console.WriteLine("I don't know this player state");

            Console.WriteLine("------------- \n Press Escape to exit, or any other button to continue");
            
            ConsoleKeyInfo key = Console.ReadKey();
            Console.Clear();

            if (key.Key != ConsoleKey.Escape)
                Main(args);
        }
    }

    public interface IObserver
    {
        public void AddSubscriber(ISubscriber subscriber);
        public void RemoveSubscriber(ISubscriber subscriber);
    }

    public interface ISubscriber
    {
        public void Updates(string state);
    }

    public class Observer : IObserver
    {
        private List<ISubscriber> _subscribers = new List<ISubscriber>();

        public void AddSubscriber(ISubscriber subscriber)
        {
            if(_subscribers != null && !_subscribers.Contains(subscriber))
                _subscribers.Add(subscriber);
        }

        public void RemoveSubscriber(ISubscriber subscriber)
        {
            if (_subscribers.Contains(subscriber))
                _subscribers.Remove(subscriber);
        }

        public void Notify(string state)
        {
            foreach (var subsciber in _subscribers)
                subsciber.Updates(state);
        }
    }

    public class Player
    {
        private Observer _observer;

        public Player(Animations animations, Sounds sounds)
        {
            _observer = new Observer();

            _observer.AddSubscriber(animations);
            _observer.AddSubscriber(sounds);
        }

        public void Move() => _observer.Notify("Move");
        public void Jump() => _observer.Notify("Jump");
        public void Crouch() => _observer.Notify("Crouch");
    }

    public class Animations : ISubscriber
    {
        public void Updates(string state)
        {
            Console.WriteLine($"{state} animation was been played");
        }
    }

    public class Sounds : ISubscriber
    {
        public void Updates(string state)
        {
            Console.WriteLine($"{state} sound was been played");
        }
    }
}
