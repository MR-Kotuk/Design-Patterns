using System;

namespace State
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
        private Document _document;

        public Client()
        {
            Input();
        }

        public void Input()
        {
            _document = new Document(new Draft());

            while (true)
            {
                Console.Clear();
                Console.WriteLine($"Select option(number):\n1. Write context\n2. Switch context\n3. Publish\n4. Return to drafting\n5. Return to moderating");

                string answer = Console.ReadLine();

                if (answer == "1")
                    _document.WriteContext();
                else if (answer == "2")
                    _document.SwitchContext();
                else if (answer == "3")
                    _document.Publich();
                else if (answer == "4")
                    _document.ReturnToDrafting();
                else if (answer == "5")
                    _document.ReturnToModeration();

                Console.ReadKey();
            }
        }
    }

    public class Document
    {
        private DocumentState _state;

        private string _context;

        public Document(DocumentState state)
        {
            SwitchState(state);
        }

        public string GetContext()
        {
            return $"Current context:\n{_context}";
        }

        public void WriteContext()
        {
            _state.WriteCurrentContext();
        }

        public void Publich()
        {
            _state.Publish();
        }

        public void SwitchContext()
        {
            _context = _state.SwitchContext();
        }

        public void ReturnToDrafting()
        {
            _state.ReturnToDrafting();
        }

        public void ReturnToModeration()
        {
            _state.ReturnToModeration();
        }

        public void SwitchState(DocumentState state)
        {
            state.SwitchDocument(this);
            _state = state;

            Console.WriteLine($"State was switched to: {_state}");
        }
    }

    public abstract class DocumentState
    {
        protected Document Document;

        public void SwitchDocument(Document document)
        {
            Document = document;
        }

        public virtual void WriteCurrentContext()
        {
            Console.WriteLine(Document.GetContext());
        }

        public abstract void Publish();

        public virtual void ReturnToDrafting() { }
        public virtual void ReturnToModeration() { }
        public virtual string SwitchContext() { return null; }
    }

    public class Draft : DocumentState
    {
        public override string SwitchContext()
        {
            Console.WriteLine(Document.GetContext());
            Console.WriteLine("Write new context:");

            return Console.ReadLine();
        }

        public override void Publish()
        {
            Document.SwitchState(new Moderation());
        }
    }

    public class Moderation : DocumentState
    {
        public override void ReturnToDrafting()
        {
            Document.SwitchState(new Draft());
        }

        public override void Publish()
        {
            Document.SwitchState(new Publishing());
        }
    }

    public class Publishing : DocumentState
    {
        public override void ReturnToModeration()
        {
            Document.SwitchState(new Moderation());
        }

        public override void ReturnToDrafting()
        {
            Document.SwitchState(new Draft());
        }

        public override void Publish()
        {
            Console.WriteLine("Document is published");
        }
    }
}