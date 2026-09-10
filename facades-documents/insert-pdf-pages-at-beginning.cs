using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string destinationPdf = "destination.pdf";   // PDF that will receive pages
        const string sourcePdf      = "source.pdf";        // PDF whose pages will be inserted
        const string outputPdf      = "merged_beginning.pdf";

        // Validate input files
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

        // Determine the last page number of the source PDF (pages to insert)
        int sourceLastPage;
        using (Document srcDoc = new Document(sourcePdf))
        {
            sourceLastPage = srcDoc.Pages.Count; // Page indexing is 1‑based
        }

        // Insert all pages of sourcePdf at the beginning (position 1) of destinationPdf
        PdfFileEditor editor = new PdfFileEditor();
        bool success = editor.Insert(
            inputFile: destinationPdf,   // target PDF
            insertLocation: 1,          // insert at the very start
            portFile: sourcePdf,        // PDF providing pages to insert
            startPage: 1,               // first page of source
            endPage: sourceLastPage,    // last page of source
            outputFile: outputPdf);     // result PDF

        if (success)
            Console.WriteLine($"Pages from '{sourcePdf}' inserted at the beginning of '{destinationPdf}'. Result saved as '{outputPdf}'.");
        else
            Console.Error.WriteLine("Insert operation failed.");
    }
}