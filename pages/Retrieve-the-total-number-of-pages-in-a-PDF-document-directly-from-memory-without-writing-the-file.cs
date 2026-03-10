using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // ------------------------------------------------------------
        // Create a PDF in memory (this is only for the demo purpose).
        // In a real scenario you would already have a byte[] containing
        // the PDF data – e.g., from a database, a web request, an
        // embedded resource, etc.
        // ------------------------------------------------------------
        byte[] pdfBytes;
        using (var tempDoc = new Document())
        {
            // Ensure the document has at least one page so the count is > 0.
            tempDoc.Pages.Add();
            using (var ms = new MemoryStream())
            {
                tempDoc.Save(ms);
                pdfBytes = ms.ToArray();
            }
        }

        // Load the PDF directly from the in‑memory byte array.
        using (var ms = new MemoryStream(pdfBytes))
        using (var doc = new Document(ms))
        {
            // Pages are 1‑based; the Count property returns the total number.
            int totalPages = doc.Pages.Count;
            Console.WriteLine($"Total pages in PDF (in memory): {totalPages}");
        }
    }
}