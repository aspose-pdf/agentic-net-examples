using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "bookmarked.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Get page count using Document (lifecycle rule: wrap in using)
        int pageCount;
        using (Document doc = new Document(inputPath))
        {
            pageCount = doc.Pages.Count; // 1‑based indexing
        }

        // PdfBookmarkEditor does not implement IDisposable
        PdfBookmarkEditor editor = new PdfBookmarkEditor();
        editor.BindPdf(inputPath);

        // Create a pink colored regular bookmark for each page
        for (int i = 1; i <= pageCount; i++)
        {
            Bookmark bm = new Bookmark();
            bm.Title = $"Page {i}";
            bm.PageNumber = i;
            // Fully qualify the System.Drawing.Color to avoid ambiguity with Aspose.Pdf.Color
            bm.TitleColor = System.Drawing.Color.Pink;
            // CreateBookmarks takes a single Bookmark instance
            editor.CreateBookmarks(bm);
        }

        // Save the modified PDF and close the editor
        editor.Save(outputPath);
        editor.Close();

        Console.WriteLine($"Bookmarks added and saved to '{outputPath}'.");
    }
}
