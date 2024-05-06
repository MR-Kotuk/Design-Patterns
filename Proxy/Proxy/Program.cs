using System.Collections.Generic;
using System;

namespace Proxy
{
    public class Program
    {
        public static void Main()
        {
            int clientsCount = 10;

            IYoutubeLibrary youtubeLogginer = new YoutubeLogginner(new YoutubeLibrary());

            for (int i = 0; i < clientsCount; i++)
            {
                Console.WriteLine($"User {i}:");
                new Client(youtubeLogginer);
                Console.WriteLine();
            }
        }
    }

    public class Client
    {
        public Client(IYoutubeLibrary youtubeLogginner)
        {
            youtubeLogginner.SeeVideo();
        }
    }

    public interface IYoutubeLibrary
    {
        public void SeeVideo();
    }

    public class YoutubeLibrary : IYoutubeLibrary
    {
        public void SeeVideo()
        {
            Console.WriteLine("YouTube: see video");
        }
    }

    public class YoutubeLogginner : IYoutubeLibrary
    {
        private YoutubeLibrary _youtubeLibrary;

        private Dictionary<string, int> _users = new Dictionary<string, int>();

        public YoutubeLogginner(YoutubeLibrary youtubeLibrary)
        {
            _youtubeLibrary = youtubeLibrary;
        }

        public void SeeVideo()
        {
            if (IsSingIn())
                _youtubeLibrary.SeeVideo();
        }

        private bool IsSingIn()
        {
            try
            {
                Console.Write("Write your name: ");
                string name = Console.ReadLine();

                Console.Write("Write your user ID: ");
                int userID = Convert.ToInt32(Console.ReadLine());

                if (_users.ContainsKey(name) && _users[name] == userID)
                    return true;

                Console.WriteLine("This account is not exist!!!\nYou have log in!");

                if (IsLogIn())
                    return true;

                return false;
            }
            catch
            {
                Console.WriteLine("You write wrong user ID!!!");
                IsSingIn();
                return false;
            }
        }

        private bool IsLogIn()
        {
            Console.Write("Write your name: ");
            string name = Console.ReadLine();

            if (!_users.ContainsKey(name))
            {
                Random random = new Random();

                int userID = random.Next();

                while (_users.ContainsValue(userID))
                    userID = random.Next();

                _users.Add(name, userID);

                Console.WriteLine($"Account was created!\nInfo:\nName: {name}\nUser ID: {userID}");

                return true;
            }
            else
            {
                Console.WriteLine("This name was used for another user!\nTry again!");
                IsLogIn();
            }

            return false;
        }
    }
}
