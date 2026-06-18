using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the destination PDF, source PDF, and the resulting output PDF
        const string destinationPdf = "dest.pdf";
        const string sourcePdf = "source.pdf";
        const string outputPdf = "merged.pdf";

        // Position (1‑based) in the destination PDF where pages will be inserted
        // For example, 2 means after the first page
        const int insertPosition = 2;

        // Specific pages from the source PDF to insert (1‑based page numbers)
        int[] pagesToInsert = new int[] { 3, 5, 7 };

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

        // PdfFileEditor does not implement IDisposable, so no using block is required
        PdfFileEditor editor = new PdfFileEditor();

        try
        {
            // Insert the specified pages from sourcePdf into destinationPdf at insertPosition
            // The method returns true on success, false otherwise
            bool result = editor.Insert(destinationPdf, insertPosition, sourcePdf, pagesToInsert, outputPdf);

            if (result)
                Console.WriteLine($"Pages inserted successfully. Output saved to '{outputPdf}'.");
            else
                Console.Error.WriteLine("Insert operation failed.");
        }
        catch (Exception ex)
        {
            // Handle any unexpected errors during the insertion process
            Console.Error.WriteLine($"Error during insertion: {ex.Message}");
        }
    }
}