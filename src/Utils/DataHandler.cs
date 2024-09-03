using System.Text.Json;

class DataHandler
{
	public static List<Person> People;

	private static bool TryGetPath(string fileToGet, out string path)
	{
		// Get the app data file for the current program
		const string dataFolderName = "timezone-viewer";
		string appDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
		string basePath = Path.Combine(appDirectory, dataFolderName);

		// Check for if the application path exists. If it doesn't
		// then create a new one
		if (Directory.Exists(basePath) == false) Directory.CreateDirectory(basePath);

		// Get the path to the file we want
		path = Path.Combine(basePath, fileToGet);

		// Check for if the path exists. If it doesn't then return false,
		// otherwise return true. Bro has the path from the out value so
		// they can create and populate the new file and whatnot
		return File.Exists(path);
	}

	private static string GetJsonPath()
	{
		// Get the path
		bool fileExists = TryGetPath("times.json", out string path);
		if (fileExists == false)
		{
			// Create new default data to and chuck it in a string
			People = new List<Person>();
			People.Add(new Person() {
				Name = "Christopher Luxon",
				Location = "Wellington, New Zealand",
				TimeZoneName = "New Zealand Standard Time",
				MapLocation = new MapLocation() { X = 81, Y = 16 }
			});
			string data = JsonSerializer.Serialize(People);

			// Save it to the file
			File.WriteAllText(path, data);
		}

		// Give back the path
		return path;
	}

	public static void GetTimes()
	{
		// Open the times json to get all of the times
		string json = File.ReadAllText(GetJsonPath());
		People = JsonSerializer.Deserialize<List<Person>>(json);
	}

	private static void SaveTimes()
	{
		string json = JsonSerializer.Serialize(People);
		File.WriteAllText(GetJsonPath(), json);
	}
}