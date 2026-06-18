using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the PDF file on disk
        const string pdfPath = "sample.pdf";

        // Verify that the file exists before attempting to open it
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Use a using block to ensure the Document is disposed properly
        using (Document doc = new Document(pdfPath))
        {
            // Retrieve the number of pages (Pages collection is 1‑based)
            int pageCount = doc.Pages.Count;

            // Output the page count to the console
            Console.WriteLine($"The document contains {pageCount} page{(pageCount == 1 ? "" : "s")}.");
        }
    }
}