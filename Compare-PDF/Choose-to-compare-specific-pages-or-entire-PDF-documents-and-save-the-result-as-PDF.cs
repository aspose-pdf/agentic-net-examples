using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main(string[] args)
    {
        // Expected arguments:
        // args[0] - first PDF file path
        // args[1] - second PDF file path
        // args[2] - output PDF file path
        // args[3] - (optional) page number to compare specific pages
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: <program> <pdf1> <pdf2> <output> [pageNumber]");
            return;
        }

        string pdfPath1 = args[0];
        string pdfPath2 = args[1];
        string outputPath = args[2];
        int pageNumber = 0; // 0 means compare whole documents

        if (args.Length >= 4 && int.TryParse(args[3], out int parsedPage) && parsedPage > 0)
        {
            pageNumber = parsedPage;
        }

        // Validate input files
        if (!File.Exists(pdfPath1))
        {
            Console.Error.WriteLine($"Error: File not found - {pdfPath1}");
            return;
        }

        if (!File.Exists(pdfPath2))
        {
            Console.Error.WriteLine($"Error: File not found - {pdfPath2}");
            return;
        }

        try
        {
            // Load the two PDF documents
            Document doc1 = new Document(pdfPath1);
            Document doc2 = new Document(pdfPath2);

            // Prepare default side‑by‑side comparison options
            SideBySideComparisonOptions options = new SideBySideComparisonOptions();

            if (pageNumber > 0)
            {
                // Ensure the requested page exists in both documents
                if (pageNumber > doc1.Pages.Count || pageNumber > doc2.Pages.Count)
                {
                    Console.Error.WriteLine("Error: Specified page number exceeds page count of one of the documents.");
                    return;
                }

                // Retrieve the specific pages (1‑based indexing)
                Page page1 = doc1.Pages[pageNumber];
                Page page2 = doc2.Pages[pageNumber];

                // Compare the two pages and save the result
                SideBySidePdfComparer.Compare(page1, page2, outputPath, options);
                Console.WriteLine($"Page {pageNumber} comparison saved to '{outputPath}'.");
            }
            else
            {
                // Compare the whole documents and save the result
                SideBySidePdfComparer.Compare(doc1, doc2, outputPath, options);
                Console.WriteLine($"Document comparison saved to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred during comparison: {ex.Message}");
        }
    }
}