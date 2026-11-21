using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";

        // Verify the file exists before attempting to open it
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Open the PDF using the Document constructor inside a using block for deterministic disposal
        using (Document doc = new Document(pdfPath))
        {
            // Aspose.Pdf uses 1‑based page indexing; doc.Pages.Count returns the total number of pages
            int pageCount = doc.Pages.Count;

            Console.WriteLine($"The document contains {pageCount} page{(pageCount == 1 ? "" : "s")}.");

            // Simple verification: ensure there is at least one page
            if (pageCount > 0)
                Console.WriteLine("Page count verification passed.");
            else
                Console.WriteLine("Page count verification failed: document has no pages.");
        }
    }
}