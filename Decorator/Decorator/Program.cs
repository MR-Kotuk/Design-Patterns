using System;

namespace Decorator
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
        private const string _instagram = "Instagram";
        private const string _faceBook = "FaceBook";

        private const string _yes = "Yes";
        private const string _no = "No";

        public Client()
        {
            Input();
        }

        private void Input()
        {
            Notifier decorator = new SMS();

            decorator = TryAddNotifier(decorator, new FaceBookDecorator(decorator), _faceBook);
            decorator = TryAddNotifier(decorator, new InstagramDecorator(decorator), _instagram);

            Console.WriteLine("Write a message: ");

            string message = Console.ReadLine();

            Console.Clear();

            decorator.Send(message);
        }

        private Notifier TryAddNotifier(Notifier myDecorator, Notifier newNotifier, string notifierName)
        {
            Console.WriteLine($"Do you want also {notifierName} notifier({_yes}/{_no})?");

            string answer = Console.ReadLine();

            Notifier decorator;

            if (answer == _yes)
                decorator = new Decorator(newNotifier);
            else
                decorator = myDecorator;

            return decorator;
        }
    }

    public abstract class Notifier
    {
        public abstract void Send(string message);
    }

    public class SMS : Notifier
    {
        public override void Send(string message)
        {
            Console.WriteLine($"SMS new notify: {message}");
        }
    }

    public class Decorator : Notifier
    {
        protected Notifier MyNotifier;

        public Decorator(Notifier notifier)
        {
            SetNotifier(notifier);
        }

        public void SetNotifier(Notifier notifier)
        {
            MyNotifier = notifier;
        }

        public override void Send(string message)
        {
            if (MyNotifier != null)
                MyNotifier.Send(message);
            else
                throw new NullReferenceException();
        }
    }

    public class FaceBookDecorator : Decorator
    {
        public FaceBookDecorator(Notifier notifier) : base(notifier)
        {

        }

        public override void Send(string message)
        {
            Console.WriteLine($"FaceBook new notify: {message}");
            base.Send(message);
        }
    }

    public class InstagramDecorator : Decorator
    {
        public InstagramDecorator(Notifier notifier) : base(notifier)
        {

        }

        public override void Send(string message)
        {
            Console.WriteLine($"Instagram new notify: {message}");
            base.Send(message);
        }
    }
}
