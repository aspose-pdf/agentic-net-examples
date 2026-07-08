using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string destinationPdf = "destination.pdf"; // PDF that will receive pages
        const string sourcePdf      = "source.pdf";      // PDF whose pages will be inserted
        const string outputPdf      = "merged.pdf";      // Resulting PDF

        // Verify that both input files exist
        if (!File.Exists(destinationPdf) || !File.Exists(sourcePdf))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Determine the insertion position (middle of the destination PDF)
        int insertPosition;
        using (Document destDoc = new Document(destinationPdf))
        {
            // Page collection is 1‑based; insert after half the pages
            insertPosition = destDoc.Pages.Count / 2 + 1;
        }

        // Determine how many pages to take from the source PDF (insert all pages)
        int sourcePageCount;
        using (Document srcDoc = new Document(sourcePdf))
        {
            sourcePageCount = srcDoc.Pages.Count;
        }

        // Perform the insertion using PdfFileEditor
        PdfFileEditor editor = new PdfFileEditor();
        bool result = editor.Insert(
            destinationPdf,   // inputFile – the PDF to be modified
            insertPosition,  // insertLocation – position in the input file
            sourcePdf,       // portFile – PDF providing pages to insert
            1,               // startPage – first page of the source PDF
            sourcePageCount, // endPage – last page of the source PDF
            outputPdf);      // outputFile – where the merged PDF will be saved

        if (result)
            Console.WriteLine($"Pages inserted successfully. Output saved to '{outputPdf}'.");
        else
            Console.Error.WriteLine("Failed to insert pages.");
    }
}