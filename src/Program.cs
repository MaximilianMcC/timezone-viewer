class Program
{

	public static void Main(string[] args)
	{
		// Load everything and also hide
		// the cursor because its ugly as
		Console.CursorVisible = false;
		DataHandler.GetTimes();
		Map.LoadMap();

		// Get the height that we should redraw
		// everything at when we refresh everything
		// TODO: Mess around with the buffer height to allow running with history
		int initialY = Console.CursorTop;

		// Display everything live
		Clock.DisplayLive(initialY);

		// If bro presses a key then stop
		// updating live and also give them
		// their cursor back
		Console.ReadKey(true);
		Console.CursorVisible = true;
	}
}