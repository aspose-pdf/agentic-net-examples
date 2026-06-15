using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPath = "input.pdf";

        // Phrase to search for (case‑sensitive by default)
        const string phrase = "your specific phrase";

        // Verify the file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a TextFragmentAbsorber (inherits from TextAbsorber) for the phrase
            TextFragmentAbsorber absorber = new TextFragmentAbsorber(phrase);

            // Search the phrase on all pages
            doc.Pages.Accept(absorber);

            // If no occurrences were found, inform the user
            if (absorber.TextFragments == null || absorber.TextFragments.Count == 0)
            {
                Console.WriteLine($"Phrase \"{phrase}\" not found in the document.");
                return;
            }

            // Iterate over each found TextFragment and display its details
            Console.WriteLine($"Found {absorber.TextFragments.Count} occurrence(s) of \"{phrase}\":");
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                // Page where the fragment was found
                int pageNumber = fragment.Page.Number;

                // Position of the fragment (baseline coordinates)
                double x = fragment.Position.XIndent;
                double y = fragment.Position.YIndent;

                // Font information (if needed)
                string fontName = fragment.TextState.Font?.FontName ?? "Unknown";
                double fontSize = fragment.TextState.FontSize;

                Console.WriteLine($"Page {pageNumber}: X={x:F2}, Y={y:F2}, Font={fontName}, Size={fontSize}");
            }
        }
    }
}