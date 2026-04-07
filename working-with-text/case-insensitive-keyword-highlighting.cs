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
        const string outputPath = "highlighted_output.pdf";
        const string keyword    = "example"; // keyword to search (case‑insensitive)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Build a case‑insensitive regular expression for the keyword
            Regex regex = new Regex(Regex.Escape(keyword), RegexOptions.IgnoreCase);

            // Create a TextFragmentAbsorber that uses the regex
            TextFragmentAbsorber absorber = new TextFragmentAbsorber(regex);

            // Search the whole document
            doc.Pages.Accept(absorber);

            // Highlight each found fragment
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                // Set foreground (text) color – use Aspose.Pdf.Color to stay cross‑platform
                fragment.TextState.ForegroundColor = Aspose.Pdf.Color.Yellow;
                // Optionally set background color for stronger emphasis
                fragment.TextState.BackgroundColor = Aspose.Pdf.Color.LightGray;
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Highlighted PDF saved to '{outputPath}'.");
    }
}