using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the source PDF (with bookmarks), the target PDF (without bookmarks),
        // the intermediate XML file that holds the bookmark hierarchy, and the final output PDF.
        const string sourcePdf = "source.pdf";
        const string targetPdf = "target.pdf";
        const string outputPdf = "output_with_bookmarks.pdf";
        const string tempXml   = "bookmarks.xml";

        // Verify that the input files exist.
        if (!File.Exists(sourcePdf))
        {
            Console.Error.WriteLine($"Source PDF not found: {sourcePdf}");
            return;
        }
        if (!File.Exists(targetPdf))
        {
            Console.Error.WriteLine($"Target PDF not found: {targetPdf}");
            return;
        }

        // ------------------------------------------------------------
        // Export bookmarks from the source PDF to an XML file.
        // ------------------------------------------------------------
        PdfBookmarkEditor exporter = new PdfBookmarkEditor();
        exporter.BindPdf(sourcePdf);                     // Load the source PDF.
        exporter.ExportBookmarksToXML(tempXml);          // Export the full bookmark hierarchy.
        exporter.Close();                               // Release resources.

        // ------------------------------------------------------------
        // Import the exported bookmarks into the target PDF.
        // ------------------------------------------------------------
        PdfBookmarkEditor importer = new PdfBookmarkEditor();
        importer.BindPdf(targetPdf);                     // Load the target PDF.
        importer.ImportBookmarksWithXML(tempXml);        // Import the hierarchy from XML.
        importer.Save(outputPdf);                        // Save the result.
        importer.Close();                               // Release resources.

        // Clean up the temporary XML file.
        try { File.Delete(tempXml); } catch { }

        Console.WriteLine($"Bookmarks successfully imported. Output saved to '{outputPdf}'.");
    }
}