using System.Collections.ObjectModel;

class Editor
{
	public static void AddPerson()
	{
		// Get all of the persons info
		string name = Input.GetString("Name: ");
		string location = Input.GetString("Location: ");
		string timezoneName = GetTimezone();
		Console.WriteLine(timezoneName);
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
}