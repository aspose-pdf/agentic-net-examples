using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "bookmarked_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (Document implements IDisposable)
        using (Document doc = new Document(inputPath))
        {
            // PdfBookmarkEditor also implements IDisposable
            using (PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor())
            {
                // Bind the loaded document to the editor
                bookmarkEditor.BindPdf(doc);

                // Create a bookmark for each page with orange color and bold style
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    Bookmark bm = new Bookmark();
                    bm.Title = $"Page {i}";
                    bm.PageNumber = i;
                    // Fully qualify System.Drawing.Color to avoid ambiguity with Aspose.Pdf.Color
                    bm.TitleColor = System.Drawing.Color.FromArgb(255, 255, 128, 0); // orange (R=255,G=128,B=0)
                    bm.BoldFlag = true; // make the title bold

                    // Add the bookmark to the document via the editor
                    bookmarkEditor.CreateBookmarks(bm);
                }

                // Save the PDF with the new bookmarks
                bookmarkEditor.Save(outputPath);
            }
        }

        Console.WriteLine($"Bookmarks added and saved to '{outputPath}'.");
    }
}
