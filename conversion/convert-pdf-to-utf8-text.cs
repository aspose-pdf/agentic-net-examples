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

        // Load the PDF document inside a using block for proper disposal
        using (var doc = new Document(inputPath))
        {
            // Configure the TextAbsorber to keep line breaks (Pure formatting mode)
            var absorber = new TextAbsorber
            {
                ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure)
            };

            // Extract text from all pages
            doc.Pages.Accept(absorber);
            string extractedText = absorber.Text ?? string.Empty;

            // Write the extracted text to a UTF‑8 encoded file
            File.WriteAllText(outputPath, extractedText, Encoding.UTF8);
        }

        Console.WriteLine($"Text extracted to '{outputPath}'.");
    }
}
