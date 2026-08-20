using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the destination PDF, the source PDF whose pages will be appended,
        // and the resulting merged PDF.
        const string destinationPdf = "destination.pdf";
        const string sourcePdf      = "source.pdf";
        const string outputPdf      = "merged.pdf";

        // Verify that the input files exist.
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
        int sourcePageCount;
        using (Document srcDoc = new Document(sourcePdf))
        {
            sourcePageCount = srcDoc.Pages.Count; // 1‑based page count
        }

        // Append all pages from the source PDF to the end of the destination PDF.
        // The Append method handles opening the files and writing the output.
        PdfFileEditor editor = new PdfFileEditor();
        bool success = editor.Append(
            inputFile:  destinationPdf,   // original PDF
            portFile:   sourcePdf,        // PDF to take pages from
            startPage:  1,                // start from first page of source
            endPage:    sourcePageCount,  // up to the last page of source
            outputFile: outputPdf);       // resulting PDF

        if (success)
        {
            Console.WriteLine($"Pages appended successfully. Output saved to '{outputPdf}'.");
        }
        else
        {
            Console.Error.WriteLine("Failed to append pages.");
        }
    }
}