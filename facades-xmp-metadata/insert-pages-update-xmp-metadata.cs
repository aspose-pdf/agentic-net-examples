using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF, PDF to be inserted, and final output paths
        const string sourcePdf   = "input.pdf";
        const string insertPdf   = "insert.pdf";
        const string tempPdf     = "temp_merged.pdf";
        const string finalPdf    = "output.pdf";

        // Validate files exist
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

        // ------------------------------------------------------------
        // 1. Insert pages from insertPdf into sourcePdf.
        //    Insert after page 2 (position = 2), inserting pages 1‑3 from insertPdf.
        // ------------------------------------------------------------
        PdfFileEditor editor = new PdfFileEditor();
        // Parameters: (inputFile, insertAtPage, insertFile, startPage, endPage, outputFile)
        editor.Insert(sourcePdf, 2, insertPdf, 1, 3, tempPdf);

        // ------------------------------------------------------------
        // 2. Update XMP metadata on the merged PDF.
        //    Use PdfXmpMetadata to bind to the temporary PDF, add entries,
        //    and save the final PDF with updated metadata.
        // ------------------------------------------------------------
        PdfXmpMetadata xmp = new PdfXmpMetadata();
        xmp.BindPdf(tempPdf);

        // Add or replace XMP properties. Keys follow the XMP namespace prefixes.
        xmp.Add("dc:creator", "My Application");
        xmp.Add("dc:title", "Combined Document");
        xmp.Add("dc:description", "PDF with inserted pages and updated XMP metadata.");

        // Save the PDF with the new XMP metadata.
        xmp.Save(finalPdf);

        // Clean up the intermediate file.
        try { File.Delete(tempPdf); } catch { /* ignore cleanup errors */ }

        Console.WriteLine($"PDF created with inserted pages and updated XMP metadata: {finalPdf}");
    }
}