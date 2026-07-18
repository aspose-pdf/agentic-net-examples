using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class PdfTextRangeExtractor
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputTxtPath = "extracted.txt";
        const int startPage = 2;      // first page to extract (1‑based)
        const int pageCount = 3;      // number of pages to extract

        try
        {
            ExtractTextFromRange(inputPdfPath, startPage, pageCount, outputTxtPath);
            Console.WriteLine($"Text extracted to '{outputTxtPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    static void ExtractTextFromRange(string pdfPath, int startPage, int pageCount, string outputPath)
    {
        if (!File.Exists(pdfPath))
            throw new FileNotFoundException($"PDF file not found: {pdfPath}");

        // Ensure valid page range
        if (startPage < 1)
            throw new ArgumentOutOfRangeException(nameof(startPage), "Start page must be >= 1.");
        if (pageCount < 1)
            throw new ArgumentOutOfRangeException(nameof(pageCount), "Page count must be >= 1.");

        // Load the PDF document (lifecycle rule: wrap in using)
        using (Document doc = new Document(pdfPath))
        {
            // Adjust pageCount if it exceeds the document length
            int maxPage = Math.Min(startPage + pageCount - 1, doc.Pages.Count);
            StringBuilder sb = new StringBuilder();

            // Iterate over the requested page range (Aspose.Pdf uses 1‑based indexing)
            for (int i = startPage; i <= maxPage; i++)
            {
                // Create a fresh TextAbsorber for each page (rule: use TextAbsorber for extraction)
                TextAbsorber absorber = new TextAbsorber();

                // Accept the absorber on the current page
                doc.Pages[i].Accept(absorber);

                // Append extracted text (preserve line breaks)
                sb.AppendLine(absorber.Text);
            }

            // Write the accumulated text to a plain text file
            File.WriteAllText(outputPath, sb.ToString(), Encoding.UTF8);
        }
    }
}