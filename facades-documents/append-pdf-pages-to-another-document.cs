using System;
using System.IO;
using Aspose.Pdf;               // Document class for page count
using Aspose.Pdf.Facades;      // PdfFileEditor for appending pages

class Program
{
    static void Main()
    {
        // Paths for the destination PDF, source PDF and the resulting merged PDF
        const string destinationPdf = "destination.pdf";
        const string sourcePdf      = "source.pdf";
        const string outputPdf      = "merged.pdf";

        // Verify that both input files exist
        if (!File.Exists(destinationPdf))
        {
            Console.Error.WriteLine($"Destination file not found: {destinationPdf}");
            return;
        }
        if (!File.Exists(sourcePdf))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePdf}");
            return;
        }

        // Determine the total number of pages in the source PDF.
        // Document implements IDisposable, so wrap it in a using block.
        int sourcePageCount;
        using (Document srcDoc = new Document(sourcePdf))
        {
            sourcePageCount = srcDoc.Pages.Count; // Pages are 1‑based
        }

        // Append all pages from the source PDF to the end of the destination PDF.
        // PdfFileEditor does NOT implement IDisposable; do NOT wrap it in a using block.
        PdfFileEditor editor = new PdfFileEditor();

        // Append(startFile, portFile, startPage, endPage, outputFile)
        // startPage = 1, endPage = sourcePageCount copies the entire source document.
        bool success = editor.Append(destinationPdf, sourcePdf, 1, sourcePageCount, outputPdf);

        if (success)
        {
            Console.WriteLine($"Pages from '{sourcePdf}' successfully appended to '{destinationPdf}'.");
            Console.WriteLine($"Merged file created at '{outputPdf}'.");
        }
        else
        {
            Console.Error.WriteLine("Failed to append pages. Check file paths and permissions.");
        }
    }
}