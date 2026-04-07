using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string phrase = "specific phrase";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a TextFragmentAbsorber that searches for the specified phrase
            TextFragmentAbsorber absorber = new TextFragmentAbsorber(phrase);

            // Use pure text extraction to get reliable coordinates (optional but recommended)
            absorber.ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure);

            // Search the entire document (all pages)
            doc.Pages.Accept(absorber);

            // Report the number of occurrences found
            Console.WriteLine($"Found {absorber.TextFragments.Count} occurrence(s) of \"{phrase}\".");

            // Iterate through each found fragment and display its details
            int idx = 1;
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                // The Rectangle property provides the bounding box of the fragment.
                // LLX/LLY represent the lower‑left corner (baseline X/Y).
                var rect = fragment.Rectangle;
                Console.WriteLine($"[{idx}] Text: \"{fragment.Text}\", Position: (X={rect.LLX}, Y={rect.LLY})");
                idx++;
            }

            // Save the (unchanged) document back to PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processing completed. Output saved to \"{outputPath}\".");
    }
}
