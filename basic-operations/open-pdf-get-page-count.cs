using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Open the PDF file using the Document constructor.
        // Wrapped in a using block for deterministic disposal.
        using (Document doc = new Document(pdfPath))
        {
            // Verify the number of pages in the document.
            int pageCount = doc.Pages.Count;
            Console.WriteLine($"Page count: {pageCount}");

            // Simple check: ensure the document contains at least one page.
            if (pageCount == 0)
            {
                Console.WriteLine("The document has no pages.");
            }
            else
            {
                Console.WriteLine("Document loaded successfully.");
            }
        }
    }
}