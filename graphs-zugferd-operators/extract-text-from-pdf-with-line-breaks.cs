using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "extracted.txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPath))
            {
                // Set extraction options to preserve line breaks and spacing
                // The formatting mode is supplied via the constructor
                TextExtractionOptions extractionOptions = new TextExtractionOptions(
                    TextExtractionOptions.TextFormattingMode.Pure);

                // Create a TextAbsorber with the configured options
                TextAbsorber absorber = new TextAbsorber
                {
                    ExtractionOptions = extractionOptions
                };

                // Extract text from all pages
                pdfDoc.Pages.Accept(absorber);

                // Get the extracted text
                string extractedText = absorber.Text;

                // Write the text to an output file
                File.WriteAllText(outputPath, extractedText);
                Console.WriteLine($"Text extracted to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
