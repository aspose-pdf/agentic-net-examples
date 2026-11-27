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
        const int targetPageNumber = 25; // 1‑based page index

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Ensure the requested page exists
            if (targetPageNumber < 1 || targetPageNumber > pdfDoc.Pages.Count)
            {
                Console.Error.WriteLine($"Page {targetPageNumber} is out of range. Document has {pdfDoc.Pages.Count} pages.");
                return;
            }

            // Configure TextAbsorber with a ScaleFactor of 2.0 to improve column detection.
            // The TextExtractionOptions constructor requires a TextFormattingMode argument.
            var extractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure)
            {
                ScaleFactor = 2.0f // widen column detection
            };
            TextAbsorber absorber = new TextAbsorber(extractionOptions);

            // Extract text from the specific page
            Page page = pdfDoc.Pages[targetPageNumber];
            page.Accept(absorber);
            string extractedText = absorber.Text ?? string.Empty;

            // Write the extracted text to a plain‑text file
            File.WriteAllText(outputTxtPath, extractedText);
            Console.WriteLine($"Extracted text from page {targetPageNumber} saved to '{outputTxtPath}'.");
        }
    }
}
