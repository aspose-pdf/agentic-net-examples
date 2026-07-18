using System;
using System.IO;
using Aspose.Pdf;                 // Core PDF API (for reading page count)
using Aspose.Pdf.Facades;        // Facade API (PdfFileEditor)

class Program
{
    static void Main()
    {
        // Paths to the destination PDF, source PDF, and the resulting merged PDF
        const string destinationPdf = "destination.pdf";
        const string sourcePdf      = "source.pdf";
        const string outputPdf      = "merged.pdf";

        // Ensure the source file exists before proceeding
        if (!File.Exists(sourcePdf))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePdf}");
            return;
        }

        // Determine the page range to append from the source PDF.
        // Here we append all pages, so start at 1 and end at the total page count.
        int startPage, endPage;
        using (Document srcDoc = new Document(sourcePdf))
        {
            startPage = 1;                     // First page (1‑based indexing)
            endPage   = srcDoc.Pages.Count;    // Last page
        }

        // Use PdfFileEditor (does NOT implement IDisposable) to append pages.
        PdfFileEditor editor = new PdfFileEditor();

        // Append the specified page range from sourcePdf to the end of destinationPdf.
        // The result is written to outputPdf.
        bool success = editor.Append(
            inputFile:  destinationPdf,
            portFile:   sourcePdf,
            startPage:  startPage,
            endPage:    endPage,
            outputFile: outputPdf);

        // Report the outcome.
        if (success)
            Console.WriteLine($"Pages {startPage}-{endPage} from '{sourcePdf}' appended to '{destinationPdf}'. Result saved as '{outputPdf}'.");
        else
            Console.Error.WriteLine("Append operation failed.");
    }
}