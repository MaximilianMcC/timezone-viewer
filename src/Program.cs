class Program
{

	public static void Main(string[] args)
	{
		// Load everything
		DataHandler.GetTimes();
		Map.LoadMap();

		// Draw the map
		Map.DisplayMap();

		// If there aren't any people in the map then
		// don't show them (nothing to show)
		if (DataHandler.People.Count == 0) return; 

		// Draw the map, and a bit of spacing to even it out
		Console.WriteLine();
		Clock.DisplayTimes();
	}
}