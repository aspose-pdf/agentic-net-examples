using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input PDF and output text file paths
        const string inputPdfPath = "input.pdf";
        const string outputTxtPath = "extracted.txt";

        // Define the page range (inclusive). Aspose.Pdf uses 1‑based indexing.
        const int startPage = 2;
        const int endPage   = 5;

        // Verify the input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Validate the requested page range against the document's page count
            if (startPage < 1 || endPage > pdfDoc.Pages.Count || startPage > endPage)
            {
                Console.Error.WriteLine("Invalid page range specified.");
                return;
            }

            // Create a TextAbsorber to extract text
            TextAbsorber absorber = new TextAbsorber();
            absorber.ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure);

            // Apply the absorber only to the desired page range by iterating manually
            for (int pageNum = startPage; pageNum <= endPage; pageNum++)
            {
                pdfDoc.Pages[pageNum].Accept(absorber);
            }

            // Retrieve the extracted text (concatenated from the selected pages)
            string extractedText = absorber.Text ?? string.Empty;

            // Save the text to a plain .txt file
            File.WriteAllText(outputTxtPath, extractedText);

            Console.WriteLine($"Text from pages {startPage}-{endPage} saved to '{outputTxtPath}'.");
        }
    }
}
