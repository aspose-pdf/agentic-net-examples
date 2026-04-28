using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "sample.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Use the Document constructor to open the PDF file.
        // Wrap in a using block for deterministic disposal (document-disposal-with-using rule).
        using (Document doc = new Document(pdfPath))
        {
            // Pages collection is 1‑based; Count gives the total number of pages.
            int pageCount = doc.Pages.Count;
            Console.WriteLine($"The PDF contains {pageCount} page{(pageCount == 1 ? "" : "s")}.");
        }
    }
}