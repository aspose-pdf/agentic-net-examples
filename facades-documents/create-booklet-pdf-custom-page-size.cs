using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string outputPath = "booklet.pdf";
        const double pageWidth = 420;   // example width in points
        const double pageHeight = 595;  // example height in points

        // ------------------------------------------------------------
        // 1. Create a minimal source PDF in memory (self‑contained example)
        // ------------------------------------------------------------
        using var sourceDoc = new Document();
        sourceDoc.Pages.Add(); // at least one page so MakeBooklet has something to work with
        using var sourceStream = new MemoryStream();
        sourceDoc.Save(sourceStream);
        sourceStream.Position = 0; // reset for reading by MakeBooklet

        // ------------------------------------------------------------
        // 2. Create the booklet using the stream‑based overload
        // ------------------------------------------------------------
        using var bookletStream = new MemoryStream();
        var editor = new PdfFileEditor();
        bool success = editor.MakeBooklet(sourceStream, bookletStream);
        if (!success)
        {
            Console.WriteLine("Failed to create booklet.");
            return;
        }

        // ------------------------------------------------------------
        // 3. Load the generated booklet, adjust page dimensions, and save
        // ------------------------------------------------------------
        bookletStream.Position = 0; // rewind before loading into Document
        using var bookletDoc = new Document(bookletStream);
        foreach (Page page in bookletDoc.Pages)
        {
            page.PageInfo.Width = pageWidth;
            page.PageInfo.Height = pageHeight;
        }

        // Save the final booklet to the desired file path
        bookletDoc.Save(outputPath);
        Console.WriteLine($"Booklet created at '{outputPath}'.");
    }
}
