using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.txt";
        const int targetPage = 13;

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            if (targetPage < 1 || targetPage > doc.Pages.Count)
            {
                Console.Error.WriteLine($"Page {targetPage} out of range. Document has {doc.Pages.Count} pages.");
                return;
            }

            TextAbsorber absorber = new TextAbsorber();
            absorber.ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure)
            {
                ScaleFactor = 2.0
                // Uncomment if column detection is needed:
                // ColumnLayout = true
            };

            // Extract text from the specified page
            doc.Pages[targetPage].Accept(absorber);

            string extractedText = absorber.Text ?? string.Empty;
            File.WriteAllText(outputPath, extractedText);
        }

        Console.WriteLine($"Extracted text from page {targetPage} saved to '{outputPath}'.");
    }
}