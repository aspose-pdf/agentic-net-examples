using System;
using System.IO;
using Aspose.Pdf.Facades;   // PdfBookmarkEditor, Bookmark, Bookmarks

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_bookmarks.pdf";

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize the bookmark editor and bind the PDF
        PdfBookmarkEditor editor = new PdfBookmarkEditor();
        editor.BindPdf(inputPath);

        // -------------------------------------------------
        // Build a three‑level bookmark hierarchy:
        // Section (level 1)
        //   ├─ Subsection (level 2)
        //   │     ├─ Sub‑Subsection (level 3)
        //   │     └─ Sub‑Subsection (level 3)
        //   └─ Subsection (level 2)
        // -------------------------------------------------

        // Level‑3 bookmarks (sub‑subsections)
        Bookmark subSub1 = new Bookmark
        {
            Title      = "Sub‑Subsection 1.1.1",
            PageNumber = 1
        };
        Bookmark subSub2 = new Bookmark
        {
            Title      = "Sub‑Subsection 1.1.2",
            PageNumber = 2
        };

        // Collect level‑3 bookmarks
        Bookmarks subSubList = new Bookmarks();
        subSubList.Add(subSub1);
        subSubList.Add(subSub2);

        // Level‑2 bookmark (subsection) that contains level‑3 children
        Bookmark subsection1 = new Bookmark
        {
            Title      = "Subsection 1.1",
            PageNumber = 1,
            ChildItems = subSubList
        };

        // Another level‑2 bookmark without deeper children (optional)
        Bookmark subsection2 = new Bookmark
        {
            Title      = "Subsection 1.2",
            PageNumber = 3
        };

        // Collect level‑2 bookmarks
        Bookmarks subsectionList = new Bookmarks();
        subsectionList.Add(subsection1);
        subsectionList.Add(subsection2);

        // Level‑1 bookmark (section) that contains level‑2 children
        Bookmark section = new Bookmark
        {
            Title      = "Section 1",
            PageNumber = 1,
            ChildItems = subsectionList
        };

        // Add the top‑level bookmark (the whole hierarchy) to the PDF
        editor.CreateBookmarks(section);

        // Save the updated PDF with the nested bookmarks
        editor.Save(outputPath);
        editor.Close();

        Console.WriteLine($"Nested bookmarks created and saved to '{outputPath}'.");
    }
}