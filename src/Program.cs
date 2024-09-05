class Program
{

	public static void Main(string[] args)
	{
		// Load everything
		DataHandler.GetTimes();
		Map.LoadMap();

		// Get the height that we should redraw
		// everything at when we refresh everything
		int initialY = Console.CursorTop;

		// Display everything live
		Console.CursorVisible = false;
		Clock.DisplayLive(initialY);

		// If bro presses a key then stop
		// updating live and also give them
		// their cursor back
		Console.CursorVisible = true;
		Console.ReadKey(true);
	}
}