using System;
using System.IO;
using Aspose.Pdf;                     // Document class for page count
using Aspose.Pdf.Facades;            // PdfFileEditor facade

class Program
{
    static void Main()
    {
        const string destinationPdf = "destination.pdf"; // PDF to receive pages
        const string sourcePdf      = "source.pdf";      // PDF whose pages will be inserted
        const string outputPdf      = "merged.pdf";      // Resulting PDF

        // Verify that input files exist
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

        try
        {
            // Load the source PDF to determine the number of pages to insert
            int sourcePageCount;
            using (Document srcDoc = new Document(sourcePdf))
            {
                sourcePageCount = srcDoc.Pages.Count;
            }

            // Insert all pages from sourcePdf at the beginning (position 1) of destinationPdf
            PdfFileEditor editor = new PdfFileEditor();
            bool success = editor.Insert(
                inputFile:      destinationPdf,   // target PDF
                insertLocation: 1,               // insert at the very start (1‑based)
                portFile:       sourcePdf,        // PDF providing pages
                startPage:      1,                // first page of source
                endPage:        sourcePageCount,  // last page of source
                outputFile:     outputPdf);       // resulting PDF

            Console.WriteLine(success
                ? $"Pages inserted successfully. Output saved to '{outputPdf}'."
                : "Insertion failed.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}