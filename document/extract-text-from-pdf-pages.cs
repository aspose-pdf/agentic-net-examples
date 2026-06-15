using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input PDF file, output text file and page range
        const string inputPdfPath  = "input.pdf";
        const string outputTxtPath = "extracted.txt";
        const int startPage = 2;   // first page to extract (1‑based)
        const int endPage   = 5;   // last page to extract (inclusive)

        // Verify the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document (core API, no Facades)
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Validate page range
                int totalPages = pdfDoc.Pages.Count;
                if (startPage < 1 || endPage > totalPages || startPage > endPage)
                {
                    Console.Error.WriteLine("Invalid page range specified.");
                    return;
                }

                // TextAbsorber collects extracted text
                TextAbsorber absorber = new TextAbsorber();

                // Extract text from each page in the specified range
                for (int pageNum = startPage; pageNum <= endPage; pageNum++)
                {
                    // Accept the absorber for the current page
                    absorber.Visit(pdfDoc.Pages[pageNum]);
                }

                // Retrieve the accumulated text
                string extractedText = absorber.Text ?? string.Empty;

                // Save the text to a plain .txt file
                File.WriteAllText(outputTxtPath, extractedText);
                Console.WriteLine($"Text extracted from pages {startPage}-{endPage} to '{outputTxtPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}