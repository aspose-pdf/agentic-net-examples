using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the destination PDF, source PDF, and the resulting PDF
        const string destinationPdf = "destination.pdf";
        const string sourcePdf = "source.pdf";
        const string outputPdf = "merged.pdf";

        // Verify that both input files exist
        if (!File.Exists(destinationPdf) || !File.Exists(sourcePdf))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Determine the number of pages in the source PDF
        int sourcePageCount;
        using (Document srcDoc = new Document(sourcePdf))
        {
            sourcePageCount = srcDoc.Pages.Count; // Page indexing is 1‑based
        }

        // Insert all pages from the source PDF at the beginning of the destination PDF
        // Insert location = 1 (the very first position in the destination file)
        PdfFileEditor editor = new PdfFileEditor();
        bool result = editor.Insert(
            destinationPdf,   // inputFile: the PDF into which pages will be inserted
            1,                // insertLocation: position in the input file (1 = start)
            sourcePdf,        // portFile: the PDF providing pages to insert
            1,                // startPage: first page of the source PDF
            sourcePageCount,  // endPage: last page of the source PDF
            outputPdf);       // outputFile: resulting PDF

        if (result)
            Console.WriteLine($"Pages successfully inserted. Output saved to '{outputPdf}'.");
        else
            Console.Error.WriteLine("Failed to insert pages into the PDF.");
    }
}