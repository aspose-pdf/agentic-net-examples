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
        const int targetPageNumber = 33; // 1‑based page index

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Prepare a TextAbsorber
            TextAbsorber absorber = new TextAbsorber();

            // Configure extraction options – the constructor now requires a TextFormattingMode
            absorber.ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure)
            {
                ScaleFactor = 2.0
            };

            // Apply the absorber to the specific page (page 33)
            if (targetPageNumber >= 1 && targetPageNumber <= pdfDoc.Pages.Count)
            {
                pdfDoc.Pages[targetPageNumber].Accept(absorber);
            }
            else
            {
                Console.Error.WriteLine($"Page {targetPageNumber} is out of range. Document has {pdfDoc.Pages.Count} pages.");
                return;
            }

            // Write the extracted text to the output file
            File.WriteAllText(outputTxtPath, absorber.Text ?? string.Empty);
            Console.WriteLine($"Extracted text from page {targetPageNumber} saved to '{outputTxtPath}'.");
        }
    }
}
