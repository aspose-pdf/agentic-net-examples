using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string phrase    = "specific phrase";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate through each page and use TextFragmentAbsorber to locate the phrase
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                var page = doc.Pages[pageIndex];

                // TextFragmentAbsorber extracts text fragments from the current page
                var absorber = new TextFragmentAbsorber();
                absorber.ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure);
                page.Accept(absorber);

                foreach (TextFragment fragment in absorber.TextFragments)
                {
                    if (fragment.Text != null && fragment.Text.IndexOf(phrase, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        // Use the fragment's rectangle to obtain coordinates (lower‑left corner)
                        double x = fragment.Rectangle.LLX; // left‑most X coordinate
                        double y = fragment.Rectangle.LLY; // lower‑most Y coordinate

                        Console.WriteLine($"Found on page {pageIndex} at ({x}, {y}): \"{fragment.Text}\"");
                    }
                }
            }
        }
    }
}
