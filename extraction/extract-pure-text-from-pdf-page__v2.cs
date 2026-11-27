using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            if (doc.Pages.Count < 32)
            {
                Console.Error.WriteLine("The document does not contain page 32.");
                return;
            }

            // Configure extraction to use Pure formatting mode (clean, unstyled text).
            var absorber = new TextAbsorber
            {
                ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure)
            };

            // Extract text from only page 32.
            doc.Pages[32].Accept(absorber);

            // Save the extracted text to a UTF‑8 encoded file.
            File.WriteAllText(outputPath, absorber.Text, Encoding.UTF8);
        }

        Console.WriteLine($"Extracted text from page 32 saved to '{outputPath}'.");
    }
}
