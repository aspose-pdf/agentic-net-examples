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
            // Load the PDF document inside a using block for proper disposal
            using (Document doc = new Document(inputPath))
            {
                // TextAbsorber extracts text; set options to preserve line breaks
                TextAbsorber absorber = new TextAbsorber();
                absorber.ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure);

                // Apply absorber to all pages
                doc.Pages.Accept(absorber);
                string extractedText = absorber.Text;

                // Write the extracted text (including paragraph breaks) to a .txt file
                File.WriteAllText(outputPath, extractedText);
            }

            Console.WriteLine($"Paragraph text extracted to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}