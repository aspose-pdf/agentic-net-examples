using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the destination PDF, source PDF (pages to insert), and the result PDF.
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

        // Define the insertion point (1‑based page index) in the destination PDF.
        // For example, insert after page 3 (i.e., at position 4).
        int insertLocation = 4;

        // Define the page range from the source PDF to be inserted.
        // Here we insert pages 2 through 5 (inclusive) from the source PDF.
        int startPage = 2;
        int endPage   = 5;

        // Create the PdfFileEditor facade. It does NOT implement IDisposable and has no Close method.
        PdfFileEditor editor = new PdfFileEditor();

        try
        {
            // Perform the insertion. The method returns true on success.
            bool success = editor.Insert(
                destinationPdf,   // inputFile: the original PDF
                insertLocation,   // insertLocation: position where pages will be inserted
                sourcePdf,        // portFile: PDF containing pages to insert
                startPage,        // startPage: first page to take from sourcePdf
                endPage,          // endPage: last page to take from sourcePdf
                outputPdf);       // outputFile: resulting PDF with inserted pages

            if (success)
                Console.WriteLine($"Pages inserted successfully. Output saved to '{outputPdf}'.");
            else
                Console.Error.WriteLine("Insertion failed.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during insertion: {ex.Message}");
        }
        // No explicit Close() call is required for PdfFileEditor.
    }
}
