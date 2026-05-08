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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Set extraction options to preserve line breaks and spacing
            TextExtractionOptions extractionOptions = new TextExtractionOptions(
                TextExtractionOptions.TextFormattingMode.Pure); // Pure retains formatting

            // Create a TextAbsorber with the configured options
            TextAbsorber absorber = new TextAbsorber(extractionOptions);

            // Extract text from all pages
            doc.Pages.Accept(absorber);

            // Retrieve and display the extracted text
            string extractedText = absorber.Text;
            Console.WriteLine(extractedText);
        }
    }
}