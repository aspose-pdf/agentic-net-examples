using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";          // PDF generated from XML
        const string outputTxt = "extracted.txt";    // Destination for plain text

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF document (wrapped in using for deterministic disposal)
        using (Document doc = new Document(pdfPath))
        {
            // Configure text extraction options – plain text without formatting
            var extractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure);

            // Create a TextAbsorber with the configured options
            var absorber = new TextAbsorber(extractionOptions);

            // Extract text from all pages of the document
            doc.Pages.Accept(absorber);
            string extractedText = absorber.Text;

            // Write the extracted plain text to a file (optional, for indexing)
            File.WriteAllText(outputTxt, extractedText);
            Console.WriteLine($"Plain text extracted and saved to '{outputTxt}'.");
        }
    }
}
