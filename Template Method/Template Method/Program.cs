using System;

namespace TemplateMethod
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
        private const string _monster = "Monster";
        private const string _orcs = "Orcs";

        public Client()
        {
            Input();
        }

        public void Input()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"Choose your enemy({_monster}/{_orcs}):");

                string enemy = Console.ReadLine();

                if (enemy == _monster)
                    new MonsterAI().TemplateMethod();
                else if (enemy == _orcs)
                    new OrcsAI().TemplateMethod();
                else
                    Console.WriteLine("Error :(\nUnknown enemy, try again!");

                Console.ReadKey();
            }
        }
    }

    public abstract class AILogic
    {
        public void TemplateMethod()
        {
            BuildUnits();
            Attack();
            CollectsResources();
        }

        public void CollectsResources()
        {
            Console.WriteLine("Collecting resources...");
        }

        public virtual void Attack()
        {
            Console.WriteLine("Attacked");
        }

        public abstract void BuildUnits();
    }

    public class MonsterAI : AILogic
    {
        public override void Attack()
        {
            Console.WriteLine("Moster unique attack");
        }

        public override void BuildUnits()
        {
            Console.WriteLine("Monster builded units");
        }
    }

    public class OrcsAI : AILogic
    {
        public override void BuildUnits()
        {
            Console.WriteLine("Orcs builded units");
        }
    }
}
