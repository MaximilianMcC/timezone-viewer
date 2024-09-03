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
		int initialY = Console.CursorTop;

		// Display everything until the person
		// presses a key to stop
		Clock.DisplayLive(initialY);
		Console.ReadKey(true);
	}
}