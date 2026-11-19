using System;
using System.IO;
using System.Text.RegularExpressions;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "highlighted.pdf";
        const string keyword    = "example";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // TextSearchOptions constructor argument indicates whether to search in annotations.
            // No IgnoreCase property exists on TextSearchOptions; case‑insensitive search is achieved
            // via a regular expression with RegexOptions.IgnoreCase.
            TextSearchOptions searchOptions = new TextSearchOptions(true);

            // Create a case‑insensitive regular expression for the keyword.
            var keywordRegex = new Regex(Regex.Escape(keyword), RegexOptions.IgnoreCase);

            // Absorb all fragments that match the keyword (case‑insensitive).
            TextFragmentAbsorber absorber = new TextFragmentAbsorber(keywordRegex, searchOptions);
            doc.Pages.Accept(absorber);

            // Highlight each found fragment
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                // Set foreground (text) color
                fragment.TextState.ForegroundColor = Aspose.Pdf.Color.Yellow;
                // Optional: set background color for stronger visual highlight
                fragment.TextState.BackgroundColor = Aspose.Pdf.Color.LightGray;
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Keyword highlighting completed. Output saved to '{outputPath}'.");
    }
}
