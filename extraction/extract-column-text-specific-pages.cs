using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load PDF document with deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // The TextExtractionOptions constructor now requires a TextFormattingMode argument.
            // "Pure" extracts plain text without preserving layout, which is suitable for column extraction.
            var extractionOpts = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure)
            {
                ScaleFactor = 2.0 // widen columns
            };

            // Create a TextAbsorber using the configured options
            var absorber = new TextAbsorber
            {
                ExtractionOptions = extractionOpts
            };

            // Process pages 5 through 7 (1‑based indexing)
            for (int pageNum = 5; pageNum <= 7 && pageNum <= doc.Pages.Count; pageNum++)
            {
                doc.Pages[pageNum].Accept(absorber);
            }

            // Save extracted text to a plain‑text file
            File.WriteAllText(outputPath, absorber.Text ?? string.Empty);
        }

        Console.WriteLine($"Extracted text saved to '{outputPath}'.");
    }
}
