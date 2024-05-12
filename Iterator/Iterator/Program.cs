using System.Collections.Generic;
using System.Collections;
using System;

namespace Iterator
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
        private const string _endOfAdding = "END";

        public Client()
        {
            try
            {
                Input();
            }
            catch
            {
                Console.WriteLine("ERROR...");
            }
        }

        private void Input()
        {
            WordCollection wordCollection = new WordCollection();

            AddingElementsToCollection(wordCollection);

            Console.WriteLine("Default:");
            WriteCollectionItems(wordCollection);

            Console.ReadKey();
            Console.Clear();

            wordCollection.Reverse();

            Console.WriteLine("Reversed:");
            WriteCollectionItems(wordCollection);

            Console.ReadKey();
            Console.Clear();

            wordCollection.Reverse();

            Console.WriteLine("Do you want reversed direction(true/false)?");

            if (Convert.ToBoolean(Console.ReadLine()))
                wordCollection.Reverse();

            MoveOfCollection(wordCollection);
        }

        private void MoveOfCollection(WordCollection wordCollection)
        {
            WordOrderIterator wordOrderIterator = new WordOrderIterator(wordCollection, !wordCollection.GetDirection());

            while (true)
            {
                Console.Clear();

                Console.WriteLine("Click Up Arrow to move, and Down Arrow to reset position: ");

                for (int i = 0; i < wordCollection.GetItems().Count; i++)
                {
                    if (i == wordOrderIterator.GetKey())
                        Console.Write(">");

                    Console.WriteLine(wordCollection.GetItems()[i]);
                }

                ConsoleKey consoleKey = Console.ReadKey().Key;

                if (consoleKey == ConsoleKey.UpArrow && !wordOrderIterator.MoveNext())
                    return;
                if (consoleKey == ConsoleKey.DownArrow)
                    wordOrderIterator.Reset();
            }
        }

        private void AddingElementsToCollection(WordCollection wordCollection)
        {
            bool isAdding = true;

            while (isAdding)
            {
                Console.WriteLine($"Write word(if you want to finish adding elements write '{_endOfAdding}'):");

                string line = Console.ReadLine();

                if (line != _endOfAdding)
                    wordCollection.AddItem(line);
                else
                    isAdding = false;

                Console.Clear();
            }
        }

        private void WriteCollectionItems(WordCollection collection)
        {
            foreach (var item in collection)
            {
                Console.WriteLine(item);
            }
        }
    }

    public abstract class Iterator : IEnumerator
    {
        object IEnumerator.Current => Current();

        public abstract bool MoveNext();

        public abstract object Current();

        public abstract int GetKey();

        public abstract void Reset();
    }

    public abstract class WordIterator
    {
        public abstract IEnumerator GetEnumerator();
    }

    public class WordOrderIterator : Iterator
    {
        private WordCollection _collection;

        private int _positionIndex = -1;

        private bool isReverse;

        public WordOrderIterator(WordCollection wordIterator, bool isReverse)
        {
            _collection = wordIterator;
            this.isReverse = isReverse;

            if (isReverse)
                _positionIndex = _collection.GetItems().Count;
        }

        public override object Current()
        {
            return _collection.GetItems()[_positionIndex];
        }

        public override int GetKey()
        {
            return _positionIndex;
        }

        public override bool MoveNext()
        {
            int nextPos = _positionIndex + (isReverse ? -1 : 1);

            if (nextPos < _collection.GetItems().Count && nextPos >= 0)
            {
                _positionIndex = nextPos;
                return true;
            }
            else
                return false;
        }

        public override void Reset()
        {
            _positionIndex = isReverse ? _collection.GetItems().Count - 1 : 0;
        }
    }

    public class WordCollection : WordIterator
    {
        private List<string> _collection = new List<string>();

        private bool isReverse;

        public List<string> GetItems()
        {
            return _collection;
        }

        public void Reverse()
        {
            isReverse = !isReverse;
        }

        public bool GetDirection()
        {
            return !isReverse;
        }

        public void AddItem(string newItem)
        {
            _collection.Add(newItem);
        }

        public override IEnumerator GetEnumerator()
        {
            return new WordOrderIterator(this, isReverse);
        }
    }
}
