using System;

namespace DigitalClockApp
{
    internal class ColorHandler
    {
        public ConsoleColor ClockColor { get; private set; }
        private ConsoleColor[] availableColors = new ConsoleColor[]
        {
            ConsoleColor.Green,
            ConsoleColor.Cyan,
            ConsoleColor.Magenta,
            ConsoleColor.Yellow,
            ConsoleColor.Blue,
            ConsoleColor.White,
            ConsoleColor.DarkGreen,
            ConsoleColor.DarkCyan
        };

        private Random random = new Random();

        public ColorHandler()
        {
            ClockColor = ConsoleColor.Green;
        }

        public void ChangeColor()
        {
            // Původní ruční změna
            Console.Write("Zadejte název barvy (např. Green, Red, Yellow):\n");
            string colorName = Console.ReadLine();
            if (Enum.TryParse(colorName, true, out ConsoleColor newColor))
            {
                ClockColor = newColor;
            }
        }

        public void RandomizeColor()
        {
            // Náhodně vybere jinou barvu
            ClockColor = availableColors[random.Next(availableColors.Length)];
        }
    }
}
