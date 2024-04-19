using System;

namespace Prototype
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
        private const string _littleCopy = "Little copy";
        private const string _deepCopy = "Deep copy";

        public Client()
        {
            Input();
        }

        private void Input()
        {
            Person person = new Person();

            Console.WriteLine($"Select a type of copy({_littleCopy} or {_deepCopy}):");

            string typeOfCopy = Console.ReadLine();

            Person resultPerson;

            if (typeOfCopy == _deepCopy)
            {
                try
                {
                    Random random = new Random();

                    Console.WriteLine("Write your age:");

                    int age = Convert.ToInt32(Console.ReadLine());

                    Console.WriteLine("Write your be born date:");

                    int beBornDate = Convert.ToInt32(Console.ReadLine());

                    person.SwitchPersonInfo(age, beBornDate, random.Next());

                    resultPerson = person.DeepCopy();
                }
                catch
                {
                    Console.WriteLine("I dont't understed this information");
                    return;
                }
            }
            else if (typeOfCopy == _littleCopy)
                resultPerson = person.LittleCopy();
            else
                return;

            Console.WriteLine(resultPerson.GetInfo());
        }
    }

    public class Person
    {
        private int _age = 0;
        private int _beBornDate = 0;

        private IdInfo _myId = new IdInfo();

        public void SwitchPersonInfo(int age, int beBornDate, int id)
        {
            _age = age;
            _beBornDate = beBornDate;
            _myId.SwitchPersonId(id);
        }

        public string GetInfo()
        {
            return $"Age: {_age}, be born date: {_beBornDate}, id: {_myId._personId}";
        }

        public Person DeepCopy()
        {
            Person person = new Person();
            person.SwitchPersonInfo(_age, _beBornDate, _myId._personId);

            return person;
        }

        public Person LittleCopy()
        {
            return new Person();
        }
    }

    public class IdInfo
    {
        public int _personId { get; private set; }

        public void SwitchPersonId(int newPersonId)
        {
            _personId = newPersonId;
        }
    }
}
