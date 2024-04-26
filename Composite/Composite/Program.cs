using System.Collections.Generic;
using System;

namespace Composite
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
        private const string _dots = "Dots";
        private const string _square = "Square";
        private const string _circle = "Circle";

        public Client()
        {
            Input();
        }

        private void Input()
        {
            Composite graficComposite = new GraficComposite();

            InputBranches(_dots, graficComposite);
            InputBranches(_square, graficComposite);
            InputBranches(_circle, graficComposite);

            Console.Clear();

            graficComposite.Draw();
        }

        private void InputBranches(string typeOfGrafic, Composite graficComposite)
        {
            try
            {
                Console.WriteLine($"How much {typeOfGrafic} do you want?");

                int graficCount = Convert.ToInt32(Console.ReadLine());

                if (graficCount > 0)
                {
                    Composite composite;

                    if (typeOfGrafic == _dots)
                        composite = new Dot();
                    else if (typeOfGrafic == _square)
                        composite = new Square();
                    else if (typeOfGrafic == _circle)
                        composite = new Circle();
                    else
                        return;

                    AddObjectsToBranch(graficComposite, composite, graficCount);
                }

            }
            catch
            {
                Console.WriteLine("I don't understand this count");
            }

        }

        private void AddObjectsToBranch(Composite branch, Composite objectType, int count)
        {
            for (int i = 0; i < count; i++)
                branch.AddGrafic(objectType);
        }
    }

    public abstract class Composite
    {
        public virtual void AddGrafic(Composite composite)
        {
            Console.WriteLine("Error, adding was filded");
        }

        public virtual void RemoveGrafic(Composite composite)
        {
            Console.WriteLine("Error, removing was filded");
        }

        public virtual bool isComposite()
        {
            return true;
        }

        public virtual void Draw()
        {
            Console.WriteLine("Error, drawing was filded");
        }
    }

    public class GraficComposite : Composite
    {
        protected List<Composite> Composites = new List<Composite>();

        public override void AddGrafic(Composite composite)
        {
            Composites.Add(composite);
        }

        public override void RemoveGrafic(Composite composite)
        {
            if (Composites.Contains(composite))
                Composites.Remove(composite);
        }

        public override bool isComposite()
        {
            return false;
        }

        public override void Draw()
        {
            for (int i = 0; i < Composites.Count; i++)
                Composites[i].Draw();
        }
    }

    public class Dot : Composite
    {
        public override bool isComposite()
        {
            return false;
        }

        public override void Draw()
        {
            Console.WriteLine("Dot was be drawing");
        }
    }

    public class Square : Composite
    {
        public override bool isComposite()
        {
            return false;
        }

        public override void Draw()
        {
            Console.WriteLine("Square was be drawing");
        }
    }

    public class Circle : Composite
    {
        public override bool isComposite()
        {
            return false;
        }

        public override void Draw()
        {
            Console.WriteLine("Circle was be drawing");
        }
    }
}
