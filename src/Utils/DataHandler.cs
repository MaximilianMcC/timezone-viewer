using System.Text.Json;

class DataHandler
{
	public static List<Person> People;

	private static string GetJsonPath()
	{
		// Get the directory of the program so that we can get
		// the path to the JSON file with a non-relative path
		string directory = AppDomain.CurrentDomain.BaseDirectory;
		string jsonPath = Path.Combine(directory, "times.json");

		// Check for if the JSON file actually exists
		if (File.Exists(jsonPath) == false)
		{
			Console.WriteLine("Couldn't find the JSON file at the path:\n" + jsonPath);
			Environment.Exit(1);
		}

		// Give back the path
		return jsonPath;
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