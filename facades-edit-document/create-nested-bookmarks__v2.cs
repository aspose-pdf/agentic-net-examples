using System;
using System.IO;
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

        using (Document doc = new Document(inputPath))
        {
            PdfBookmarkEditor editor = new PdfBookmarkEditor(doc);

            // Sub‑subsection bookmark (level 3)
            Bookmark subSub = new Bookmark();
            subSub.Title = "Sub‑subsection 1.1.1";
            subSub.PageNumber = 3;

            // Subsection bookmark (level 2) with its child
            Bookmark sub = new Bookmark();
            sub.Title = "Subsection 1.1";
            sub.PageNumber = 2;
            Bookmarks subChildren = new Bookmarks();
            subChildren.Add(subSub);
            sub.ChildItems = subChildren;

            // Section bookmark (level 1) with its child
            Bookmark section = new Bookmark();
            section.Title = "Section 1";
            section.PageNumber = 1;
            Bookmarks sectionChildren = new Bookmarks();
            sectionChildren.Add(sub);
            section.ChildItems = sectionChildren;

            // Add the top‑level bookmark hierarchy to the PDF
            editor.CreateBookmarks(section);
            editor.Save(outputPath);
            editor.Close();
        }

        Console.WriteLine($"Nested bookmarks saved to '{outputPath}'.");
    }
}