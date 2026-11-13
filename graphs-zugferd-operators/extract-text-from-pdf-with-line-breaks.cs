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
            using (Document doc = new Document(inputPath))
            {
                // Configure TextAbsorber to preserve layout using the constructor overload
                var extractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure);
                TextAbsorber absorber = new TextAbsorber(extractionOptions);

                // Extract text from all pages
                doc.Pages.Accept(absorber);
                string extractedText = absorber.Text;

                // Write the extracted text to a file
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
