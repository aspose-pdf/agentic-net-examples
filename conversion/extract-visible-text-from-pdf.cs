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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a TextAbsorber to collect visible text
            TextAbsorber absorber = new TextAbsorber();

            // Configure extraction to use the Pure formatting mode (visible text only)
            absorber.ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure);

            // Apply the absorber to all pages of the document
            doc.Pages.Accept(absorber);

            // Retrieve the extracted text
            string extractedText = absorber.Text;

            // Write the result to a plain text file
            File.WriteAllText(outputPath, extractedText);
        }

        Console.WriteLine($"Visible text extracted to '{outputPath}'.");
    }
}