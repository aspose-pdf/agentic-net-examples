using System;
using System.IO;
using System.Text.RegularExpressions;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class HighlightKeyword
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "highlighted.pdf";
        const string keyword    = "example"; // keyword to highlight (case‑insensitive)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Build a case‑insensitive regular expression for the keyword
            Regex regex = new Regex(Regex.Escape(keyword), RegexOptions.IgnoreCase);

            // Create a TextFragmentAbsorber that uses the regex
            TextFragmentAbsorber absorber = new TextFragmentAbsorber(regex);

            // Search the whole document (accept absorber for each page)
            foreach (Page page in doc.Pages)
            {
                page.Accept(absorber);
            }

            // Highlight each found fragment by setting its foreground color
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                fragment.TextState.ForegroundColor = Aspose.Pdf.Color.Yellow;
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Keyword highlighted and saved to '{outputPath}'.");
    }
}