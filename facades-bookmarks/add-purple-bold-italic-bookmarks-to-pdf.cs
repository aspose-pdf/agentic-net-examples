using System;
using System.IO;
using System.Drawing; // for System.Drawing.Color
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "bookmarked.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the document to obtain the page count (PdfBookmarkEditor does not expose it).
        Document doc = new Document(inputPath);
        int pageCount = doc.Pages.Count; // 1‑based page count

        // PdfBookmarkEditor does NOT implement IDisposable, so we manage its lifetime manually.
        PdfBookmarkEditor editor = new PdfBookmarkEditor();

        // Bind the source PDF.
        editor.BindPdf(inputPath);

        // Create a purple, bold, italic bookmark for each page.
        for (int i = 1; i <= pageCount; i++)
        {
            Bookmark bm = new Bookmark
            {
                Title      = $"Page {i}",
                PageNumber = i,
                BoldFlag   = true,
                ItalicFlag = true,
                TitleColor = System.Drawing.Color.Purple // fully qualified to avoid ambiguity
                // Action defaults to "GoTo" – no need to set explicitly.
            };

            // Add the bookmark to the editor. The overload accepts a single Bookmark.
            editor.CreateBookmarks(bm);
        }

        // Save the PDF with the new bookmarks.
        editor.Save(outputPath);

        // Release any internal resources held by the editor.
        editor.Close();

        Console.WriteLine($"Bookmarks added and saved to '{outputPath}'.");
    }
}
