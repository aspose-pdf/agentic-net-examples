using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputTxtPath = "output.txt";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Create a TextAbsorber to extract text
            TextAbsorber absorber = new TextAbsorber();

            // Configure extraction options with a low scale factor (0.1) for fine column detection
            // The constructor now requires a TextFormattingMode argument (e.g., Pure)
            var extractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure)
            {
                ScaleFactor = 0.1
            };
            absorber.ExtractionOptions = extractionOptions;

            // Apply the absorber to page 28 (Aspose.Pdf uses 1‑based page indexing)
            doc.Pages[28].Accept(absorber);

            // Retrieve the extracted text
            string extractedText = absorber.Text ?? string.Empty;

            // Write the text to the output file
            File.WriteAllText(outputTxtPath, extractedText);
        }

        Console.WriteLine($"Text from page 28 saved to '{outputTxtPath}'.");
    }
}
