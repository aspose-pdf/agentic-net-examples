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

        // Load the PDF inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Ensure the document has at least 20 pages (1‑based indexing)
            if (pdfDoc.Pages.Count < 20)
            {
                Console.Error.WriteLine("The document does not contain page 20.");
                return;
            }

            // Create extraction options – the constructor now requires a TextFormattingMode.
            // "Pure" extracts plain text without layout; you can switch to PreserveLayout if needed.
            var extractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure)
            {
                // Reduce the scale factor to 0.1 as requested
                ScaleFactor = 0.1
            };

            // If the installed Aspose.PDF version supports fine‑column extraction, the following line can be uncommented:
            // extractionOptions.ColumnExtractionMode = TextExtractionOptions.ColumnExtractionMode.Fine;

            // Create a TextAbsorber and assign the configured options
            var absorber = new TextAbsorber { ExtractionOptions = extractionOptions };

            // Extract text from page 20 only
            pdfDoc.Pages[20].Accept(absorber);

            // Write the extracted text to the output file
            File.WriteAllText(outputTxtPath, absorber.Text);
        }

        Console.WriteLine($"Text from page 20 extracted to '{outputTxtPath}'.");
    }
}
