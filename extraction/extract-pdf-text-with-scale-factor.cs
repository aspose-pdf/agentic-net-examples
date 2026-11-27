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

        // Load the PDF document inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPath))
        {
            // Create a TextAbsorber and configure extraction options.
            TextAbsorber absorber = new TextAbsorber();

            // Set ScaleFactor to 2.0 to improve extraction from wide columns.
            // The constructor now requires a TextFormattingMode argument (e.g., Pure).
            var extractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure)
            {
                ScaleFactor = 2.0
            };
            absorber.ExtractionOptions = extractionOptions;

            // Extract text from all pages.
            doc.Pages.Accept(absorber);

            // Write the extracted text to a plain‑text file.
            File.WriteAllText(outputPath, absorber.Text);
        }

        Console.WriteLine($"Text extracted to '{outputPath}'.");
    }
}
