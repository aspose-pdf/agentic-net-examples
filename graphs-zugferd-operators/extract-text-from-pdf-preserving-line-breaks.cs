using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputTxt = "extracted.txt";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Create extraction options that preserve line breaks and spacing
            TextExtractionOptions options = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure);
            // Optional: adjust spacing if needed (1 = default, 0 = auto)
            options.ScaleFactor = 1;

            // Initialize the TextAbsorber with the options
            TextAbsorber absorber = new TextAbsorber(options);

            // Extract text from all pages
            doc.Pages.Accept(absorber);

            // Get the extracted text
            string extractedText = absorber.Text;

            // Write the text to a file (or handle as needed)
            File.WriteAllText(outputTxt, extractedText);
            Console.WriteLine($"Text extracted to '{outputTxt}'.");
        }
    }
}