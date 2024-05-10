using System;

namespace Command
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
        private const string _yes = "Yes";
        private const string _no = "No";

        private bool isSingIn;

        public Client()
        {
            Input();
        }

        private void Input()
        {
            Console.WriteLine($"Is you registred({_yes}/{_no})?: ");
            string isSingInText = Console.ReadLine();

            if (isSingInText == _yes)
                isSingIn = true;
            else
                isSingIn = false;

            Console.WriteLine("Write your message: ");
            string message = Console.ReadLine();

            Receiver receiver = new Receiver();

            ICommand sendMessage = new DisplaySomeMessage(message);
            ICommand registration = new RegistrationCommand(receiver, "You sing in!", "Error, you don't sing in!!!", isSingIn);

            Invoker invoker = new Invoker();

            invoker.SwitchFirstCommand(registration);

            if (isSingIn)
                invoker.SwitchSecondCommand(sendMessage);

            invoker.StartProgram();
        }
    }

    public interface ICommand
    {
        public void Execute();
    }

    public class DisplaySomeMessage : ICommand
    {
        private string _message;

        public DisplaySomeMessage(string message)
        {
            _message = message;
        }

        public void Execute()
        {
            Console.WriteLine(_message);
        }
    }

    public class RegistrationCommand : ICommand
    {
        private Receiver _receiver;

        private string _messageIfIsSingIn;
        private string _messageIfNoSingIn;

        private bool isSingIn;

        public RegistrationCommand(Receiver receiver, string messageIfSingIn, string messageIfNoSingIn, bool isSingInUser)
        {
            _receiver = receiver;

            _messageIfIsSingIn = messageIfSingIn;
            _messageIfNoSingIn = messageIfNoSingIn;

            isSingIn = isSingInUser;
        }

        public void Execute()
        {
            if (TrySingIn())
                _receiver.DisplayMesaage(_messageIfIsSingIn);
            else
                _receiver.DisplayMessageElse(_messageIfNoSingIn);
        }

        private bool TrySingIn()
        {
            return isSingIn;
        }
    }

    public class Receiver
    {
        public void DisplayMesaage(string message)
        {
            Console.WriteLine(message);
        }

        public void DisplayMessageElse(string message)
        {
            Console.WriteLine(message);
        }
    }

    public class Invoker
    {
        private ICommand _firstCommand;
        private ICommand _secondCommand;

        public void SwitchFirstCommand(ICommand command)
        {
            _firstCommand = command;
        }

        public void SwitchSecondCommand(ICommand command)
        {
            _secondCommand = command;
        }

        public void StartProgram()
        {
            if (_firstCommand != null)
            {
                _firstCommand.Execute();

                if (_secondCommand != null)
                    _secondCommand.Execute();
            }
        }
    }
}
