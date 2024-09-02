using System.Diagnostics;
using System.Text;

class AsciiTable
{
	private int xPosition;
	private string[] headers;
	private int[] headerWidths;
	private int totalWidth;

	public AsciiTable(string[] headings, int[] weights, int xPosition, int width)
	{
		// TODO: Don't use `this`
		totalWidth = width;
		this.xPosition = xPosition;

		headers = headings;
		headerWidths = CalculateHeaderWidths(weights, totalWidth);
		DrawHeaders();
	}

	// Draw an ascii table in the centre of the screen
	public AsciiTable(string[] headings, int[] weights)
	{
		totalWidth = Console.WindowWidth - (Console.WindowWidth / 5);
		xPosition = (Console.WindowWidth - totalWidth) / 2;

		headers = headings;
		headerWidths = CalculateHeaderWidths(weights, totalWidth);
		DrawHeaders();
	}

	// Draw an ascii table in the centre of the screen with default
	// weights of all being 1
	public AsciiTable(params string[] headings)
	{
		// TODO: Don't rewrite
		totalWidth = Console.WindowWidth - (Console.WindowWidth / 5);
		xPosition = (Console.WindowWidth - totalWidth) / 2;

		headers = headings;
		int[] weights = Enumerable.Repeat(1, headings.Length).ToArray();
		headerWidths = CalculateHeaderWidths(weights, totalWidth);
		DrawHeaders();
	}

	public void AddRow(params string[] data)
	{
		Draw(Populate(Slice('│', ' ', '│', '│'), data));
	}

	public void End()
	{
		Draw(Slice('└', '─', '┴', '┘'));
	}

	private int[] CalculateHeaderWidths(int[] headerWeights, int totalWidth)
	{
		int[] headerWidths = new int[headerWeights.Length];
		float totalWeight = headerWeights.Sum();

		// Loop through every weight that we need
		// to calculate
		for (int i = 0; i < headerWeights.Length; i++)
		{
			// Get a 0.x to determine how much space
			// the width of this header should take up
			float percentage = headerWeights[i] / totalWeight;

			// Turn the percentage into a width
			int currentWidth = (int)(percentage * totalWidth);
			headerWidths[i] = currentWidth;
		}

		// Give back the list of widths
		// TODO: Check for if this exceeds totalWidth (idk if it might)
		return headerWidths;
	}

	private void DrawHeaders()
	{
		Draw(Slice('┌', '─', '┬', '┐'));
		Draw(Populate(Slice('│', ' ', '│', '│'), headers));
		Draw(Slice('╞', '═', '╪', '╡'));
	}

	private StringBuilder Slice(char left, char middle, char separator, char right)
	{
		//? using a string builder because it lets me
		//? change stuff at an index (don't need to use .Insert)
		StringBuilder stringBuilder = new StringBuilder();

		// Make the initial line thing
		//? -2 is to account for the left and right characters
		stringBuilder.Append(left);
		stringBuilder.Append(middle, totalWidth - 2);
		stringBuilder.Append(right);

		// Go back and add in all the separators
		// over the top of the clean line
		int x = headerWidths[0];
		for (int i = 1; i < headers.Length; i++)
		{
			// Set the current character to be
			// a separator one
			stringBuilder[x] = separator;

			// Increase the X for drawing the
			// next header thingy
			x += headerWidths[i];
		}
		
		// Give back the string builder so it can
		// be edited by something else or drawn
		return stringBuilder;
	}

	private StringBuilder Populate(StringBuilder stringBuilder, string[] data)
	{
		// Loop over every bit of data
		//? 2 because there is 1 character padding on the left, and
		//? also to skip over the border on the left
		int x = 2;
		for (int i = 0; i < data.Length; i++)
		{
			// First check for if the data can fit
			// in the space given to it. If it can't
			// then chuck a '...' on the end of it 
			if (data[i].Length > headerWidths[i])
			{
				//? 4 because 1 character padding on the right, and 3 ...s
				Debug.WriteLine(data[i]);
				data[i] = data[i].Substring(0, headerWidths[i] - 6) + "...";
				Debug.WriteLine(data[i]);
			}

			// Get rid of the characters we're about to replace
			// and replace them with the new stuff
			stringBuilder.Remove(x, data[i].Length);
			stringBuilder.Insert(x, data[i]);

			// Increase the x for the next header
			x += headerWidths[i];
		}

		// Give back our modified string builder thing
		return stringBuilder;
	}

	// Draw at the x position
	private void Draw(StringBuilder text)
	{
		Console.CursorLeft = xPosition;
		Console.WriteLine(text);
	}
}