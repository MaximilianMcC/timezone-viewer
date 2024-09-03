class Clock
{
	private static Timer timer;

	public static void DisplayLive(int y)
	{
		// Get the number of seconds until the next minute rolls over
		//? 60 because theres 60 seconds in a minute
		int nextSecond = 60 - DateTime.Now.Second;
		TimeSpan startTime = TimeSpan.FromSeconds(nextSecond);

		// Get how often to display everything (every minute)
		TimeSpan repeat = TimeSpan.FromMinutes(1);

		// Set the timer to start when the next minute starts,
		// the repeat every single minute after that point
		timer = new Timer(state => Display(y), null, startTime, repeat);

		// Run the method initially so that we don't have
		// to wait for the next minute to roll over
		Display(y);
	}

	public static void Display(int y)
	{
		// Set the Y to the specified one
		Console.CursorTop = y;

		// Show the map
		Map.DisplayMap();

		// If there are people to display,
		// then display them in a table
		if (DataHandler.People.Count > 0)
		{
			Console.WriteLine();
			Table.DisplayTimes();
		}
	}
}