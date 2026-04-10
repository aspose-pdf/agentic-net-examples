using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string destinationPdf = "destination.pdf"; // PDF to receive pages
        const string sourcePdf      = "source.pdf";      // PDF providing pages
        const string outputPdf      = "merged.pdf";      // Resulting PDF

        // Verify that both input files exist
        if (!File.Exists(destinationPdf) || !File.Exists(sourcePdf))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Determine the total number of pages in the source PDF
        int sourcePageCount;
        using (Document srcDoc = new Document(sourcePdf))
        {
            sourcePageCount = srcDoc.Pages.Count; // Pages are 1‑based
        }

        // Insert all pages from sourcePdf at the beginning (position 1) of destinationPdf
        PdfFileEditor editor = new PdfFileEditor();
        bool inserted = editor.Insert(
            inputFile:      destinationPdf,
            insertLocation: 1,               // Insert at the start
            portFile:       sourcePdf,
            startPage:      1,
            endPage:        sourcePageCount,
            outputFile:     outputPdf);

        if (inserted)
            Console.WriteLine($"Pages inserted successfully. Output saved to '{outputPdf}'.");
        else
            Console.Error.WriteLine("Failed to insert pages.");
    }
}