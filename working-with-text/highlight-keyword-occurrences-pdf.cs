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

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Create a case‑insensitive regex for the keyword
            Regex regex = new Regex(keyword, RegexOptions.IgnoreCase);

            // Create a TextFragmentAbsorber that uses the regex
            TextFragmentAbsorber absorber = new TextFragmentAbsorber(regex);

            // Search the entire document
            doc.Pages.Accept(absorber);

            // Highlight each found fragment with a background color
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                fragment.TextState.BackgroundColor = Aspose.Pdf.Color.Yellow;
                // Optional: change foreground color as well
                // fragment.TextState.ForegroundColor = Aspose.Pdf.Color.Red;
            }

            // Save the modified PDF (lifecycle rule: save inside using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Highlighted PDF saved to '{outputPath}'.");
    }
}