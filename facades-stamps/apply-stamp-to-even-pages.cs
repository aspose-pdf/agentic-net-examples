using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";      // source PDF
        const string stampPdf   = "stamp.pdf";      // PDF page to be used as stamp
        const string outputPdf  = "output_even_pages.pdf";

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

        // Load the source document (disposal handled by using)
        using (Document doc = new Document(inputPdf))
        {
            // Prepare the stamp – use the first page of stampPdf as the stamp content
            Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
            stamp.BindPdf(stampPdf, 1);          // bind page 1 of stampPdf
            stamp.IsBackground = true;           // place stamp behind page content (optional)

            // Determine even page numbers (Aspose.Pdf uses 1‑based indexing)
            int pageCount = doc.Pages.Count;
            List<int> evenPages = new List<int>();
            for (int i = 1; i <= pageCount; i++)
            {
                if (i % 2 == 0)                  // even page
                    evenPages.Add(i);
            }
            stamp.Pages = evenPages.ToArray();   // apply stamp only to even pages

            // Apply the stamp using PdfFileStamp (no using – it does not implement IDisposable)
            PdfFileStamp fileStamp = new PdfFileStamp();
            fileStamp.BindPdf(inputPdf);          // load the source PDF into the facade
            fileStamp.AddStamp(stamp);            // add the configured stamp
            fileStamp.Save(outputPdf);            // save the result
            fileStamp.Close();                    // close the facade
        }

        Console.WriteLine($"Stamp applied to even pages. Output saved to '{outputPdf}'.");
    }
}