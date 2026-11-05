using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input PDF files
        const string firstPdfPath  = "first.pdf";
        const string secondPdfPath = "second.pdf";

        // Define the page range to compare (1‑based inclusive)
        const int startPage = 2;
        const int endPage   = 4;

        // Verify that both files exist
        if (!File.Exists(firstPdfPath) || !File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Load both documents inside using blocks for deterministic disposal
        using (Document doc1 = new Document(firstPdfPath))
        using (Document doc2 = new Document(secondPdfPath))
        {
            // Ensure the requested page range is valid for both documents
            int maxPage = Math.Min(doc1.Pages.Count, doc2.Pages.Count);
            if (startPage < 1 || endPage > maxPage || startPage > endPage)
            {
                Console.Error.WriteLine("Invalid page range specified.");
                return;
            }

            // Extract text from the selected page range of each document
            string textFromDoc1 = ExtractTextFromPageRange(doc1, startPage, endPage);
            string textFromDoc2 = ExtractTextFromPageRange(doc2, startPage, endPage);

            // Simple textual comparison of the extracted content
            if (string.Equals(textFromDoc1, textFromDoc2, StringComparison.Ordinal))
            {
                Console.WriteLine($"Pages {startPage}-{endPage} are identical in both PDFs.");
            }
            else
            {
                Console.WriteLine($"Pages {startPage}-{endPage} differ between the PDFs.");
                Console.WriteLine($"Length of extracted text – Doc1: {textFromDoc1.Length}, Doc2: {textFromDoc2.Length}");
            }
        }
    }

    // Helper method: extracts concatenated text from a range of pages (inclusive)
    private static string ExtractTextFromPageRange(Document doc, int start, int end)
    {
        // TextAbsorber gathers text from pages
        TextAbsorber absorber = new TextAbsorber
        {
            // Use pure text formatting to avoid layout artifacts
            ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure)
        };

        // Iterate over the required pages (Aspose.Pdf uses 1‑based indexing)
        for (int pageNum = start; pageNum <= end; pageNum++)
        {
            doc.Pages[pageNum].Accept(absorber);
        }

        // Return the combined text from the specified range
        return absorber.Text;
    }
}