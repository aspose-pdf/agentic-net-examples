using System;
using System.IO;
using System.Text;
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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Verify the document has at least 9 pages (1‑based indexing)
            if (doc.Pages.Count < 9)
            {
                Console.Error.WriteLine("Document contains fewer than 9 pages.");
                return;
            }

            // Set up text extraction options for pure formatting
            var extractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure);

            // Extract text from page 9 using TextAbsorber (core API replacement for TextDevice)
            var absorber = new TextAbsorber
            {
                ExtractionOptions = extractionOptions
            };
            doc.Pages[9].Accept(absorber);

            // Write the extracted clean text to the output file (UTF‑8)
            File.WriteAllText(outputPath, absorber.Text, Encoding.UTF8);
        }

        Console.WriteLine($"Page 9 text extracted to '{outputPath}'.");
    }
}
