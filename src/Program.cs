class Program
{
	public static int InitialX;
	public static int InitialY;
	public static int TotalY;

	public static void Main(string[] args)
	{
		// Capture the original cursor
		// position so that we can return
		// to the top later and whatnot
		InitialX = Console.CursorLeft;
		InitialY = Console.CursorTop;

		// First load the JSON stuff
		// and load in the map
		DataHandler.GetTimes();
		Map.LoadMap();

		// Then draw the map and
		// the table of people
		Map.DisplayMap();
		Clock.DisplayTimes();

		//! debug
		Console.ReadLine();
	}
}