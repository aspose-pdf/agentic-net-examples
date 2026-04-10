using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string targetPdfPath   = "target.pdf";   // PDF to receive the bookmarks
        const string sourcePdfPath   = "source.pdf";   // PDF whose bookmarks will be imported
        const string tempXmlPath     = "tempBookmarks.xml";

        // Ensure source and target files exist
        if (!File.Exists(targetPdfPath))
        {
            Console.Error.WriteLine($"Target PDF not found: {targetPdfPath}");
            return;
        }
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {sourcePdfPath}");
            return;
        }

        try
        {
            // ---------- Export bookmarks from the source PDF to an XML file ----------
            PdfBookmarkEditor sourceEditor = new PdfBookmarkEditor();
            sourceEditor.BindPdf(sourcePdfPath);
            // ExportBookmarksToXML creates an XML representation preserving order
            sourceEditor.ExportBookmarksToXML(tempXmlPath);
            sourceEditor.Close(); // Release the bound document

            // ---------- Import the exported bookmarks into the target PDF ----------
            PdfBookmarkEditor targetEditor = new PdfBookmarkEditor();
            targetEditor.BindPdf(targetPdfPath);
            // ImportBookmarksWithXML reads the XML and adds the bookmarks in the same order
            targetEditor.ImportBookmarksWithXML(tempXmlPath);
            // Save the updated PDF (overwrites the original target file)
            targetEditor.Save("merged_with_bookmarks.pdf");
            targetEditor.Close(); // Release the bound document

            // Clean up temporary XML file
            if (File.Exists(tempXmlPath))
                File.Delete(tempXmlPath);

            Console.WriteLine("Bookmarks imported successfully. Output saved as 'merged_with_bookmarks.pdf'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}