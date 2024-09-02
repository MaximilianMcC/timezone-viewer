using System.Diagnostics;

class Person
{
	public string Name { get; set; }
	public string Location { get; set; }
	public MapLocation MapLocation { get; set; }
	public string TimeZoneName { get; set; }

	public DateTime GetCurrentTime()
	{
		// Get bros timezone
		// TODO: Don't get the timezone into each time. make custom parser
		TimeZoneInfo timeZone = TimeZoneInfo.FindSystemTimeZoneById(TimeZoneName);
		
		// Get the current UTC time and add the 
		// persons offset to the time for them rn
		DateTime utcTime = DateTime.UtcNow;
		DateTime currentTime = utcTime + timeZone.BaseUtcOffset;

		// Return the time
		return currentTime;
	}
}

// TODO: Use random as api to get x and y from timezone or something
// TODO: or make visual input thingy with move arrow keys idk
class MapLocation
{
	// 1 based index
	public int X { get; set; } // Column
	public int Y { get; set; } // Line
}