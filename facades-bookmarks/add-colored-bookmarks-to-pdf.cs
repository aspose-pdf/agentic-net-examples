using System;
using System.IO;
using System.Drawing; // for Color
using Aspose.Pdf.Facades; // PdfBookmarkEditor, Bookmark, Bookmarks

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_bookmarked.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Bind the PDF to the bookmark editor
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            editor.BindPdf(inputPath);

            // ----- Warning bookmark (red) -----
            Bookmark warningBookmark = new Bookmark
            {
                Title       = "Warnings",
                PageNumber  = 1,                     // adjust to the actual page
                TitleColor  = System.Drawing.Color.Red,
                BoldFlag    = true,
                ItalicFlag = false
            };

            // ----- Information bookmark (green) -----
            Bookmark infoBookmark = new Bookmark
            {
                Title       = "Information",
                PageNumber  = 2,                     // adjust to the actual page
                TitleColor  = System.Drawing.Color.Green,
                BoldFlag    = false,
                ItalicFlag = false
            };

            // Optional parent bookmark to group the sections
            Bookmark rootBookmark = new Bookmark
            {
                Title      = "Document Sections",
                TitleColor = System.Drawing.Color.Black
            };

            // Attach child bookmarks to the parent
            Bookmarks childBookmarks = new Bookmarks();
            childBookmarks.Add(warningBookmark);
            childBookmarks.Add(infoBookmark);
            rootBookmark.ChildItems = childBookmarks; // hierarchy

            // Add the hierarchy to the PDF
            editor.CreateBookmarks(rootBookmark);

            // Save the updated PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Bookmarks added and saved to '{outputPath}'.");
    }
}