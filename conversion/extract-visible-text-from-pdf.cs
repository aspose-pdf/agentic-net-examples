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
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Configure extraction options with explicit formatting mode (required by newer API)
            var extractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure);

            // Create absorber and assign the configured options
            TextAbsorber absorber = new TextAbsorber
            {
                ExtractionOptions = extractionOptions
            };

            // Apply the absorber to all pages
            pdfDoc.Pages.Accept(absorber);

            // Write the extracted (visible) text to a plain‑text file
            File.WriteAllText(outputTxt, absorber.Text);
        }

        Console.WriteLine($"Visible text extracted to '{outputTxt}'.");
    }
}
