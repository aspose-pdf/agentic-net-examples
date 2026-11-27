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
        const int targetPageNumber = 23; // 1‑based index

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Verify that the requested page exists
                if (pdfDoc.Pages.Count < targetPageNumber)
                {
                    Console.Error.WriteLine($"Document has only {pdfDoc.Pages.Count} pages; page {targetPageNumber} is out of range.");
                    return;
                }

                // Set up a TextAbsorber to extract raw text (no formatting)
                Aspose.Pdf.Text.TextAbsorber absorber = new Aspose.Pdf.Text.TextAbsorber();
                absorber.ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Raw);

                // Extract text from the specific page
                pdfDoc.Pages[targetPageNumber].Accept(absorber);
                string rawPageText = absorber.Text ?? string.Empty;

                // Write the extracted raw data to a text file
                File.WriteAllText(outputTxtPath, rawPageText);
                Console.WriteLine($"Raw text from page {targetPageNumber} saved to '{outputTxtPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}