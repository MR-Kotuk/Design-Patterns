using System;

namespace Singleton
{
    public class Program
    {
        public static void Main()
        {
            Game game1 = Game.GetGame();
            Game game2 = Game.GetGame();

            if (game1 == game2)
                Console.WriteLine("Singleton works, both variables contain the same instance :)");
            else
                Console.WriteLine("Singleton failed, variables contain different instances. :(");
        }
    }

    public class Game
    {
        private static Game _game;

        private Game() { }

        public static Game GetGame()
        {
            if (_game == null)
                _game = new Game();

            return _game;
        }

        public void Play()
        {
            Console.WriteLine("The game has been played");
        }
    }
}
