using System.Text;

class Map
{
	public static string[] MapFile;
	public static int Width;
	public static int Height;

	public static void LoadMap()
	{
		// Let us use the fancy symbols and whatnot
		Console.OutputEncoding = Encoding.UTF8;

		// Load in the map
		MapFile = DataHandler.GetMap();

		// Set the width and height
		Width = MapFile.OrderBy(line => line.Length).First().Length;
		Height = MapFile.Length;
	}

	public static void DisplayMap()
	{
		// Get the X position that we need to start drawing at
		// based on the width of the map and the console
		int drawingX = (Console.WindowWidth - Width) / 2;

		// Loop over every character/coordinate in the map
		for (int y = 0; y < Height; y++)
		{
			// Move over to the correct x offset so that
			// we can draw the map in the centre of the screen
			Console.CursorLeft = drawingX;

			for (int x = 0; x < Width; x++)
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
				Console.Write(MapFile[y][x]);
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
			normalized version:		https://www.desmos.com/calculator/ffrvmrjscn
			dynamic version:		https://www.desmos.com/calculator/czfiuwirqj

			Shout out Eve for helping
		*/

		// Plot the curve thing
		//? -1 makes sure we only go downwards
		double height = -1d * Height;
		
		//? dividing by x scales it to tht width
		//? Width is divided by 2 because ascii isn't a square (rectangle)
		double width = x / (Width / 2d);

		//? GMT offset is added to shift the line over
		//? modulo 2 makes it repeat, and -1 makes it "reflect" so we
		//? we get the entire curve and not just half of it
		double xPosition = ((width + gmtOffset) % 2d) - 1d;

		//? times by 2 because this only plots half the curve
		//? 8 shows the curve. Anything thats a multiple of 2 (2, 4, 6, 8, etc)
		//? will give the same "shape", but the number decides the "strength"
		//? +1 applies the height
		double y = height / (Math.Pow(xPosition * 2d, 8d) + 1d);

		//? Return the y as absolute because we calculate as negative
		//? Using convert and not casting because casting truncates, and convert rounds
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