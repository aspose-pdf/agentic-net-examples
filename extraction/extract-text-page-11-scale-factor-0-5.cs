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
            // Ensure the document has at least 11 pages (1‑based indexing)
            if (pdfDoc.Pages.Count < 11)
            {
                Console.Error.WriteLine("The document does not contain page 11.");
                return;
            }

            // Configure text extraction options – use Pure formatting mode (the
            // closest available mode that keeps column order) and set ScaleFactor to 0.5 (50 % of original resolution).
            var extractOpts = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure)
            {
                ScaleFactor = 0.5f
            };

            // Create a TextAbsorber with the configured options
            var absorber = new TextAbsorber
            {
                ExtractionOptions = extractOpts
            };

            // Extract text from page 11 only
            pdfDoc.Pages[11].Accept(absorber);

            // Write the extracted text to a plain‑text file
            File.WriteAllText(outputTxtPath, absorber.Text);
        }

        Console.WriteLine($"Text from page 11 extracted to '{outputTxtPath}'.");
    }
}
