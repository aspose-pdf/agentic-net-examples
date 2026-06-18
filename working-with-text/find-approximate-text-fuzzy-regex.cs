using System;
using System.IO;
using System.Text.RegularExpressions;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string targetPhrase = "example phrase";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (wrapped in using for proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Enable regular‑expression mode for fuzzy matching
            TextSearchOptions searchOptions = new TextSearchOptions(isRegularExpressionUsed: true)
            {
                UseFontEngineEncoding = true
            };

            // Build a simple fuzzy regex that allows any non‑letter characters between the letters of the phrase
            string fuzzyPattern = BuildFuzzyPattern(targetPhrase);

            // Create a TextFragmentAbsorber with the pattern and options
            TextFragmentAbsorber absorber = new TextFragmentAbsorber(fuzzyPattern, searchOptions);
            // Search all pages of the document
            doc.Pages.Accept(absorber);

            // Highlight each found fragment (optional)
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                fragment.TextState.BackgroundColor = Color.Yellow;
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }

    // Helper to convert a phrase into a fuzzy regex (e.g., "e\W*?x\W*?a…")
    private static string BuildFuzzyPattern(string phrase)
    {
        // Escape any regex meta‑characters in the phrase first
        string escaped = Regex.Escape(phrase);
        // Insert "\W*?" between each character to allow non‑letter separators
        var pattern = "";
        foreach (char c in escaped)
        {
            pattern += c + "\\W*?";
        }
        return pattern;
    }
}