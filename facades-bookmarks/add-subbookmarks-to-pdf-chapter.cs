using System;
using System.IO;
using Aspose.Pdf.Facades;   // PdfBookmarkEditor, Bookmark, Bookmarks

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_subbookmarks.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Bind the PDF and create a hierarchical bookmark structure
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            editor.BindPdf(inputPath);

            // Parent bookmark representing a chapter
            Aspose.Pdf.Facades.Bookmark parent = new Aspose.Pdf.Facades.Bookmark
            {
                Title      = "Chapter 1",
                PageNumber = 1,
                Open       = true   // expanded by default
            };

            // Child bookmarks representing subsections
            Aspose.Pdf.Facades.Bookmark sub1 = new Aspose.Pdf.Facades.Bookmark
            {
                Title      = "Section 1.1",
                PageNumber = 2
            };
            Aspose.Pdf.Facades.Bookmark sub2 = new Aspose.Pdf.Facades.Bookmark
            {
                Title      = "Section 1.2",
                PageNumber = 3
            };

            // Assemble the child collection
            Aspose.Pdf.Facades.Bookmarks children = new Aspose.Pdf.Facades.Bookmarks();
            children.Add(sub1);
            children.Add(sub2);

            // Attach children to the parent bookmark
            parent.ChildItems = children;

            // Add the hierarchical bookmark to the document
            editor.CreateBookmarks(parent);

            // Save the updated PDF
            editor.Save(outputPath);
            editor.Close();
        }

        Console.WriteLine($"PDF saved with sub‑bookmarks: {outputPath}");
    }
}