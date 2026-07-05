using System;
using System.IO;
using Aspose.Pdf.Facades;          // PdfBookmarkEditor, Bookmark, Bookmarks
using Aspose.Pdf;                 // Document (if needed for binding via Document)

// Example: add three‑level nested bookmarks (Section → Subsection → Sub‑Subsection)
class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // source PDF
        const string outputPdf = "output_with_bookmarks.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Bind the existing PDF to the bookmark editor
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            editor.BindPdf(inputPdf);

            // ----- Build sub‑sub‑section bookmarks -----
            Bookmark subSub1 = new Bookmark
            {
                Title      = "Sub‑Subsection 1",
                PageNumber = 3               // target page for this bookmark
            };

            Bookmark subSub2 = new Bookmark
            {
                Title      = "Sub‑Subsection 2",
                PageNumber = 4
            };

            // Collection of sub‑sub‑sections
            Bookmarks subSubCollection = new Bookmarks();
            subSubCollection.Add(subSub1);
            subSubCollection.Add(subSub2);

            // ----- Build subsection bookmark and attach its children -----
            Bookmark subSection = new Bookmark
            {
                Title      = "Subsection",
                PageNumber = 2,
                ChildItems = subSubCollection   // nested level 2
            };

            // Collection of subsections (could hold multiple)
            Bookmarks subSectionCollection = new Bookmarks();
            subSectionCollection.Add(subSection);

            // ----- Build top‑level section bookmark and attach its children -----
            Bookmark section = new Bookmark
            {
                Title      = "Section",
                PageNumber = 1,
                ChildItems = subSectionCollection // nested level 1
            };

            // Create the hierarchical bookmark structure in the PDF
            editor.CreateBookmarks(section);

            // Save the modified PDF
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Nested bookmarks added. Output saved to '{outputPdf}'.");
    }
}