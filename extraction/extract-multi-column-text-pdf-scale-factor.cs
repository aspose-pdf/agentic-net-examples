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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Configure text extraction for multi‑column layout
            var extractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure)
            {
                ScaleFactor = 0.5f // Reduce scale to improve column detection
            };

            TextAbsorber absorber = new TextAbsorber
            {
                ExtractionOptions = extractionOptions
            };

            // Extract text from all pages
            doc.Pages.Accept(absorber);

            // Write extracted text to a file
            File.WriteAllText(outputPath, absorber.Text);
        }

        Console.WriteLine($"Text extracted to '{outputPath}'.");
    }
}
