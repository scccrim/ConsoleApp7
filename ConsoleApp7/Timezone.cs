internal class TimeZoneHandler
{
    public TimeZoneInfo SelectedTimeZone { get; private set; }
    private List<TimeZoneInfo> timeZones;

    public int FontStyle { get; private set; } = 0; // 0 = výchozí styl, 1 = alternativní

    public TimeZoneHandler()
    {
        SelectedTimeZone = TimeZoneInfo.Local;
        timeZones = TimeZoneInfo.GetSystemTimeZones().ToList();
    }

    public void ChangeTimeZone()
    {
        Console.WriteLine("Dostupná časová pásma:\n");
        int colWidth = 40;
        int cols = 3;
        int rows = (int)Math.Ceiling(timeZones.Count / (double)cols);

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                int idx = row + col * rows;
                if (idx < timeZones.Count)
                {
                    string name = timeZones[idx].DisplayName;
                    Console.Write(String.Format("{0,2}. {1,-" + colWidth + "} ", idx, name));
                }
            }
            Console.WriteLine();
        }

        Console.Write("\nZadejte číslo časového pásma:\n");
        if (int.TryParse(Console.ReadLine(), out int selectedIndex) && selectedIndex >= 0 && selectedIndex < timeZones.Count)
        {
            SelectedTimeZone = timeZones[selectedIndex];

            // Nastavení stylu "písma" podle výběru (například pouze symbolicky)
            FontStyle = selectedIndex % 2; // lichá = styl 1, sudá = styl 0
        }
    }
}
