using System.Collections.Generic;
using System;

namespace Builder
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
        private const string _casualGame = "Casual Game";
        private const string _bigGame = "Big Game";

        public Client()
        {
            ClientInput();
        }

        public void ClientInput()
        {
            Console.WriteLine($"Select type of game({_casualGame} or {_bigGame}):");

            string typeOfGame = Console.ReadLine();

            Director director = new Director();
            GameBuilder builder = new GameBuilder();

            director.SetBuilder(builder);

            if (typeOfGame == _casualGame)
                director.CreateCasualGame();
            else if (typeOfGame == _bigGame)
                director.CreateBigGame();
            else
                return;

            var game = builder.GetGame();

            Console.WriteLine(game.ListParts());
        }
    }

    public class Director
    {
        private IBuilder _builder;

        public void SetBuilder(IBuilder builder)
        {
            _builder = builder;
        }

        public void CreateCasualGame()
        {
            _builder.AddGamePlay();
        }

        public void CreateBigGame()
        {
            _builder.AddGamePlay();
            _builder.AddOpenWorld();
            _builder.AddAdventurs();
        }
    }

    public interface IBuilder
    {
        public void AddOpenWorld();

        public void AddGamePlay();

        public void AddAdventurs();
    }

    public class GameBuilder : IBuilder
    {
        private Game _game = new Game();

        public void AddOpenWorld()
        {
            _game.AddPart("Open World");
        }

        public void AddGamePlay()
        {
            _game.AddPart("Gameplay");
        }

        public void AddAdventurs()
        {
            _game.AddPart("Adventures");
        }

        private void Reset()
        {
            _game = new Game();
        }

        public Game GetGame()
        {
            Game game = _game;

            Reset();

            return game;
        }
    }

    public class Game
    {
        private List<string> _parts = new List<string>();

        public void AddPart(string part)
        {
            if (_parts.Count == 0)
                _parts.Add($"Game parts: {part}");
            else
                _parts.Add($", {part}");
        }

        public string ListParts()
        {
            string result = string.Empty;

            for(int i = 0; i < _parts.Count; i++)
                result += _parts[i];

            return result;
        }
    }
}
