using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Path to the PDF file to be read.
        const string pdfPath = "input.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Open the PDF document inside a using block for deterministic disposal.
        using (Document pdfDocument = new Document(pdfPath))
        {
            // Configure TextAbsorber for efficient extraction.
            // MemorySaving mode reduces memory usage for large documents.
            TextAbsorber absorber = new TextAbsorber
            {
                ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.MemorySaving)
            };

            // Apply the absorber to all pages of the document.
            pdfDocument.Pages.Accept(absorber);

            // The extracted text is now available in absorber.Text.
            string extractedText = absorber.Text ?? string.Empty;

            // Output basic information about the extraction.
            Console.WriteLine($"Pages processed: {pdfDocument.Pages.Count}");
            Console.WriteLine($"Extracted characters: {extractedText.Length}");
            Console.WriteLine("First 500 characters of extracted text:");
            Console.WriteLine(extractedText.Substring(0, Math.Min(500, extractedText.Length)));
        }
    }
}