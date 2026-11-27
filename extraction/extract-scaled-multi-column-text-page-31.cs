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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the requested page exists (pages are 1‑based)
            if (doc.Pages.Count < 31)
            {
                Console.Error.WriteLine("The document has fewer than 31 pages.");
                return;
            }

            // Configure text extraction options – the constructor now requires a TextFormattingMode value.
            // Pure mode extracts plain text without preserving layout; you can change it to PreserveLayout if needed.
            TextExtractionOptions extractOpts = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure)
            {
                // ScaleFactor reduces the resolution used for OCR‑based extraction; 0.5 = 50%
                ScaleFactor = 0.5
                // Multi‑column detection is handled automatically in recent versions; explicit property removed.
            };

            // Create a TextAbsorber with the configured options
            TextAbsorber absorber = new TextAbsorber
            {
                ExtractionOptions = extractOpts
            };

            // Extract text from page 31 only
            doc.Pages[31].Accept(absorber);

            // Write the extracted text to a plain‑text file
            File.WriteAllText(outputPath, absorber.Text);
        }

        Console.WriteLine($"Text from page 31 saved to '{outputPath}'.");
    }
}
