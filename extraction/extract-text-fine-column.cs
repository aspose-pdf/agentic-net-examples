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

        // Load the PDF document; using ensures deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a TextAbsorber for text extraction
            TextAbsorber absorber = new TextAbsorber();

            // Configure extraction options: fine column extraction with ScaleFactor = 0.1
            // The TextFormattingMode must be supplied via the constructor (no parameterless ctor in recent versions)
            TextExtractionOptions extractOpts = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure)
            {
                ScaleFactor = 0.1
            };
            absorber.ExtractionOptions = extractOpts;

            // Accept all pages in the document
            doc.Pages.Accept(absorber);

            // Retrieve the extracted text
            string extractedText = absorber.Text;

            // Write the text to a .txt file
            File.WriteAllText(outputPath, extractedText);
        }

        Console.WriteLine($"Text extracted and saved to '{outputPath}'.");
    }
}
