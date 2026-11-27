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

        // Determine the number of pages in the PDF
        int pageCount;
        using (Document doc = new Document(inputPath))
        {
            pageCount = doc.Pages.Count;
        }

        // Build a root bookmark that will contain a child bookmark for each page.
        Bookmark rootBookmark = new Bookmark
        {
            Title = "Pages",
            ItalicFlag = true,
            Action = "GoTo",
            PageNumber = 1 // any existing page can be used for the root
        };

        // Add a child bookmark for every page.
        for (int i = 0; i < pageCount; i++)
        {
            Bookmark child = new Bookmark
            {
                Title = $"Page {i + 1}",
                PageNumber = i + 1,
                ItalicFlag = true,
                Action = "GoTo"
            };
            rootBookmark.ChildItems.Add(child);
        }

        // Bind the PDF and create the bookmarks.
        PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor();
        bookmarkEditor.BindPdf(inputPath);
        bookmarkEditor.CreateBookmarks(rootBookmark);
        bookmarkEditor.Save(outputPath);

        Console.WriteLine($"Bookmarks added and saved to '{outputPath}'.");
    }
}
