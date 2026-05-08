using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string outputTextPath = "extracted.txt";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF document with deterministic disposal
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Set extraction options for plain (pure) text formatting
            var extractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure);

            // Create a TextAbsorber using the options
            TextAbsorber absorber = new TextAbsorber(extractionOptions);

            // Apply the absorber to all pages (Aspose.Pdf uses 1‑based page indexing)
            pdfDoc.Pages.Accept(absorber);

            // Retrieve the extracted text
            string extractedText = absorber.Text;

            // Save the extracted text to a file for indexing
            File.WriteAllText(outputTextPath, extractedText);
            Console.WriteLine($"Text extracted to '{outputTextPath}'.");
        }
    }
}
