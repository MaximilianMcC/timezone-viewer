class Person
{
	public string Name { get; set; }
	public string Location { get; set; }
	public int GmtOffset { get; set; }
	public MapLocation MapLocation { get; set; }

	public DateTime GetCurrentTime()
	{
		// Get the current UTC time and add the 
		// persons GMT offset to the time for them rn
		DateTime utcTime = DateTime.UtcNow;
		DateTime currentTime = utcTime.AddHours(GmtOffset);

		// Return the time
		return currentTime;
	}

	public ConsoleColor GetColorBasedOnTime()
	{
		// Get the time
		TimeSpan time = GetCurrentTime().TimeOfDay;

		// Define the sunset and sunrise times
		TimeSpan sunRise = new TimeSpan(6, 0, 0);     //? 6am
		TimeSpan sunSet = new TimeSpan(12 + 6, 0, 0); //? 6pm

		// Check for if its day or not, then return the
		// color based on that
		// TODO: Add more colors depending on different things
		bool day = (time > sunRise && time < sunSet);
		return day ? ConsoleColor.Blue : ConsoleColor.DarkGray;
	}
}

class MapLocation
{
	// 1 based index
	public int X { get; set; } // Column
	public int Y { get; set; } // Line
}