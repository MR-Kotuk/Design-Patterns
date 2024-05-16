using System.Collections.Generic;
using System;

namespace Memento
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
        private const string _undoOperation = "UNDO";
        private const string _showHistory = "SHOW HISTORY";

        public Client()
        {
            Input();
        }

        private void Input()
        {
            Redactor redactor = new Redactor();
            CareTaker careTaker = new CareTaker(redactor);

            while (true)
            {
                Console.WriteLine($"Write any operation, other operations: {_undoOperation}, {_showHistory}");

                string line = Console.ReadLine();

                if (line == _undoOperation)
                    careTaker.Undo();
                else if (line == _showHistory)
                    careTaker.ShowHistory();
                else
                {
                    redactor.DoAnyOperation(line);
                    careTaker.Backup();
                }

                Console.WriteLine();
            }
        }
    }

    public class Redactor
    {
        private string _state;

        public IMemento Save()
        {
            return new RedactorStepMemento(_state);
        }

        public void Restore(IMemento memento)
        {
            if (memento is RedactorStepMemento)
            {
                _state = memento.GetState();
                Console.WriteLine($"Switched state to {_state}");
            }
            else
                Console.WriteLine("Error :(\nUncorrect memento");
        }

        public void DoAnyOperation(string operation)
        {
            _state = operation;
            Console.WriteLine($"Redactor {operation}");
        }
    }

    public interface IMemento
    {
        public string GetState();

        public string GetName();

        public DateTime GetDateTime();
    }

    public class RedactorStepMemento : IMemento
    {
        private string _state;

        private DateTime _dataTime;

        public RedactorStepMemento(string state)
        {
            _state = state;
            _dataTime = DateTime.Now;
        }

        public string GetState()
        {
            return _state;
        }

        public string GetName()
        {
            return $"Date: {_dataTime}\n    State: {_state}";
        }

        public DateTime GetDateTime()
        {
            return _dataTime;
        }
    }

    public class CareTaker
    {
        private Redactor _redactor;

        private List<IMemento> _mementos = new List<IMemento>();

        public CareTaker(Redactor redactor)
        {
            _redactor = redactor;
        }

        public void Backup()
        {
            Console.WriteLine("Saving...");
            _mementos.Add(_redactor.Save());
        }

        public void Undo()
        {
            if (_mementos.Count == 0)
                return;

            IMemento memento = _mementos[_mementos.Count - 1];
            _mementos.Remove(memento);

            Console.WriteLine($"Restoring to: {memento.GetName()}");

            _redactor.Restore(memento);
        }

        public void ShowHistory()
        {
            foreach (IMemento memento in _mementos)
            {
                Console.WriteLine(memento.GetName());
            }
        }
    }
}
