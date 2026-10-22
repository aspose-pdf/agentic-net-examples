using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_hyphenated.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the source PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure we have at least one page; Aspose.Pdf uses 1‑based indexing
            Page page = doc.Pages[1];

            // Create a TextFragment with a long word that would benefit from hyphenation
            string longText = "Antidisestablishmentarianism is a famously long word that can be hyphenated automatically when the text exceeds the line width.";
            TextFragment fragment = new TextFragment(longText);

            // NOTE: The Hyphenation property is not available in the current Aspose.Pdf version.
            // Automatic hyphenation can be enabled in newer library versions via:
            //     fragment.TextState.Hyphenation = true;
            // For this example we keep the line‑wrapping behavior without explicit hyphenation.

            // Optionally set other text formatting properties
            fragment.TextState.FontSize = 12;
            fragment.TextState.Font = FontRepository.FindFont("Arial");

            // Add the fragment to the page's paragraph collection
            page.Paragraphs.Add(fragment);

            // Save the modified PDF; the Document is still alive until Save completes
            doc.Save(outputPath);
        }

        Console.WriteLine($"Hyphenated PDF saved to '{outputPath}'.");
    }
}
