class Input
{
	// TODO: Make this look half decent
	public static string GetString(string prompt)
	{
		// Draw the prompt
		Console.Write(prompt);

		// Get the input
		Console.ForegroundColor = ConsoleColor.DarkYellow;
		string input = Console.ReadLine().Trim();
		Console.ResetColor();

		return input;
	}

	

	public static bool IsCloseMatch(string inputWord, string wordToMatch, float matchPercentage = 0.4f)
	{
		// Store the words, and matches
		string[] words = wordToMatch.Split(" ");
		string[] inputWords = inputWord.Split(" ");
		int matches = 0;

		// Loop over each word in the input and check how many match
		foreach (string currentWord in inputWords)
		{
			if (words.Any(word => word.Contains(currentWord))) matches++;
		}

		// Get the percentage of matches and return
		// if its over the correct threshold thingy
		float percentage = matches / inputWords.Length;
		return percentage >= matchPercentage;
	}
}