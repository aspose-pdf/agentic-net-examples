using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the PDFs
        const string sourcePdf   = "source.pdf";   // PDF into which pages will be inserted
        const string insertPdf   = "insert.pdf";   // PDF containing pages to insert
        const string outputPdf   = "merged.pdf";   // Resulting PDF

        // Validate input files
        if (!File.Exists(sourcePdf))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePdf}");
            return;
        }
        if (!File.Exists(insertPdf))
        {
            Console.Error.WriteLine($"Insert file not found: {insertPdf}");
            return;
        }

        try
        {
            // Create the PdfFileEditor facade (does NOT implement IDisposable)
            PdfFileEditor editor = new PdfFileEditor();

            // Define where to insert the pages (1‑based index).
            // Example: insert after the first page of the source PDF.
            int insertLocation = 1;

            // Define which pages from the insert PDF should be taken.
            // Here we take pages 2 and 4 (1‑based indexing).
            int[] pagesToInsert = new int[] { 2, 4 };

            // Perform the insertion.
            // The method returns true if the operation succeeded.
            bool success = editor.Insert(sourcePdf, insertLocation, insertPdf, pagesToInsert, outputPdf);

            if (success)
                Console.WriteLine($"Pages inserted successfully. Output saved to '{outputPdf}'.");
            else
                Console.Error.WriteLine("Insertion failed. Check the editor's LastException for details.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during insertion: {ex.Message}");
        }
    }
}