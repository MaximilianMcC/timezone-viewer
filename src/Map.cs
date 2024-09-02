using System.Text;

class Map
{
	private static string[] map;
	public static int Width;
	public static int Height;

	public static void LoadMap()
	{
		// Let us use the fancy symbols and whatnot
		Console.OutputEncoding = Encoding.UTF8;

		// Load in the map
		map = File.ReadAllLines("./map.txt");

		Width = map.OrderBy(line => line.Length).First().Length;
		Height = map.Length;
	}

	public static int GetMapX()
	{
		// Find the longest line in the map
		//? +2 is 1 to get us past the length, and 1 for padding
		int longest = map.OrderBy(line => line.Length).First().Length;
		return longest + 2;
	}

	public static void DisplayMap()
	{
		// Loop over every character/coordinate in the map
		for (int y = 0; y < Height; y++)
		{
			for (int x = 0; x < Width; x++)
			{
				// Get the color based on if its day or
				// night for the current area then draw it
				Console.ForegroundColor = GetColorFromTerminatorLine(x, y, Width, Height);

				// Check for if there is any known people and
				// highlight their locations
				foreach (Person person in DataHandler.People)
				{
					// Check for if their coordinates match the
					// current coordinate that we're on rn
					//? +1 because the map doesn't use a zero-based index
					if (!((person.MapLocation.X - 1) == x && (person.MapLocation.Y - 1 == y))) continue;

					// Set the color to represent them rn
					Console.ForegroundColor = ConsoleColor.DarkYellow;
				}

				// Draw the character
				Console.Write(map[y][x]);
			}

			// Move onto the next line
			Console.WriteLine();
		}

		// Return to the original color
		// for drawing other stuff
		Console.ResetColor();
	}

	public static int GetHeightFromTerminatorLine(int x, int mapWidth, int mapHeight)
	{
		// Get the current UTC time, and use that to 
		// adjust the position of the map as the sun
		// goes around the earth or however it works
		//? /12 because from greenwich you can either +12 or -12 since greenwich is in the centre of the world
		double utc = DateTime.UtcNow.TimeOfDay.TotalHours;
		double gmtOffset = utc / 12f;

		/*
			normalized version:		https://www.desmos.com/calculator/giz4rrerrw
			dynamic version:		https://www.desmos.com/calculator/xsmt0asak1

			Shout out Eve for helping
		*/

		// Plot the curve thing
		double height = -1d * mapHeight;
		
		double width = x / (mapWidth / 2d);
		double xPosition = ((width + gmtOffset) % 2d) - 1;

		double y = height / (Math.Pow(xPosition * 2, 8) + 1);
		return Convert.ToInt32(Math.Abs(y));
	}

	public static ConsoleColor GetColorFromTerminatorLine(int x, int y, int mapWidth, int mapHeight)
	{
		// Check for if the current position is inside or
		// outside the terminator line (day or right)
		int lineY = GetHeightFromTerminatorLine(x, mapWidth, mapHeight);

		// If we're blow the line then its night
		if (y <= lineY) return ConsoleColor.DarkGray;

		// Otherwise its day
		return ConsoleColor.Gray;
	}

	//! debug remove
	public static ConsoleColor GetColorFromTerminatorLineDebug(int x, int y, int mapWidth, int mapHeight)
	{
		// Check for if the current position is inside or
		// outside the terminator line (day or right)
		int lineY = GetHeightFromTerminatorLine(x, mapWidth, mapHeight);

		// If we're above the line then its day
		// if (y <= lineY) return ConsoleColor.Green;
		if (y <= lineY) return ConsoleColor.Yellow;

		// If we're not any of that then its night
		return ConsoleColor.Magenta;
	}
}