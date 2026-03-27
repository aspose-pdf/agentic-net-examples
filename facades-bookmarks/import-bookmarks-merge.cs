using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string sourcePdfPath = "source.pdf";
        const string targetPdfPath = "target.pdf";
        const string outputPdfPath = "merged_bookmarks.pdf";

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

        // Export bookmarks from the source PDF to an in‑memory XML stream
        using (Document sourceDoc = new Document(sourcePdfPath))
        using (MemoryStream bookmarkStream = new MemoryStream())
        {
            PdfBookmarkEditor exporter = new PdfBookmarkEditor(sourceDoc);
            exporter.ExportBookmarksToXML(bookmarkStream);
            // Reset stream position for reading
            bookmarkStream.Position = 0;

            // Import the exported bookmarks into the target PDF
            using (Document targetDoc = new Document(targetPdfPath))
            {
                PdfBookmarkEditor importer = new PdfBookmarkEditor(targetDoc);
                importer.ImportBookmarksWithXML(bookmarkStream);
                targetDoc.Save(outputPdfPath);
            }
        }

        Console.WriteLine($"Bookmarks merged and saved to '{outputPdfPath}'.");
    }
}