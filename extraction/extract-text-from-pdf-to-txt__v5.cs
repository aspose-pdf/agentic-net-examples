using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "Output.txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document with deterministic disposal
        using (Document pdfDoc = new Document(inputPath))
        {
            // Create a TextAbsorber to extract text from the document
            TextAbsorber absorber = new TextAbsorber();

            // Optional: configure extraction to get pure text without formatting
            absorber.ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure);

            // Apply the absorber to all pages
            pdfDoc.Pages.Accept(absorber);

            // Retrieve the extracted text
            string extractedText = absorber.Text;

            // Write the extracted text to Output.txt
            File.WriteAllText(outputPath, extractedText);
        }

        Console.WriteLine($"Text extracted to '{outputPath}'.");
    }
}