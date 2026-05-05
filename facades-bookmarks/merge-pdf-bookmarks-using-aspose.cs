using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the source PDF (contains the bookmarks to import),
        // the target PDF (the document we want to add the bookmarks to),
        // and the resulting PDF.
        const string sourcePdfPath = "source_with_bookmarks.pdf";
        const string targetPdfPath = "target.pdf";
        const string outputPdfPath = "merged_bookmarks.pdf";

        // Verify that the input files exist.
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {sourcePdfPath}");
            return;
        }
        if (!File.Exists(targetPdfPath))
        {
            Console.Error.WriteLine($"Target PDF not found: {targetPdfPath}");
            return;
        }

        // Use a memory stream to hold the exported bookmarks XML.
        using (MemoryStream bookmarkXml = new MemoryStream())
        {
            // ---------- Export bookmarks from the source PDF ----------
            PdfBookmarkEditor exportEditor = new PdfBookmarkEditor();
            exportEditor.BindPdf(sourcePdfPath);
            // Export all bookmarks to the memory stream as XML.
            exportEditor.ExportBookmarksToXML(bookmarkXml);
            // Close the editor for the source PDF.
            exportEditor.Close();

            // Reset the stream position before reading it for import.
            bookmarkXml.Position = 0;

            // ---------- Import bookmarks into the target PDF ----------
            PdfBookmarkEditor importEditor = new PdfBookmarkEditor();
            importEditor.BindPdf(targetPdfPath);
            // Import the previously exported bookmarks XML.
            importEditor.ImportBookmarksWithXML(bookmarkXml);
            // Save the target PDF with the newly added bookmarks.
            importEditor.Save(outputPdfPath);
            // Close the editor for the target PDF.
            importEditor.Close();
        }

        Console.WriteLine($"Bookmarks merged successfully. Output saved to '{outputPdfPath}'.");
    }
}