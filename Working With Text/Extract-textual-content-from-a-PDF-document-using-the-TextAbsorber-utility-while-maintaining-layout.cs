using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document; using ensures proper disposal.
        using (Document doc = new Document(inputPath))
        {
            // Configure TextAbsorber to preserve layout (Pure formatting mode).
            TextExtractionOptions extractionOptions = new TextExtractionOptions(
                TextExtractionOptions.TextFormattingMode.Pure);
            TextAbsorber absorber = new TextAbsorber(extractionOptions);

            // Apply the absorber to all pages of the document.
            doc.Pages.Accept(absorber);

            // Retrieve the extracted text, which retains the original layout.
            string extractedText = absorber.Text;

            Console.WriteLine("=== Extracted Text ===");
            Console.WriteLine(extractedText);
        }
    }
}