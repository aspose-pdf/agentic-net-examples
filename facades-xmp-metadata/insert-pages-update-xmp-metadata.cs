using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf;   // for XmpValue

class Program
{
    static void Main()
    {
        // Input PDF, PDF whose pages will be inserted, and final output PDF
        const string sourcePdf   = "source.pdf";
        const string insertPdf   = "insert.pdf";
        const string finalPdf    = "result.pdf";

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

        // Temporary file that will hold the document after page insertion
        string tempPdf = Path.GetTempFileName();

        // Pages to insert from insertPdf (1‑based indexing)
        int[] pagesToInsert = new int[] { 1, 2 };   // example: insert first two pages

        // Position where pages will be inserted in sourcePdf (1‑based)
        int insertPosition = 1; // insert after the first page

        // ---------- Page insertion using PdfFileEditor ----------
        // PdfFileEditor does NOT implement IDisposable, so do NOT use a using block.
        var editor = new PdfFileEditor();
        bool success = editor.TryInsert(sourcePdf, insertPosition, insertPdf, pagesToInsert, tempPdf);
        if (!success)
        {
            Console.Error.WriteLine("Page insertion failed.");
            return;
        }

        // ---------- XMP metadata update using PdfXmpMetadata ----------
        using (PdfXmpMetadata xmp = new PdfXmpMetadata())
        {
            // Bind the temporary PDF (result of insertion)
            xmp.BindPdf(tempPdf);

            // Add or update XMP metadata entries.
            // The key follows the XMP namespace prefix (e.g., "dc:creator").
            xmp.Add("dc:creator", "John Doe");
            xmp.Add("dc:title", "Combined Document");
            xmp.Add("dc:description", "Document with inserted pages and updated metadata.");

            // Save the final PDF with the new metadata.
            xmp.Save(finalPdf);
        }

        // Clean up the temporary file
        try { File.Delete(tempPdf); } catch { /* ignore cleanup errors */ }

        Console.WriteLine($"PDF successfully created: {finalPdf}");
    }
}
