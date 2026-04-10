using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "extracted.txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document with deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Set extraction options to preserve line breaks and spacing
            TextExtractionOptions options = new TextExtractionOptions(
                TextExtractionOptions.TextFormattingMode.Pure);
            // Let the engine decide the best scaling factor (0 = auto)
            options.ScaleFactor = 0;

            // Create a TextAbsorber using the configured options
            TextAbsorber absorber = new TextAbsorber(options);

            // Extract text from all pages
            doc.Pages.Accept(absorber);

            // Retrieve the extracted text
            string extractedText = absorber.Text;

            // Save the extracted text to a file
            File.WriteAllText(outputPath, extractedText);
            Console.WriteLine($"Text extracted to '{outputPath}'.");
        }
    }
}