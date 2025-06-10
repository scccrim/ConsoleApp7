using System;
using System.Globalization;
using System.Threading;

namespace DigitalClockApp
{
    internal class AlarmHandler
    {
        public DateTime? AlarmTime { get; private set; }
        private bool alarmTriggered = false;
        private bool stopAlarm = false;

        public AlarmHandler()
        {
            AlarmTime = null;
        }

        public void SetAlarm()
        {
            Console.Clear();
            Console.Write("Zadejte čas budíku (HH:mm):\n");
            if (DateTime.TryParseExact(Console.ReadLine(), "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime alarm))
            {
                AlarmTime = alarm;
                alarmTriggered = false;
                stopAlarm = false;
                Console.WriteLine("Budík nastaven na {0:HH:mm}.", alarm);
                Console.WriteLine("Čeká se na spuštění budíku. Stiskněte 'X' pro zrušení.");

                Thread listenerThread = new Thread(ListenForCancelBeforeTrigger);
                listenerThread.Start();

                while (!alarmTriggered && !stopAlarm)
                {
                    DateTime now = DateTime.Now;
                    if (now.Hour == AlarmTime.Value.Hour && now.Minute == AlarmTime.Value.Minute)
                    {
                        alarmTriggered = true;
                        break;
                    }
                    Thread.Sleep(1000);
                }

                if (stopAlarm)
                {
                    Console.WriteLine("Budík byl zrušen.");
                    Thread.Sleep(1500);
                    return;
                }

                StartBeeping();
            }
            else
            {
                Console.WriteLine("Neplatný formát času.");
                Thread.Sleep(1500);
            }
        }

        private void ListenForCancelBeforeTrigger()
        {
            while (!alarmTriggered && !stopAlarm)
            {
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true);
                    if (key.Key == ConsoleKey.X)
                    {
                        stopAlarm = true;
                    }
                }
                Thread.Sleep(100);
            }
        }

        private void StartBeeping()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("!!! Budík zvoní !!!");
            Console.WriteLine("Stiskněte klávesu 'X' pro zastavení...");
            Console.ResetColor();

            Thread keyListener = new Thread(() =>
            {
                while (!stopAlarm)
                {
                    if (Console.KeyAvailable)
                    {
                        var key = Console.ReadKey(true);
                        if (key.Key == ConsoleKey.X)
                        {
                            stopAlarm = true;
                        }
                    }
                    Thread.Sleep(100);
                }
            });
            keyListener.IsBackground = true;
            keyListener.Start();

            while (!stopAlarm)
            {
                Console.Beep(1000, 500);
                Thread.Sleep(300);
            }

            Console.Clear();
            Console.WriteLine("Budík zastaven. Návrat do menu...");
            AlarmTime = null;
            Thread.Sleep(1500);
        }
    }
}
