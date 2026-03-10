using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the PDFs
        const string targetPdf = "target.pdf";   // PDF into which pages will be inserted
        const string sourcePdf = "source.pdf";   // PDF providing pages to insert
        const string outputPdf = "merged.pdf";   // Resulting PDF

        // Verify that input files exist
        if (!File.Exists(targetPdf))
        {
            Console.Error.WriteLine($"File not found: {targetPdf}");
            return;
        }
        if (!File.Exists(sourcePdf))
        {
            Console.Error.WriteLine($"File not found: {sourcePdf}");
            return;
        }

        // Determine the number of pages in the source PDF.
        // Document must be disposed via using (lifecycle rule).
        int sourcePageCount;
        using (Document srcDoc = new Document(sourcePdf))
        {
            sourcePageCount = srcDoc.Pages.Count; // 1‑based page count
        }

        // Insert all pages from sourcePdf after the first page of targetPdf.
        // Insert location is 1‑based; 2 means "after page 1".
        PdfFileEditor editor = new PdfFileEditor();

        // Insert(startLocation, startPage, endPage) preserves order and formatting.
        bool result = editor.Insert(
            inputFile: targetPdf,
            insertLocation: 2,          // position where insertion begins
            portFile: sourcePdf,
            startPage: 1,               // first page of source to insert
            endPage: sourcePageCount,   // last page of source to insert
            outputFile: outputPdf);

        if (result)
        {
            Console.WriteLine($"Pages inserted successfully. Output saved to '{outputPdf}'.");
        }
        else
        {
            Console.Error.WriteLine("Failed to insert pages.");
        }
    }
}