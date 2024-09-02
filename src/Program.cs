class Program
{

	public static void Main(string[] args)
	{
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