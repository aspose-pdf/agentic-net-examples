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
        const int targetPageNumber = 12; // 1‑based index

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(inputPdfPath))
        {
            // Verify that the requested page exists
            if (targetPageNumber < 1 || targetPageNumber > doc.Pages.Count)
            {
                Console.Error.WriteLine($"Page {targetPageNumber} is out of range. Document has {doc.Pages.Count} pages.");
                return;
            }

            // Configure TextAbsorber to extract raw text (no formatting)
            Aspose.Pdf.Text.TextAbsorber absorber = new Aspose.Pdf.Text.TextAbsorber();
            absorber.ExtractionOptions = new Aspose.Pdf.Text.TextExtractionOptions(
                Aspose.Pdf.Text.TextExtractionOptions.TextFormattingMode.Raw);

            // Extract text from the specific page
            Aspose.Pdf.Page page = doc.Pages[targetPageNumber];
            page.Accept(absorber);
            string rawText = absorber.Text ?? string.Empty;

            // Write the extracted raw data to a text file
            File.WriteAllText(outputTxtPath, rawText);
            Console.WriteLine($"Raw text from page {targetPageNumber} saved to '{outputTxtPath}'.");
        }
    }
}