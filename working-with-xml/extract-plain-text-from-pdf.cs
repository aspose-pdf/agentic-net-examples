using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";          // PDF generated from XML
        const string outputPath = "extracted.txt";   // Plain‑text output for indexing

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(pdfPath))
        {
            // Set extraction options to plain text (no layout formatting)
            TextExtractionOptions extractionOptions = new TextExtractionOptions(
                TextExtractionOptions.TextFormattingMode.Pure);

            // Create the absorber with the options
            TextAbsorber absorber = new TextAbsorber(extractionOptions);

            // Extract text from all pages
            doc.Pages.Accept(absorber);

            // Retrieve the extracted text
            string extractedText = absorber.Text;

            // Write the plain text to a file (or any other storage)
            File.WriteAllText(outputPath, extractedText);
            Console.WriteLine($"Plain text extracted to '{outputPath}'.");
        }
    }
}