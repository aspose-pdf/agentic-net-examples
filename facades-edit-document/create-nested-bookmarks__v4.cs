using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPdf = "output.pdf";

        // Create a simple PDF with three pages
        using (Document doc = new Document())
        {
            // Page 1
            Page page1 = doc.Pages.Add();
            TextFragment tf1 = new TextFragment("Content of Page 1");
            page1.Paragraphs.Add(tf1);

            // Page 2
            Page page2 = doc.Pages.Add();
            TextFragment tf2 = new TextFragment("Content of Page 2");
            page2.Paragraphs.Add(tf2);

            // Page 3
            Page page3 = doc.Pages.Add();
            TextFragment tf3 = new TextFragment("Content of Page 3");
            page3.Paragraphs.Add(tf3);

            // Create nested bookmarks (three levels deep)
            PdfBookmarkEditor editor = new PdfBookmarkEditor();
            editor.BindPdf(doc);

            // Top‑level bookmark (Chapter)
            Bookmark chapter = new Bookmark
            {
                Title = "Chapter 1",
                PageNumber = 1,
                Open = true
            };

            // Second‑level bookmark (Section)
            Bookmark section = new Bookmark
            {
                Title = "Section 1.1",
                PageNumber = 2,
                Open = true
            };

            // Third‑level bookmark (Sub‑section)
            Bookmark subsection = new Bookmark
            {
                Title = "Sub‑section 1.1.1",
                PageNumber = 3,
                Open = true
            };

            // Build hierarchy
            Bookmarks subSectionList = new Bookmarks();
            subSectionList.Add(subsection);
            section.ChildItems = subSectionList;

            Bookmarks sectionList = new Bookmarks();
            sectionList.Add(section);
            chapter.ChildItems = sectionList;

            // Add the top‑level bookmark (the hierarchy is attached to it)
            editor.CreateBookmarks(chapter);

            // Save the PDF – guard against missing GDI+ (libgdiplus) on non‑Windows platforms
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPdf);
            }
            else
            {
                try
                {
                    doc.Save(outputPdf);
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. PDF was not saved.");
                }
            }
        }

        Console.WriteLine("PDF generation completed.");
    }

    // Helper that walks the inner‑exception chain looking for a DllNotFoundException
    private static bool ContainsDllNotFound(Exception? ex)
    {
        while (ex != null)
        {
            if (ex is DllNotFoundException)
                return true;
            ex = ex.InnerException;
        }
        return false;
    }
}
