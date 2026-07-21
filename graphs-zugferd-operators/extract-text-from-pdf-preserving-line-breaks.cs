using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Path to the source PDF file
        const string inputPdf = "input.pdf";

        // Verify that the file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Create extraction options that preserve line breaks and spacing
            TextExtractionOptions options = new TextExtractionOptions(
                TextExtractionOptions.TextFormattingMode.Pure); // Pure mode keeps formatting

            // Initialize a TextAbsorber with the above options
            TextAbsorber absorber = new TextAbsorber(options);

            // Apply the absorber to all pages of the document
            doc.Pages.Accept(absorber);

            // Retrieve the extracted text
            string extractedText = absorber.Text;

            // Output the text to the console (or write to a file as needed)
            Console.WriteLine("=== Extracted Text ===");
            Console.WriteLine(extractedText);
        }
    }
}