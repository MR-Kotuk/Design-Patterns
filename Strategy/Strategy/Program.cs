using System.Collections.Generic;
using System;

namespace Strategy
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
        private const string _walk = "Walk";
        private const string _bicycle = "Bicycle";
        private const string _car = "Car";

        private Dictionary<string, IRoadMap> _roadMaps = new Dictionary<string, IRoadMap>
        {
            {_walk, new WalkMap() },
            {_bicycle, new BicycleMap() },
            {_car, new CarMap() }
        };

        public Client()
        {
            Input();
        }

        public void Input()
        {
            Navigator navigator = new Navigator(new WalkMap());

            while (true)
            {
                Console.Clear();

                Console.WriteLine($"Choose your transport({_walk}/{_bicycle}/{_car}):");

                try
                {
                    navigator.SwitchRoadMap(_roadMaps[Console.ReadLine()]);
                    navigator.GenerateMap();
                }
                catch
                {
                    Console.WriteLine("Error :(!!!\nUnknown transport!");
                }

                Console.ReadKey();
            }
        }
    }

    public class Navigator
    {
        private IRoadMap _roadMap = new WalkMap();

        public Navigator(IRoadMap roadMap)
        {
            SwitchRoadMap(roadMap);
        }

        public void SwitchRoadMap(IRoadMap roadMap)
        {
            Console.WriteLine($"Road map has been switched from: {_roadMap.GetType().Name}, to: {roadMap.GetType().Name}");

            _roadMap = roadMap;
        }

        public void GenerateMap()
        {
            Console.WriteLine(_roadMap.GenerateMap());
        }
    }

    public interface IRoadMap
    {
        public string GenerateMap();
    }

    public class WalkMap : IRoadMap
    {
        public string GenerateMap()
        {
            return "The walk map has been generated";
        }
    }

    public class BicycleMap : IRoadMap
    {
        public string GenerateMap()
        {
            return "The bicycle map has been generated";
        }
    }

    public class CarMap : IRoadMap
    {
        public string GenerateMap()
        {
            return "The car map has been generated";
        }
    }
}
