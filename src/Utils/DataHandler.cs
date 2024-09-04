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
			string data = JsonSerializer.Serialize(People);

			// Save it to the file
			File.WriteAllText(path, data);
		}

		// Give back the path
		return path;
	}

	public static string[] GetMap()
	{
		// Get the path
		bool fileExists = TryGetPath("map.txt", out string path);
		if (fileExists == false)
		{
			// Write the ascii art map
			// TODO: Don't hardcode somehow (messy)
			string mapFile = "⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣀⣀⣀⣀⣀⡀⠀⡀⣀⢀⣀⣀⣀⣀⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀\n⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣀⡠⠖⠤⠲⣖⣹⡏⡓⠛⠋⠛⠾⠻⠿⣿⣿⣿⣿⣿⣿⣿⣿⠉⠀⠀⠀⠀⠀⠀⠐⠒⠉⠀⠀⠀⠀⠀⠀⢀⣀⠤⠤⠀⠀⠀⠀⣀⣠⣠⣤⣤⡤⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀\n⠤⣀⠀⣰⣶⣶⣶⣶⣶⣤⣶⣶⣼⣧⣿⣿⣿⣶⣽⣷⣬⣽⠟⣟⣷⡦⡀⠀⢻⣿⣿⡿⠿⠟⠛⣃⣀⡀⠀⠀⠀⠀⠀⢀⣤⡶⣶⣶⡤⢤⣠⣠⣭⣤⣶⣴⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣷⣶⣾⣿⣿⣶⣶⣶⣦⣦⣶⣦\n⠀⠀⠁⠚⢿⡿⠛⠛⠛⠻⣿⣿⣿⣿⣿⣿⣿⣿⣿⣏⡁⠀⠁⢶⣾⣩⣄⠀⠀⠙⠛⠁⠀⠀⠀⠀⠁⠀⠀⣀⠀⠰⢾⢿⡏⣘⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠟⠛⠻⢟⣹⠙⠛⠋⠉\n⠀⠀⠀⠈⠀⠀⠀⠀⠀⠀⠀⠹⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣬⣿⣿⣿⠿⣳⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠘⢽⣓⣴⣾⣶⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣷⠲⠀⠀⠘⠋⠀⠀⠀⠀\n⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢸⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠿⠙⠈⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢰⣦⡿⠟⢛⠯⣟⣿⢿⣉⣅⣙⣿⡇⢺⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⣿⡿⠛⠁⡼⠂⠀⠀⠀⠀⠀⠀⠀\n⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠙⢿⣿⣿⣿⣿⣿⣿⣿⡿⠃⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢨⣯⣷⣶⣾⣈⡀⣈⣊⣉⣻⣿⣿⣿⣾⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣏⠈⠣⠴⠚⠃⠀⠀⠀⠀⠀⠀⠀⠀\n⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⠊⢿⣿⡏⠀⠁⢀⣃⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣴⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣝⢿⣿⣷⣙⣝⠛⠻⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⢯⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀\n⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⠛⠻⠶⣿⣄⠀⠋⠒⠐⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣎⣿⡿⠟⠋⠀⠀⠀⢿⡿⠋⠀⠸⢻⣿⣧⠃⠀⢰⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀\n⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠑⠔⣴⣿⣶⣶⣄⡀⠀⠀⠀⠀⠀⠀⠀⠀⠹⢿⣿⡿⢿⣿⣿⣿⣿⣿⣿⣿⣿⣷⡶⠀⠀⠀⠀⠀⠘⠣⠀⠀⠀⡘⣈⠋⠀⢀⡂⢾⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀\n⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢰⣿⣿⣿⣿⣿⣿⣦⣄⣀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠸⣿⣿⣿⣿⣿⣿⡿⠋⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠘⢮⡀⢶⡿⣒⠄⢂⢤⣀⡀⠀⠀⠀⠀⠀⠀⠀⠀\n⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠙⣿⣿⣿⣿⣿⣿⣿⣿⣿⠇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢹⣿⣿⣿⣿⣿⡇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠑⠒⠀⠄⠠⠀⠀⠘⢛⠣⠍⠀⠀⠀⠀⠀⠀\n⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⢻⣿⣿⣿⣿⣿⣿⡟⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢾⣿⣿⣿⣿⡿⠋⢰⡞⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣀⣴⣾⣿⣦⣼⣆⠀⠀⠀⠀⠀⠀⠀\n⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢸⣿⣿⣿⣿⡟⠉⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣿⣿⣿⣿⠃⠀⠙⠃⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢿⣿⣿⣿⣿⣿⣿⣿⣷⠀⠀⠀⠀⠀⠀\n⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣼⣿⣿⣟⠛⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠘⠛⠛⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠸⠛⠋⠉⠙⠻⣿⣿⡟⠀⠀⠀⠀⠠⠀\n⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣿⡿⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠒⠀⠀⠀⠀⣠⠞⠁\n⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢿⣋⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀";
			File.WriteAllText(path, mapFile);
		}

		// Give back the map as an array
		return File.ReadAllLines(path);
	}

	public static void GetTimes()
	{
		// Open the times json to get all of the times
		string json = File.ReadAllText(GetJsonPath());
		People = JsonSerializer.Deserialize<List<Person>>(json);
	}

	public static void SaveTimes()
	{
		// Turn the people to a stringm then write it to the file
		string json = JsonSerializer.Serialize(People);
		File.WriteAllText(GetJsonPath(), json);
	}
}