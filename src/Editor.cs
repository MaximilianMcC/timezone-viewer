using System.Collections.ObjectModel;

class Editor
{
	// TODO: Put a warning saying that its recommended to not use wt for this (might break on map location thing)
	public static void AddPerson()
	{
		// Get all of the persons info
		string name = Input.GetString("Name: ");
		string location = Input.GetString("Location: ");
		string timezoneName = GetTimezone();
		MapLocation mapLocation = GetMapLocation();

		// Make the new person
		Person person = new Person()
		{
			Name = name,
			Location = location,
			TimeZoneName = timezoneName,
			MapLocation = mapLocation
		};

		// Save the person to the JSON
		DataHandler.People.Add(person);
		DataHandler.SaveTimes();
	}

	private static string GetTimezone()
	{
		// First get all of the time zones registered rn
		ReadOnlyCollection<TimeZoneInfo> timezones = TimeZoneInfo.GetSystemTimeZones();

		// Keep trying until we get a valid
		// timezone input string thingy
		while (true)
		{
			// Get the input
			string input = Input.GetString("timezone name: ").ToLower();

			// First check for if bros put in a flawless input
			bool valid = TimeZoneInfo.TryFindSystemTimeZoneById(input, out _);
			if (valid) return input;

			// If they didn't get a flawless input then
			// loop through every timezone and see for if
			// they've guessed close enough
			foreach (TimeZoneInfo timezone in timezones)
			{
				// If >= 40% of the words are correct then
				// say that its good enough and use the
				// timezone thingy as the accepted one
				// TODO: Check for what has the highest percentage, because rn its biased to the index yk
				if (Input.IsCloseMatch(input, timezone.Id.ToLower(), 0.4f)) return timezone.Id;
			}

			// They weren't close at all. Pull a tanty
			// and ask for another input
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine("Couldn't find a timezone with that name");
			Console.ResetColor();
		}
	}

	private static MapLocation GetMapLocation()
	{
		// Store the initial map cursor X and Y positions
		//? Starts in the centre of the map
		MapLocation mapLocation = new MapLocation()
		{
			X = Map.Width / 2,
			Y = Map.Height / 2
		};

		// Store the initial Y position so we
		// can return to it to redraw the map
		int initialY = Console.CursorTop;

		while (true)
		{
			// Print some instructions, and the map
			Console.CursorTop = initialY;
			Input.CentreText("Use arrow keys to move, enter to select\n");
			DrawMapWithCursorSelected(mapLocation);

			// Get the input
			ConsoleKeyInfo input = Console.ReadKey(true);

			// Check for where we wanna move
			if (input.Key == ConsoleKey.LeftArrow) mapLocation.X--;
			if (input.Key == ConsoleKey.RightArrow) mapLocation.X++;
			if (input.Key == ConsoleKey.UpArrow) mapLocation.Y--;
			if (input.Key == ConsoleKey.DownArrow) mapLocation.Y++;

			// If there is overflow the wrap the thingy so the
			// cursor pulls a Pacman and goes on the other side
			if (mapLocation.X > Map.Width) mapLocation.X = 1;
			if (mapLocation.X < 1) mapLocation.X = Map.Width;
			if (mapLocation.Y > Map.Height) mapLocation.Y = 1;
			if (mapLocation.Y < 1) mapLocation.Y = Map.Height;

			// Check for if we pressed enter to
			// submit/lock in the selection rn
			// TODO: Maybe clear the map cursor thing (get rid of it)
			if (input.Key == ConsoleKey.Enter) return mapLocation;
		}
	}

	// TODO: Put this in the map class
	private static void DrawMapWithCursorSelected(MapLocation mapLocation)
	{
		// Get the X position that we need to start drawing at
		// based on the width of the map and the console
		int drawingX = (Console.WindowWidth - Map.Width) / 2;

		// Loop over every character/coordinate in the map. Also
		// hide the cursor because its ugly as to see it jumping around
		// when it draws the map
		Console.CursorVisible = false;
		for (int y = 0; y < Map.Height; y++)
		{
			// Move over to the correct x offset so that
			// we can draw the map in the centre of the screen
			Console.CursorLeft = drawingX;

			for (int x = 0; x < Map.Width; x++)
			{
				// Check for if their coordinates match the
				// current coordinate that we're on rn
				//? +1 because the map doesn't use a zero-based index
				if ((mapLocation.X - 1) == x && (mapLocation.Y - 1 == y))
				{
					// Set the color to show the cursor
					Console.ForegroundColor = ConsoleColor.DarkYellow;
					Console.Write("@");
					Console.ResetColor();
				}
				else Console.Write(Map.MapFile[y][x]); 
			}
			Console.WriteLine();
		}

		// Give bro back the cursor
		Console.CursorVisible = true;
	}
}