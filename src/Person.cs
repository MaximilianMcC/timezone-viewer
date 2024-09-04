using System.Diagnostics;

class Person
{
	public string Name { get; set; }
	public string Location { get; set; }
	public MapLocation MapLocation { get; set; }
	public string TimeZoneName { get; set; }

	public DateTimeOffset GetCurrentTime()
	{
		// Get bros timezone
		// TODO: Don't get the timezone into each time. make custom parser
		TimeZoneInfo timezone = TimeZoneInfo.FindSystemTimeZoneById(TimeZoneName);
		
		// Get the current UTC time and add the 
		// persons offset to the time for them rn
		//? using DateTimeOffset because DateTime has no timezone info in it
		DateTimeOffset utcTime = DateTimeOffset.Now;
		DateTimeOffset currentTime = TimeZoneInfo.ConvertTime(utcTime, timezone);

		// Return the time
		return currentTime;
	}
}

// TODO: Use random as api to get x and y from timezone or something
// TODO: or make visual input thingy with move arrow keys idk
class MapLocation
{
	// 1 based index (is adjusted when drawing)
	public int X { get; set; } // Column
	public int Y { get; set; } // Line
}