using System;

namespace Mediator
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
        public Client()
        {
            Input();
        }

        public void Input()
        {
            Sender sender = new Sender();
            TextField textField = new TextField();

            IMediator mediator = new UIMediator(sender, textField);

            sender.SetMediator(mediator);
            textField.SetMediator(mediator);

            textField.Read();
            sender.Send();
        }
    }

    public interface IMediator
    {
        public void Notify(object sender, string type);
    }

    public class UIElementBase
    {
        protected IMediator Mediator;

        public UIElementBase(IMediator mediator = null)
        {
            SetMediator(mediator);
        }

        public void SetMediator(IMediator mediator)
        {
            Mediator = mediator;
        }
    }

    public class UIMediator : IMediator
    {
        private Sender _sender;
        private TextField _textField;

        private string _message;

        public UIMediator(Sender sender, TextField textField)
        {
            _sender = sender;
            _textField = textField;
        }

        public void Notify(object sender, string type)
        {
            if (type == "Read")
                _message = _textField.Reading();
            else if (type == "Send" && _message != null)
                _sender.Sending(_message);
        }
    }

    public class Sender : UIElementBase
    {
        public void Send()
        {
            Mediator.Notify(this, "Send");
        }

        public void Sending(string message)
        {
            Console.WriteLine("Sending...");
            Console.WriteLine(message);
        }
    }

    public class TextField : UIElementBase
    {
        public void Read()
        {
            Mediator.Notify(this, "Read");
        }

        public string Reading()
        {
            Console.WriteLine("Write something:");
            return Console.ReadLine();
        }
    }
}
