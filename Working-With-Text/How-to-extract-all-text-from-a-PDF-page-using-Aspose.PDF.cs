using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const int pageNumber = 1; // 1‑based page index

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a TextAbsorber – the correct API for text extraction
            TextAbsorber absorber = new TextAbsorber();

            // Optional: choose a formatting mode (Pure, Raw, Flatten, MemorySaving)
            absorber.ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure);

            // Extract text from the specified page (pages are 1‑based)
            doc.Pages[pageNumber].Accept(absorber);

            // Retrieve the extracted text
            string extractedText = absorber.Text;

            Console.WriteLine($"Extracted text from page {pageNumber}:\n{extractedText}");
        }
    }
}