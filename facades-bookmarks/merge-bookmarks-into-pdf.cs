using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string targetPdfPath = "target.pdf";          // PDF to receive bookmarks
        const string sourcePdfPath = "source.pdf";          // PDF providing bookmarks
        const string outputPdfPath = "merged_bookmarks.pdf"; // Resulting PDF

        if (!File.Exists(targetPdfPath))
        {
            Console.Error.WriteLine($"Target file not found: {targetPdfPath}");
            return;
        }
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePdfPath}");
            return;
        }

        // Load the target document (the one we will modify)
        using (Document targetDoc = new Document(targetPdfPath))
        {
            // Facade for editing bookmarks in the target document
            using (PdfBookmarkEditor targetEditor = new PdfBookmarkEditor())
            {
                targetEditor.BindPdf(targetDoc);

                // Load the source document (the one we read bookmarks from)
                using (Document sourceDoc = new Document(sourcePdfPath))
                {
                    // Facade for extracting bookmarks from the source document
                    using (PdfBookmarkEditor sourceEditor = new PdfBookmarkEditor())
                    {
                        sourceEditor.BindPdf(sourceDoc);

                        // Extract all bookmarks (including hierarchy) from source PDF
                        Bookmarks sourceBookmarks = sourceEditor.ExtractBookmarks();

                        // Append each top‑level bookmark (with its children) to the target PDF
                        foreach (Bookmark bm in sourceBookmarks)
                        {
                            // CreateBookmarks adds the bookmark hierarchy preserving order
                            targetEditor.CreateBookmarks(bm);
                        }
                    }
                }

                // Save the modified target document with merged bookmarks
                targetDoc.Save(outputPdfPath);
            }
        }

        Console.WriteLine($"Bookmarks merged successfully. Output saved to '{outputPdfPath}'.");
    }
}