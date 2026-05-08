using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the destination PDF, source PDF, and the resulting output PDF
        const string destinationPdf = "destination.pdf";
        const string sourcePdf      = "source.pdf";
        const string outputPdf      = "merged.pdf";

        // Position (1‑based) in the destination PDF where the pages will be inserted
        const int insertPosition = 3;

        // Array of page numbers (1‑based) to take from the source PDF
        int[] pagesToInsert = new int[] { 2, 4, 5 };

        // Verify that the input files exist before proceeding
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

        // Use PdfFileEditor (Aspose.Pdf.Facades) to insert the specified pages
        PdfFileEditor editor = new PdfFileEditor();
        bool result = editor.Insert(
            destinationPdf,   // inputFile – the PDF into which pages will be inserted
            insertPosition,   // insertLocation – position in the destination PDF (1‑based)
            sourcePdf,        // portFile – the PDF providing pages to insert
            pagesToInsert,    // pageNumber – array of pages from the source PDF
            outputPdf);       // outputFile – resulting PDF with pages inserted

        // Report the outcome
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