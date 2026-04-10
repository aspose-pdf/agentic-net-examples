using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input files
        const string firstPdfPath  = "input.pdf";      // PDF from which pages will be removed
        const string secondPdfPath = "second.pdf";     // PDF to concatenate after deletion
        // Temporary file to hold the result of the delete operation
        const string tempPdfPath   = "temp_deleted.pdf";
        // Final output file after concatenation
        const string outputPdfPath = "merged.pdf";

        // Pages to delete – note that Aspose.Pdf uses 1‑based indexing
        int[] pagesToDelete = new int[] { 2, 3 }; // example: remove pages 2 and 3

        // Verify that source files exist
        if (!File.Exists(firstPdfPath))
        {
            Console.Error.WriteLine($"File not found: {firstPdfPath}");
            return;
        }
        if (!File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine($"File not found: {secondPdfPath}");
            return;
        }

        // PdfFileEditor does NOT implement IDisposable, so we instantiate it directly
        PdfFileEditor editor = new PdfFileEditor();

        // 1) Delete the specified pages from the first PDF and write to a temporary file
        bool deleteResult = editor.Delete(firstPdfPath, pagesToDelete, tempPdfPath);
        if (!deleteResult)
        {
            Console.Error.WriteLine("Page deletion failed.");
            return;
        }

        // 2) Concatenate the edited PDF (tempPdfPath) with the second PDF
        bool concatResult = editor.Concatenate(tempPdfPath, secondPdfPath, outputPdfPath);
        if (!concatResult)
        {
            Console.Error.WriteLine("Concatenation failed.");
            return;
        }

        Console.WriteLine($"Successfully created merged PDF: '{outputPdfPath}'.");
    }
}