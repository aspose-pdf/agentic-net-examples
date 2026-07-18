using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string sourcePdf = "source.pdf";   // PDF into which pages will be inserted
        const string insertPdf = "insert.pdf";   // PDF providing pages to insert
        const string intermediatePdf = "intermediate.pdf"; // Result after insertion
        const string finalPdf = "final.pdf";    // Result after XMP update

        // ------------------------------------------------------------
        // 1. Insert pages using PdfFileEditor (Facades API)
        // ------------------------------------------------------------
        // PdfFileEditor does NOT implement IDisposable, so we instantiate it
        // without a using‑statement.
        var fileEditor = new PdfFileEditor();
        bool insertSuccess = fileEditor.TryInsert(
            sourcePdf,          // input file
            2,                  // insert location (after first page)
            insertPdf,          // file providing pages to insert
            new int[] { 1, 2 }, // page numbers to take from insertPdf
            intermediatePdf);   // output file with pages inserted

        if (!insertSuccess)
        {
            Console.Error.WriteLine("Page insertion failed.");
            return;
        }

        // ------------------------------------------------------------
        // 2. Update XMP metadata using PdfXmpMetadata (Facades API)
        // ------------------------------------------------------------
        using (var xmp = new PdfXmpMetadata())
        {
            // Bind the intermediate PDF that already contains the inserted pages
            xmp.BindPdf(intermediatePdf);

            // The indexer works with plain string values – there is no XmpValue type.
            xmp["dc:creator"] = "John Doe";
            xmp["dc:title"]   = "Combined Document";
            xmp["pdf:producer"] = "Aspose.Pdf for .NET";

            // Save the PDF with the updated XMP metadata to the final file.
            xmp.Save(finalPdf);
        }
    }
}
