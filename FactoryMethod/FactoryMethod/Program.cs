using System;

namespace FactoryMethod
{
    public class Program
    {
        private const string _casual = "Casual";
        private const string _adventure = "Adventure";

        public static void Main(string[] args)
        {
            Console.WriteLine($"Select type of game({_casual} or {_adventure})");

            string typeOfGame = Console.ReadLine();

            Creator creator;

            if (typeOfGame == _casual)
                creator = new CreateCasualGame();
            else if (typeOfGame == _adventure)
                creator = new CreateAdventureGame();
            else
                return;

            creator.CreateGame().Play();
        }
    }

    public abstract class Creator
    {
        public abstract IGame CreateGame();
    }

    public class CreateCasualGame : Creator
    {
        public override IGame CreateGame()
        {
            return new CasualGame();
        }
    }

    public class CreateAdventureGame : Creator
    {
        public override IGame CreateGame()
        {
            return new AdventureGame();
        }
    }

    public interface IGame
    {
        public void Play();
    }

    public class AdventureGame : IGame
    {
        public void Play()
        {
            Console.WriteLine("Adventure game was played");
        }
    }

    public class CasualGame : IGame
    {
        public void Play()
        {
            Console.WriteLine("Casual game was played");
        }
    }
}
