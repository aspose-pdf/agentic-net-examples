using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output_bookmarked.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Initialize the bookmark editor and bind the PDF file
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            editor.BindPdf(inputPdf);

            // Build hierarchical bookmarks
            // Example hierarchy:
            // Chapter 1 (page 1)
            //   Section 1.1 (page 2)
            //   Section 1.2 (page 3)
            // Chapter 2 (page 5)
            //   Section 2.1 (page 6)

            // Leaf bookmarks for Chapter 1
            Bookmark sec11 = new Bookmark
            {
                Title = "Section 1.1",
                PageNumber = 2,
                Action = "GoTo"
            };
            Bookmark sec12 = new Bookmark
            {
                Title = "Section 1.2",
                PageNumber = 3,
                Action = "GoTo"
            };
            // Group sections under Chapter 1
            Bookmarks chapter1Children = new Bookmarks();
            chapter1Children.Add(sec11);
            chapter1Children.Add(sec12);
            Bookmark chapter1 = new Bookmark
            {
                Title = "Chapter 1",
                PageNumber = 1,
                Action = "GoTo",
                ChildItem = chapter1Children
            };

            // Leaf bookmark for Chapter 2
            Bookmark sec21 = new Bookmark
            {
                Title = "Section 2.1",
                PageNumber = 6,
                Action = "GoTo"
            };
            // Group under Chapter 2
            Bookmarks chapter2Children = new Bookmarks();
            chapter2Children.Add(sec21);
            Bookmark chapter2 = new Bookmark
            {
                Title = "Chapter 2",
                PageNumber = 5,
                Action = "GoTo",
                ChildItem = chapter2Children
            };

            // Top‑level collection of chapters
            Bookmarks rootBookmarks = new Bookmarks();
            rootBookmarks.Add(chapter1);
            rootBookmarks.Add(chapter2);

            // Add each top‑level bookmark (which includes its children) to the PDF
            foreach (Bookmark bm in rootBookmarks)
            {
                editor.CreateBookmarks(bm);
            }

            // Save the PDF with the new bookmark hierarchy
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Bookmarks added and saved to '{outputPdf}'.");
    }
}