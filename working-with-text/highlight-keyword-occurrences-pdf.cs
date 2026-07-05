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

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Build a case‑insensitive regular expression for the keyword
            Regex regex = new Regex(Regex.Escape(keyword), RegexOptions.IgnoreCase);

            // Create a TextFragmentAbsorber that uses the regex
            TextFragmentAbsorber absorber = new TextFragmentAbsorber(regex);

            // Search all pages of the document
            doc.Pages.Accept(absorber);

            // Highlight each found text fragment by setting its background color
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                fragment.TextState.BackgroundColor = Aspose.Pdf.Color.Yellow;
            }

            // Save the modified PDF (output format is PDF, no SaveOptions needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Highlighted PDF saved to '{outputPath}'.");
    }
}