using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string sourcePdfPath = "source.pdf";   // PDF to take pages from
        const string outputPdfPath = "appended.pdf"; // Resulting PDF
        const int startPage = 2; // first page to copy (1‑based)
        const int endPage   = 4; // last page to copy (inclusive)

        // Verify source file exists
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePdfPath}");
            return;
        }

        // -----------------------------------------------------------------
        // 1. Create an empty PDF document that will receive the pages.
        //    The document must contain at least one page; otherwise the
        //    Append operation would have no target pages.
        // -----------------------------------------------------------------
        string tempEmptyPdf = Path.GetTempFileName(); // temporary placeholder
        try
        {
            using (Document emptyDoc = new Document())
            {
                // Add a single blank page
                emptyDoc.Pages.Add();
                emptyDoc.Save(tempEmptyPdf);
            }

            // -----------------------------------------------------------------
            // 2. Use PdfFileEditor.Append to copy the desired page range from
            //    the source PDF and append it to the empty PDF.
            // -----------------------------------------------------------------
            PdfFileEditor editor = new PdfFileEditor();
            bool appended = editor.Append(
                inputFile:  tempEmptyPdf,   // first (target) PDF
                portFile:   sourcePdfPath, // PDF providing pages to append
                startPage:  startPage,     // start of range in source PDF
                endPage:    endPage,       // end of range in source PDF
                outputFile: outputPdfPath   // resulting PDF
            );

            if (!appended)
            {
                Console.Error.WriteLine("Append operation failed.");
                return;
            }

            Console.WriteLine($"Pages {startPage}-{endPage} from '{sourcePdfPath}' appended to new PDF '{outputPdfPath}'.");
        }
        finally
        {
            // Clean up the temporary empty PDF file
            if (File.Exists(tempEmptyPdf))
            {
                try { File.Delete(tempEmptyPdf); } catch { /* ignore */ }
            }
        }
    }
}