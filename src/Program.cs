class Program
{

	public static void Main(string[] args)
	{
		// First load the JSON stuff
		// and load in the map
		DataHandler.GetTimes();
		Map.LoadMap();

		// Then draw the map, a bit of padding
		// and the table of people & times
		Map.DisplayMap();
		Console.WriteLine();
		Clock.DisplayTimes();

		//! debug
		Console.ReadLine();
	}
}