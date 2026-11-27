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
        const int pageNumber = 22; // 1‑based index

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Configure extraction options: high scale factor and wide‑column mode.
            // The wide‑column mode is the default behaviour when the column extraction mode
            // is not explicitly set, so we only need to set the ScaleFactor.
            var extractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure)
            {
                ScaleFactor = 10
            };

            // Prepare a TextAbsorber with the custom extraction options
            TextAbsorber absorber = new TextAbsorber { ExtractionOptions = extractionOptions };

            // Apply the absorber to the specific page (page 22)
            doc.Pages[pageNumber].Accept(absorber);

            // Retrieve the extracted text
            string extractedText = absorber.Text ?? string.Empty;

            // Save the text to a plain‑text file
            File.WriteAllText(outputPath, extractedText);
        }

        Console.WriteLine($"Text from page {pageNumber} saved to '{outputPath}'.");
    }
}
