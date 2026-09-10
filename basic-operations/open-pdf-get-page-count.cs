using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        const string pdfPath = "sample.pdf";

        // Verify the file exists before attempting to open it
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Use a using block for deterministic disposal of the Document (lifecycle rule)
        using (Document doc = new Document(pdfPath))
        {
            // Pages collection is 1‑based (page-indexing-one-based rule)
            int pageCount = doc.Pages.Count;

            Console.WriteLine($"The PDF '{pdfPath}' contains {pageCount} page{(pageCount == 1 ? "" : "s")}.");
        }
    }
}