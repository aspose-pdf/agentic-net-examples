using System;
using System.IO;
using System.Drawing; // for System.Drawing.Color
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

        // Get the total number of pages (1‑based indexing)
        int pageCount;
        using (Document doc = new Document(inputPath))
        {
            pageCount = doc.Pages.Count;
        }

        // Bind the PDF to the bookmark editor and create bookmarks
        using (PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor())
        {
            bookmarkEditor.BindPdf(inputPath);

            // Optional: remove any existing bookmarks
            // bookmarkEditor.DeleteBookmarks();

            // Create a bookmark for each page with red color and no style (default flags)
            for (int i = 1; i <= pageCount; i++)
            {
                Bookmark bm = new Bookmark();
                bm.Title = $"Page {i}";
                bm.PageNumber = i;
                bm.TitleColor = System.Drawing.Color.Red; // fully qualified to avoid CS0104
                // BoldFlag and ItalicFlag default to false → no style
                bookmarkEditor.CreateBookmarks(bm);
            }

            // Save the PDF with the new bookmarks
            bookmarkEditor.Save(outputPath);
        }

        Console.WriteLine($"Bookmarks added and saved to '{outputPath}'.");
    }
}
