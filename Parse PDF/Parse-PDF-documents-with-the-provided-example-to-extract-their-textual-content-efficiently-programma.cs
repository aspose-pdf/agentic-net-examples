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
        using (Document pdfDoc = new Document(inputPath))
        {
            // Create a TextAbsorber with Pure formatting mode for readable output.
            TextAbsorber absorber = new TextAbsorber(
                new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure));

            // Accept the absorber for all pages – performs a single pass extraction.
            pdfDoc.Pages.Accept(absorber);

            // Retrieve the extracted text.
            string extractedText = absorber.Text;

            Console.WriteLine("Extracted text:");
            Console.WriteLine(extractedText);
        }
    }
}