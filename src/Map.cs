using System.Text;

class Map
{
	private static string[] map;
	private static int mapWidth;
	private static int mapHeight;

	public static void LoadMap()
	{
		// Let us use the fancy symbols and whatnot
		Console.OutputEncoding = Encoding.UTF8;

		// Load in the map
		map = File.ReadAllLines("./map.txt");

		// Set the width and height
		mapWidth = map.OrderBy(line => line.Length).First().Length;
		mapHeight = map.Length;
	}

	public static void DisplayMap()
	{
		// Get the X position that we need to start drawing at
		// based on the width of the map and the console
		int drawingX = (Console.WindowWidth - mapWidth) / 2;

		// Loop over every character/coordinate in the map
		for (int y = 0; y < mapHeight; y++)
		{
			// Move over to the correct x offset so that
			// we can draw the map in the centre of the screen
			Console.CursorLeft = drawingX;

			for (int x = 0; x < mapWidth; x++)
			{
				// Get the color based on if its day or
				// night for the current area then draw it
				Console.ForegroundColor = GetColorFromTerminatorLine(x, y);

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

	public static int GetHeightFromTerminatorLine(int x)
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

	public static ConsoleColor GetColorFromTerminatorLine(int x, int y)
	{
		// Check for if the current position is inside or
		// outside the terminator line (day or right)
		int lineY = GetHeightFromTerminatorLine(x);

		// If we're blow the line then its night
		if (y <= lineY) return ConsoleColor.DarkGray;

		// Otherwise its day
		return ConsoleColor.Gray;
	}
}