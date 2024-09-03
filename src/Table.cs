class Table
{
	public static void DisplayTimes()
	{
		// Create the table
		AsciiTable table = new AsciiTable(
			new string[] { "Name", "Location", "Time" },
			new int[] { 2, 3, 1 }
		);

		// Add all the data to the table
		foreach (Person person in DataHandler.People)
		{
			// Convert the time to a string
			string time = person.GetCurrentTime().ToString("hh:mmtt").ToLower();
			string[] data = new string[]
			{
				person.Name,
				person.Location,
				time,
			};

			// Add all the other rubbish and whatnot
			table.AddRow(data);
		}
		table.End();
	}
}