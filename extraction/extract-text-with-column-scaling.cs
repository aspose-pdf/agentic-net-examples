using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputTxt = "output.txt";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Create a TextAbsorber to extract text
            TextAbsorber absorber = new TextAbsorber();

            // Set maximum column scaling factor (10) for better column detection
            // The constructor now requires a TextFormattingMode argument (e.g., Pure)
            var extractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure)
            {
                ScaleFactor = 10
            };
            absorber.ExtractionOptions = extractionOptions;

            // Apply the absorber to all pages of the document
            doc.Pages.Accept(absorber);

            // Write the extracted text to a plain text file
            File.WriteAllText(outputTxt, absorber.Text);
        }

        Console.WriteLine($"Text extracted to '{outputTxt}'.");
    }
}
