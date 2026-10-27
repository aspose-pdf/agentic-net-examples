using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

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

        // Load the PDF document inside a using block for deterministic disposal
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(pdfPath))
        {
            // Total pages (1‑based indexing, but Count gives the total)
            int pageCount = doc.Pages.Count;

            // Count images by iterating each page's image collection
            int imageCount = 0;
            foreach (Aspose.Pdf.Page page in doc.Pages)
            {
                foreach (Aspose.Pdf.XImage img in page.Resources.Images)
                {
                    imageCount++;
                }
            }

            // Count embedded files (attachments) – Document.Attachments does not exist
            int attachmentCount = (doc.EmbeddedFiles != null) ? doc.EmbeddedFiles.Count : 0;

            // Report the diagnostics
            Console.WriteLine($"Pages      : {pageCount}");
            Console.WriteLine($"Images     : {imageCount}");
            Console.WriteLine($"Attachments: {attachmentCount}");
        }
    }
}