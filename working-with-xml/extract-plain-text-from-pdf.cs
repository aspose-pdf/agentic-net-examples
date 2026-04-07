using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";          // PDF generated from XML
        const string outputTextPath = "extracted.txt";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Set up extraction options – Pure mode gives plain text with minimal formatting
            TextExtractionOptions extractionOpts = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure);

            // Create a TextAbsorber with the configured options
            TextAbsorber absorber = new TextAbsorber(extractionOpts);

            // Extract text from all pages
            pdfDoc.Pages.Accept(absorber);

            // Retrieve the extracted text
            string extractedText = absorber.Text;

            // Optionally write the text to a file for indexing
            File.WriteAllText(outputTextPath, extractedText);

            Console.WriteLine($"Extracted text saved to '{outputTextPath}'.");
        }
    }
}
