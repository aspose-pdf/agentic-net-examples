using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "clickable_toc.pdf";
        const int sectionCount = 3;
        const float tocStartY = 750f;
        const float tocLineHeight = 30f;
        const float headingY = 750f;
        const float headingFontSize = 24f;
        const float tocEntryFontSize = 14f;
        const float leftMargin = 50f;
        const float linkRight = 300f;

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page that will hold the Table of Contents
            Page tocPage = doc.Pages.Add();

            // Store the page numbers of each section for linking
            List<int> sectionPageNumbers = new List<int>();

            // Create sections with headings
            for (int i = 1; i <= sectionCount; i++)
            {
                Page sectionPage = doc.Pages.Add();
                int pageNumber = doc.Pages.Count; // 1‑based index
                sectionPageNumbers.Add(pageNumber);

                // Add a heading to the section page
                TextFragment heading = new TextFragment($"Section {i}");
                heading.TextState.FontSize = headingFontSize;
                heading.Position = new Position(leftMargin, headingY);
                sectionPage.Paragraphs.Add(heading);
            }

            // Build the Table of Contents on the first page
            for (int i = 1; i <= sectionCount; i++)
            {
                float entryY = tocStartY - (i * tocLineHeight);

                // Add visible TOC entry text
                TextFragment tocEntry = new TextFragment($"{i}. Section {i}");
                tocEntry.TextState.FontSize = tocEntryFontSize;
                tocEntry.Position = new Position(leftMargin, entryY);
                tocPage.Paragraphs.Add(tocEntry);

                // Define a clickable rectangle around the TOC entry
                Aspose.Pdf.Rectangle linkRect = new Aspose.Pdf.Rectangle(
                    leftMargin,
                    entryY - 5f,
                    linkRight,
                    entryY + 5f);

                // Create a link annotation that jumps to the corresponding section page
                LinkAnnotation link = new LinkAnnotation(tocPage, linkRect);
                link.Action = new GoToAction(doc.Pages[sectionPageNumbers[i - 1]]);
                tocPage.Annotations.Add(link);
            }

            // Save the resulting PDF – guard against missing GDI+ on non‑Windows platforms
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPath);
                Console.WriteLine($"PDF saved to '{outputPath}'.");
            }
            else
            {
                try
                {
                    doc.Save(outputPath);
                    Console.WriteLine($"PDF saved to '{outputPath}' (non‑Windows platform).");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. PDF was not saved.");
                }
            }
        }
    }

    // Helper to detect a nested DllNotFoundException (e.g., missing libgdiplus)
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
