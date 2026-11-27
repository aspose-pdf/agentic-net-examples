using System;
using System.Drawing;
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

        // Determine the number of pages using the core Document API.
        int pageCount;
        using (Document doc = new Document(inputPath))
        {
            pageCount = doc.Pages.Count; // 1‑based indexing
        }

        // Create and bind the PdfBookmarkEditor.
        using (PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor())
        {
            bookmarkEditor.BindPdf(inputPath);

            // Add a gray, italic bookmark for each page.
            for (int i = 1; i <= pageCount; i++)
            {
                Bookmark bm = new Bookmark();
                bm.Title = $"Page {i}";
                bm.PageNumber = i;
                bm.ItalicFlag = true;               // italic style
                bm.BoldFlag = false;                // not bold
                bm.TitleColor = System.Drawing.Color.Gray; // fully qualified to avoid ambiguity
                // Create the bookmark in the PDF.
                bookmarkEditor.CreateBookmarks(bm);
            }

            // Save the modified PDF with the new bookmarks.
            bookmarkEditor.Save(outputPath);
        }

        Console.WriteLine($"Bookmarks added and saved to '{outputPath}'.");
    }
}
