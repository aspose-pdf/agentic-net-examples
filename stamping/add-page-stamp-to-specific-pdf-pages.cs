using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";      // source document
        const string stampPdf  = "stamp.pdf";      // PDF containing the page to be used as a stamp
        const string outputPdf = "output.pdf";     // result document

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        if (!File.Exists(stampPdf))
        {
            Console.Error.WriteLine($"Stamp file not found: {stampPdf}");
            return;
        }

        // Load the source PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Create a PdfPageStamp from the first page of the stamp PDF
            // (page index is 1‑based)
            PdfPageStamp pageStamp = new PdfPageStamp(stampPdf, 1);

            // Optional: configure stamp appearance (e.g., make it semi‑transparent)
            pageStamp.Opacity = 0.5f;
            pageStamp.Background = false; // draw on top of page content

            // Apply the stamp to pages 5 through 10 (inclusive)
            // Ensure we do not exceed the actual page count
            int start = 5;
            int end   = Math.Min(10, doc.Pages.Count);

            for (int i = start; i <= end; i++)
            {
                // Each page in the collection is 1‑based
                Page targetPage = doc.Pages[i];
                targetPage.AddStamp(pageStamp);
            }

            // Save the modified document
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPdf}'.");
    }
}