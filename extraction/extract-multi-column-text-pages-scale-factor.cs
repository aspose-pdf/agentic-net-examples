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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Create extraction options with an explicit formatting mode.
            var extractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure)
            {
                ScaleFactor = 0.5
                // Column detection is enabled by default in recent Aspose.Pdf versions.
            };

            // Configure the TextAbsorber.
            var absorber = new TextAbsorber
            {
                ExtractionOptions = extractionOptions
            };

            // Apply the absorber only to pages 3 and 4 (1‑based indexing).
            doc.Pages[3].Accept(absorber);
            doc.Pages[4].Accept(absorber);

            // Retrieve the extracted text.
            string extractedText = absorber.Text ?? string.Empty;

            // Write the text to the output file.
            File.WriteAllText(outputPath, extractedText);
        }

        Console.WriteLine($"Text extracted to '{outputPath}'.");
    }
}
