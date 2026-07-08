using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input files
        const string originalPdf = "original.pdf";   // PDF from which pages will be removed
        const string otherPdf    = "other.pdf";      // PDF to concatenate after editing
        // Intermediate and final output files
        const string tempPdf     = "original_edited.pdf";
        const string finalPdf    = "merged.pdf";

        // Pages to delete (1‑based indexing as required by Aspose.Pdf)
        int[] pagesToDelete = new int[] { 2, 3 };

        // Verify that source files exist
        if (!File.Exists(originalPdf))
        {
            Console.Error.WriteLine($"File not found: {originalPdf}");
            return;
        }
        if (!File.Exists(otherPdf))
        {
            Console.Error.WriteLine($"File not found: {otherPdf}");
            return;
        }

        // Create the PdfFileEditor facade (does NOT implement IDisposable)
        PdfFileEditor editor = new PdfFileEditor();

        // OPTIONAL: close streams automatically after each operation
        // editor.CloseConcatenatedStreams = true;

        // 1. Delete specified pages from the original PDF and save to a temporary file
        bool deleteResult = editor.Delete(originalPdf, pagesToDelete, tempPdf);
        if (!deleteResult)
        {
            Console.Error.WriteLine("Failed to delete pages from the PDF.");
            return;
        }

        // 2. Concatenate the edited PDF with the second PDF into the final output file
        bool concatResult = editor.Concatenate(tempPdf, otherPdf, finalPdf);
        if (!concatResult)
        {
            Console.Error.WriteLine("Failed to concatenate PDFs.");
            return;
        }

        Console.WriteLine($"Successfully created merged PDF: '{finalPdf}'.");
    }
}