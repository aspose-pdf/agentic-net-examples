using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string targetPath = "target.pdf";          // PDF to receive bookmarks
        const string sourcePath = "source.pdf";          // PDF containing bookmarks
        const string outputPath = "merged_bookmarks.pdf"; // Resulting PDF

        if (!File.Exists(targetPath) || !File.Exists(sourcePath))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Export bookmarks from the source PDF to an in‑memory XML stream
        using (MemoryStream bookmarkXml = new MemoryStream())
        {
            using (PdfBookmarkEditor sourceEditor = new PdfBookmarkEditor())
            {
                sourceEditor.BindPdf(sourcePath);
                sourceEditor.ExportBookmarksToXML(bookmarkXml);
                bookmarkXml.Position = 0; // Reset for reading
            }

            // Load the target PDF and import the exported bookmarks
            using (Document targetDoc = new Document(targetPath))
            {
                using (PdfBookmarkEditor targetEditor = new PdfBookmarkEditor())
                {
                    targetEditor.BindPdf(targetDoc);
                    targetEditor.ImportBookmarksWithXML(bookmarkXml);
                    targetEditor.Save(outputPath); // Save the merged document
                }
            }
        }

        Console.WriteLine($"Bookmarks merged successfully. Output saved to '{outputPath}'.");
    }
}