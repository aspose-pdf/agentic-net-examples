using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;
using Aspose.Pdf.Text; // needed for TextFragment

class Program
{
    static void Main()
    {
        const string outputPath = "tagged_toc.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // ----- Tagged content setup -----
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("en-US");
            tagged.SetTitle("Document with TOC");

            // Root element of the logical structure
            StructureElement root = tagged.RootElement;

            // ----- Table of Contents page -----
            Page tocPage = doc.Pages.Add();
            tocPage.TocInfo = new TocInfo
            {
                Title = new TextFragment("Table of Contents"), // Title must be a TextFragment
                IsShowPageNumbers = true,
                CopyToOutlines = true
            };

            // Create TOC structure element and add it to the root
            TOCElement toc = tagged.CreateTOCElement();
            toc.Title = "Table of Contents"; // visible title for the TOC element
            root.AppendChild(toc);

            // ----- Add headings and corresponding TOC entries -----
            for (int i = 1; i <= 3; i++)
            {
                // Add a new page for the chapter
                Page chapterPage = doc.Pages.Add();

                // Add visible heading text to the page (optional but typical)
                TextFragment headingFragment = new TextFragment($"Chapter {i}");
                chapterPage.Paragraphs.Add(headingFragment);

                // Create a heading (HeaderElement level 1)
                HeaderElement heading = tagged.CreateHeaderElement(1);
                heading.SetText($"Chapter {i}");
                heading.Language = "en-US";
                root.AppendChild(heading);

                // Add a paragraph under the heading (visible text)
                TextFragment paraFragment = new TextFragment($"Content of chapter {i} goes here. This is sample text for demonstration purposes.");
                chapterPage.Paragraphs.Add(paraFragment);

                ParagraphElement para = tagged.CreateParagraphElement();
                para.SetText($"Content of chapter {i} goes here. This is sample text for demonstration purposes.");
                heading.AppendChild(para);

                // Create a TOC item (TOCIElement) and attach it to the TOC
                TOCIElement toci = tagged.CreateTOCIElement();
                toci.ActualText = $"Chapter {i}"; // Use ActualText instead of SetText
                toc.AppendChild(toci);
            }

            // ----- Save the tagged PDF (guarded against missing GDI+ on non‑Windows platforms) -----
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPath);
                Console.WriteLine($"Tagged PDF with TOC saved to '{outputPath}'.");
            }
            else
            {
                try
                {
                    doc.Save(outputPath);
                    Console.WriteLine($"Tagged PDF with TOC saved to '{outputPath}' (non‑Windows platform)." );
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. PDF was not saved.");
                }
            }
        }
    }

    // Helper that walks the inner‑exception chain looking for a DllNotFoundException (e.g., missing libgdiplus)
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
