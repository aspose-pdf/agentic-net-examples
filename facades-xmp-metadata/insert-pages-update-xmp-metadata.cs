using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF, PDF whose pages will be inserted, and final output path
        const string sourcePdf = "source.pdf";
        const string insertPdf = "insert.pdf";
        const string tempPdf   = "temp_combined.pdf";
        const string outputPdf = "final.pdf";

        // Pages from insertPdf to be inserted (1‑based indexing)
        int[] pagesToInsert = new int[] { 1, 2 };

        // ---------- Insert pages ----------
        // PdfFileEditor does NOT implement IDisposable, so do NOT wrap it in a using block.
        var editor = new PdfFileEditor();
        bool success = editor.TryInsert(sourcePdf, 1, insertPdf, pagesToInsert, tempPdf);
        if (!success)
        {
            Console.Error.WriteLine("Page insertion failed.");
            return;
        }

        // ---------- Update XMP metadata ----------
        // PdfXmpMetadata *does* implement IDisposable, so a using block is appropriate.
        using (PdfXmpMetadata xmp = new PdfXmpMetadata())
        {
            xmp.BindPdf(tempPdf);

            // Add or overwrite XMP entries.
            xmp.Add("dc:creator", "John Doe");
            xmp.Add("dc:title",   "Combined Document");
            xmp.Add("pdf:Producer", "Aspose.Pdf for .NET");

            // Save the PDF with updated metadata.
            xmp.Save(outputPdf);
        }

        // Clean up the temporary file.
        try { File.Delete(tempPdf); } catch { }

        Console.WriteLine($"Output saved to '{outputPdf}'.");
    }
}
