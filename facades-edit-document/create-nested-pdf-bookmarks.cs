using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using System.Drawing;

class Program
{
    static void Main()
    {
        const string basePdfPath = "base.pdf";
        const string outputPdfPath = "bookmarked.pdf";

        // 1. Create a simple PDF with three pages (one for each bookmark level)
        using (Document doc = new Document())
        {
            for (int i = 0; i < 3; i++)
                doc.Pages.Add();

            doc.Save(basePdfPath);
        }

        // 2. Create nested bookmarks using PdfBookmarkEditor (Facade API)
        var bookmarkEditor = new PdfBookmarkEditor();
        bookmarkEditor.BindPdf(basePdfPath);

        // Top‑level bookmark (Section)
        var section = new Bookmark
        {
            Title = "Section 1",
            PageNumber = 1,
            Action = "GoTo"
        };

        // Subsection bookmark
        var subsection = new Bookmark
        {
            Title = "Subsection 1.1",
            PageNumber = 2,
            Action = "GoTo"
        };

        // Sub‑subsection bookmark
        var subsubsection = new Bookmark
        {
            Title = "Sub‑subsection 1.1.1",
            PageNumber = 3,
            Action = "GoTo"
        };

        // Build the hierarchy: Section -> Subsection -> Sub‑subsection
        subsection.ChildItems.Add(subsubsection);
        section.ChildItems.Add(subsection);

        // Add the hierarchy to the document
        bookmarkEditor.CreateBookmarks(section);

        // Save the PDF with the new bookmark structure
        bookmarkEditor.Save(outputPdfPath);
        bookmarkEditor.Close();

        // Optional cleanup of the intermediate file
        if (File.Exists(basePdfPath))
            File.Delete(basePdfPath);

        Console.WriteLine($"PDF with nested bookmarks saved to '{outputPdfPath}'.");
    }
}
