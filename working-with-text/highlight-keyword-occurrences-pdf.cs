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
        const string outputPath = "highlighted.pdf";
        const string keyword = "example"; // keyword to search (case‑insensitive)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Build a regex that matches the keyword ignoring case
            Regex regex = new Regex(Regex.Escape(keyword), RegexOptions.IgnoreCase);

            // Create a TextFragmentAbsorber that uses the regex for searching
            TextFragmentAbsorber absorber = new TextFragmentAbsorber(regex);

            // Search the entire document – use the Pages collection's Accept method
            doc.Pages.Accept(absorber);

            // Highlight each found fragment by setting its background color
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                fragment.TextState.BackgroundColor = Aspose.Pdf.Color.Yellow;
            }

            // Save the modified PDF (PDF format, no SaveOptions needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Highlighted PDF saved to '{outputPath}'.");
    }
}
