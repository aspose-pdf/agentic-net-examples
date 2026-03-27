using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the base PDF and source PDFs
        string basePdfPath = "base.pdf";
        string sourcePdfPath1 = "source1.pdf";
        string sourcePdfPath2 = "source2.pdf";
        string intermediatePdfPath = "intermediate.pdf";
        string finalPdfPath = "merged.pdf";

        // Verify that all input files exist
        if (!File.Exists(basePdfPath))
        {
            Console.Error.WriteLine($"Base PDF not found: {basePdfPath}");
            return;
        }
        if (!File.Exists(sourcePdfPath1))
        {
            Console.Error.WriteLine($"Source PDF 1 not found: {sourcePdfPath1}");
            return;
        }
        if (!File.Exists(sourcePdfPath2))
        {
            Console.Error.WriteLine($"Source PDF 2 not found: {sourcePdfPath2}");
            return;
        }

        // Create the PdfFileEditor instance (it does not implement IDisposable)
        PdfFileEditor editor = new PdfFileEditor();

        // Define page numbers to be taken from each source PDF (1‑based indexing)
        int[] pagesFromSource1 = new int[] { 2, 4 };
        int[] pagesFromSource2 = new int[] { 1, 3, 5 };

        // Insert pages from the first source PDF after the first page of the base PDF
        // The result is written to an intermediate file
        int insertLocation = 1; // after page 1 of the base PDF
        bool firstInsertSuccess = editor.Insert(basePdfPath, insertLocation, sourcePdfPath1, pagesFromSource1, intermediatePdfPath);
        if (!firstInsertSuccess)
        {
            Console.Error.WriteLine("Failed to insert pages from source1.pdf.");
            return;
        }

        // Insert pages from the second source PDF after the first page of the intermediate PDF
        // The final result is written to the output file
        bool secondInsertSuccess = editor.Insert(intermediatePdfPath, insertLocation, sourcePdfPath2, pagesFromSource2, finalPdfPath);
        if (!secondInsertSuccess)
        {
            Console.Error.WriteLine("Failed to insert pages from source2.pdf.");
            return;
        }

        // Clean up the intermediate file
        try
        {
            File.Delete(intermediatePdfPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Could not delete intermediate file: {ex.Message}");
        }

        Console.WriteLine($"Pages inserted successfully. Output saved to '{finalPdfPath}'.");
    }
}