using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the destination PDF, source PDF and the resulting PDF
        const string destinationPdf = "destination.pdf";
        const string sourcePdf      = "source.pdf";
        const string outputPdf      = "merged.pdf";

        // Verify that the input files exist
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

        // Determine all page numbers of the source PDF
        int[] sourcePages;
        using (Document srcDoc = new Document(sourcePdf))
        {
            int pageCount = srcDoc.Pages.Count; // 1‑based page count
            sourcePages = new int[pageCount];
            for (int i = 0; i < pageCount; i++)
                sourcePages[i] = i + 1; // pages are 1‑based
        }

        // PdfFileEditor does NOT implement IDisposable, so do NOT wrap it in a using block
        PdfFileEditor editor = new PdfFileEditor();

        // Insert the source pages at the beginning (position 1) of the destination PDF
        // Insert(string inputFile, int insertLocation, string portFile, int[] pageNumber, string outputFile)
        bool success = editor.Insert(destinationPdf, 1, sourcePdf, sourcePages, outputPdf);

        if (success)
            Console.WriteLine($"Pages from '{sourcePdf}' inserted at the beginning of '{destinationPdf}'. Result saved as '{outputPdf}'.");
        else
            Console.Error.WriteLine("Failed to insert pages using PdfFileEditor.");
    }
}