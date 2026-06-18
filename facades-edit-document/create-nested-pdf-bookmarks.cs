using System;
using System.IO;
using Aspose.Pdf.Facades;   // Facade classes for bookmark manipulation
using Aspose.Pdf;           // Core PDF classes (if needed)

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Use PdfBookmarkEditor to bind the existing PDF and create nested bookmarks
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            // Bind the source PDF file
            editor.BindPdf(inputPath);

            // ----- Create the deepest bookmark (sub‑subsection) -----
            Bookmark subSubSection = new Bookmark
            {
                Title      = "Sub‑subsection 1.1.1",
                PageNumber = 3               // Destination page for this bookmark
            };

            // ----- Create the middle bookmark (subsection) and attach the child -----
            Bookmark subSection = new Bookmark
            {
                Title      = "Subsection 1.1",
                PageNumber = 2
            };
            // ChildItems expects a Bookmarks collection
            Bookmarks subSubList = new Bookmarks();
            subSubList.Add(subSubSection);
            subSection.ChildItems = subSubList;

            // ----- Create the top‑level bookmark (section) and attach the child -----
            Bookmark section = new Bookmark
            {
                Title      = "Section 1",
                PageNumber = 1
            };
            Bookmarks subSectionList = new Bookmarks();
            subSectionList.Add(subSection);
            section.ChildItems = subSectionList;

            // Add the hierarchical bookmark structure to the document
            editor.CreateBookmarks(section);

            // Save the modified PDF with the new bookmarks
            editor.Save(outputPath);
        }

        Console.WriteLine($"Nested bookmarks created and saved to '{outputPath}'.");
    }
}