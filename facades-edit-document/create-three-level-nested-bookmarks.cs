using System;
using System.IO;
using Aspose.Pdf.Facades;          // PdfBookmarkEditor, Bookmark, Bookmarks
using Aspose.Pdf;                 // Document (if needed for binding via Document)

// Example: create a PDF with three‑level nested bookmarks (section → subsection → sub‑subsection)
class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // existing PDF to which bookmarks will be added
        const string outputPdf = "output_with_bookmarks.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Initialize the bookmark editor and bind the source PDF
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            editor.BindPdf(inputPdf);

            // ----- Build sub‑sub‑section bookmarks (level 3) -----
            Bookmark subSub1 = new Bookmark
            {
                Title      = "Sub‑Subsection 1",
                PageNumber = 3
            };

            Bookmark subSub2 = new Bookmark
            {
                Title      = "Sub‑Subsection 2",
                PageNumber = 4
            };

            Bookmarks subSubBookmarks = new Bookmarks();
            subSubBookmarks.Add(subSub1);
            subSubBookmarks.Add(subSub2);

            // ----- Build subsection bookmarks (level 2) -----
            Bookmark subSection1 = new Bookmark
            {
                Title      = "Subsection 1",
                PageNumber = 2,
                ChildItems = subSubBookmarks   // attach level‑3 children
            };

            Bookmark subSection2 = new Bookmark
            {
                Title      = "Subsection 2",
                PageNumber = 5
                // No further children
            };

            Bookmarks subSectionBookmarks = new Bookmarks();
            subSectionBookmarks.Add(subSection1);
            subSectionBookmarks.Add(subSection2);

            // ----- Build top‑level section bookmark (level 1) -----
            Bookmark section = new Bookmark
            {
                Title      = "Section 1",
                PageNumber = 1,
                ChildItems = subSectionBookmarks   // attach level‑2 children
            };

            // Create the nested bookmark hierarchy in the PDF
            editor.CreateBookmarks(section);

            // Save the modified PDF
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Nested bookmarks added. Output saved to '{outputPdf}'.");
    }
}