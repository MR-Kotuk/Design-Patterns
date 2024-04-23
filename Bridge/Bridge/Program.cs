using System;

namespace Bridge
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
        private const string _tv = "TV";
        private const string _radio = "Radio";

        private const string _remote = "Remote";
        private const string _advanceRemote = "Advanced Remote";

        private const string _yes = "Yes";
        private const string _no = "No";

        public Client()
        {
            Input();
        }

        private void Input()
        {
            Console.WriteLine($"Select remote ({_remote} or {_advanceRemote}): ");

            string remoteType = Console.ReadLine();

            Console.WriteLine($"Select device ({_tv} or {_radio}): ");

            string deviceType = Console.ReadLine();

            Console.Clear();

            IDevice device;

            if (deviceType == _tv)
                device = new TV();
            else if (deviceType == _radio)
                device = new Radio();
            else
                return;

            if (remoteType == _remote)
            {
                Remote remote = new Remote(device);
                Console.WriteLine($"{deviceType} {remote.GetEnable()}");
            }
            else if (remoteType == _advanceRemote)
            {
                AdvancedRemove remote = new AdvancedRemove(device);

                Console.WriteLine($"Switch device enable ({_yes}/{_no})?");

                string answer = Console.ReadLine();

                if (answer == _yes)
                    remote.SwitchPower();
                else if (answer != _no)
                    return;

                Console.WriteLine($"{deviceType} {remote.GetEnable()}");
            }
            else
                return;
        }
    }

    public interface IDevice
    {
        public string GetEnabled();

        public void SwitchPower();
    }

    public class Remote
    {
        protected IDevice Device;

        public Remote(IDevice device)
        {
            Device = device;
        }

        public string GetEnable()
        {
            return $"enable is {Device.GetEnabled()}";
        }
    }

    public class AdvancedRemove : Remote
    {
        public AdvancedRemove(IDevice device) : base(device)
        {
        }

        public void SwitchDevice(IDevice device)
        {
            Device = device;
        }

        public void SwitchPower()
        {
            Device.SwitchPower();
        }
    }

    public class TV : IDevice
    {
        private bool isOn;

        public string GetEnabled()
        {
            return $"{isOn}";
        }

        public void SwitchPower()
        {
            isOn = !isOn;
        }

    }

    public class Radio : IDevice
    {
        private bool isOn;

        public string GetEnabled()
        {
            return $"{isOn}";
        }

        public void SwitchPower()
        {
            isOn = !isOn;
        }
    }
}