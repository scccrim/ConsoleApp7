using System;
using System.Threading;

namespace DigitalClockApp
{
    class Program
    {
        static void Main()
        {
            ClockManager clockManager = new ClockManager();
            clockManager.Run();
        }
    }

    class ClockManager
    {
        private TimeZoneHandler timeZoneHandler = new TimeZoneHandler();
        private AlarmHandler alarmHandler = new AlarmHandler();
        private bool running = true;
        private bool forceRefresh = false; // nová proměnná

        public void Run()
        {
            while (running)
            {
                DisplayClock(); // zobraz hodiny vždy jako první

                Console.SetCursorPosition(0, 7); // menu pod hodinami
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("==== MENU ====");
                Console.WriteLine("1. Změna časového pásma");
                Console.WriteLine("2. Nastavení budíku");
                Console.WriteLine("3. Konec");
                Console.Write("Zadejte volbu: ");
                Console.ResetColor();

                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        timeZoneHandler.ChangeTimeZone();
                        break;
                    case "2":
                        alarmHandler.SetAlarm();
                        break;
                    case "3":
                        running = false;
                        break;
                }

                Console.Clear(); // po akci vyčisti a vrať se zpět na začátek cyklu
            }

            Environment.Exit(0);
        }

        private void DisplayClock()
        {
            string[] digitsStandard = new string[]
            {
        " ███ \n█   █\n█   █\n█   █\n ███ ",
        "  █  \n ██  \n  █  \n  █  \n ███ ",
        " ███ \n    █\n ███ \n█    \n█████",
        "████ \n    █\n ███ \n    █\n████ ",
        "█   █\n█   █\n█████\n    █\n    █",
        "█████\n█    \n████ \n    █\n████ ",
        " ███ \n█    \n████ \n█   █\n ███ ",
        "█████\n    █\n   █ \n  █  \n  █  ",
        " ███ \n█   █\n ███ \n█   █\n ███ ",
        " ███ \n█   █\n ████\n    █\n ███ ",
            };

            string[] digitsAlt = new string[]
            {
        "╔═══╗\n║   ║\n║   ║\n║   ║\n╚═══╝",
        "  ║  \n  ║  \n  ║  \n  ║  \n  ║  ",
        "╔═══╗\n    ║\n╔═══╝\n║    \n╚════",
        "╔═══╗\n    ║\n ═══╣\n    ║\n╚═══╝",
        "║   ║\n║   ║\n╚═══╣\n    ║\n    ║",
        "╔════\n║    \n╚═══╗\n    ║\n╚═══╝",
        "╔═══╗\n║    \n╠═══╗\n║   ║\n╚═══╝",
        "════╗\n    ║\n   ╔╝\n  ╔╝ \n  ║  ",
        "╔═══╗\n║   ║\n╠═══╣\n║   ║\n╚═══╝",
        "╔═══╗\n║   ║\n╚═══╣\n    ║\n╚═══╝",
            };

            string colon = "     \n  █  \n     \n  █  \n     ";

            var digits = timeZoneHandler.FontStyle == 0 ? digitsStandard : digitsAlt;

            DateTime currentTime = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneHandler.SelectedTimeZone);
            string timeString = currentTime.ToString("HHmm");

            string[] output = new string[5];
            for (int i = 0; i < 4; i++)
            {
                string digitArt = digits[int.Parse(timeString[i].ToString())];
                string[] lines = digitArt.Split('\n');
                for (int j = 0; j < 5; j++)
                {
                    output[j] += lines[j] + "  ";
                    if (i == 1)
                        output[j] += colon.Split('\n')[j] + "  ";
                }
            }

            Console.SetCursorPosition(0, 0); // vždy na stejné místo
            Console.ForegroundColor = ConsoleColor.Green;
            foreach (string line in output)
            {
                Console.WriteLine(line);
            }
            Console.ResetColor();
        }


    }
}
