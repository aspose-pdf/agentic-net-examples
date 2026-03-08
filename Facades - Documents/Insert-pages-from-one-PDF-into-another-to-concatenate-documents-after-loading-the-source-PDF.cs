using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the PDFs
        const string sourcePdf   = "source.pdf";   // PDF that will receive the inserted pages
        const string insertPdf   = "insert.pdf";   // PDF containing pages to be inserted
        const string outputPdf   = "merged.pdf";   // Resulting PDF after insertion

        // Position (1‑based) in the source PDF where pages will be inserted.
        // For example, 2 means insert after the first page.
        const int insertLocation = 2;

        // Page numbers (1‑based) from the insertPdf that should be inserted.
        // Adjust the array as needed.
        int[] pagesToInsert = new int[] { 1, 3, 5 };

        // Verify that the input files exist.
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

        // Create the PdfFileEditor facade (it does NOT implement IDisposable).
        PdfFileEditor editor = new PdfFileEditor();

        // Perform the insertion.
        // This method inserts the specified pages from insertPdf into sourcePdf
        // at the given location and writes the result to outputPdf.
        bool success = editor.Insert(sourcePdf, insertLocation, insertPdf, pagesToInsert, outputPdf);

        if (success)
        {
            Console.WriteLine($"Pages inserted successfully. Output saved to '{outputPdf}'.");
        }
        else
        {
            Console.Error.WriteLine("Insertion failed. See editor.LastException for details if needed.");
        }
    }
}