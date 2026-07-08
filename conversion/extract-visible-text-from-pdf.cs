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
            // Load the PDF document
            using (Document doc = new Document(inputPath))
            {
                // Create a TextAbsorber to extract visible text only
                TextAbsorber absorber = new TextAbsorber();

                // Use the Pure formatting mode to get readable text without hidden layers
                absorber.ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure);

                // Apply the absorber to all pages
                doc.Pages.Accept(absorber);

                // Retrieve the extracted text
                string extractedText = absorber.Text;

                // Save the text to a file
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