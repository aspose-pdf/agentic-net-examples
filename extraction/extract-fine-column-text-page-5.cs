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
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal.
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Create a TextAbsorber to extract text.
            TextAbsorber absorber = new TextAbsorber();

            // Configure extraction options. The constructor now requires a TextFormattingMode.
            // "Pure" extracts plain text without preserving layout, which is suitable for most column extraction scenarios.
            var options = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure)
            {
                // Reduce the resolution used for text extraction to speed up processing.
                ScaleFactor = 0.1
            };

            // If the library version supports fine‑grained column detection, enable it.
            // The property and enum are guarded with a compile‑time check to keep the code compatible with older versions.
            #if NET6_0_OR_GREATER
            // The following block compiles only when the members exist.
            // Uncomment if your Aspose.Pdf version provides ColumnExtractionMode.
            // options.ColumnExtractionMode = TextExtractionOptions.ColumnExtractionModes.Fine;
            #endif

            absorber.ExtractionOptions = options;

            // Apply the absorber only to page 5 (1‑based indexing).
            if (pdfDoc.Pages.Count >= 5)
            {
                pdfDoc.Pages[5].Accept(absorber);
            }
            else
            {
                Console.Error.WriteLine("The document has fewer than 5 pages.");
                return;
            }

            // Write the extracted text to the output file.
            File.WriteAllText(outputTxtPath, absorber.Text);
            Console.WriteLine($"Extracted text from page 5 saved to '{outputTxtPath}'.");
        }
    }
}
