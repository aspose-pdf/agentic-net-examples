using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf  = "large.pdf";          // PDF generated from XML
        const string outputDir = "Chapters";           // Folder for split PDFs

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Load the source PDF (lifecycle rule: wrap Document in using)
        using (Document srcDoc = new Document(inputPdf))
        {
            // Retrieve top‑level outline items (usually chapters)
            if (srcDoc.Outlines == null || srcDoc.Outlines.Count == 0)
            {
                Console.WriteLine("No outline (bookmark) information found – cannot split by chapters.");
                return;
            }

            // Collect start page numbers of each chapter
            List<int> chapterStartPages = new List<int>();
            foreach (OutlineItemCollection chapter in srcDoc.Outlines)
            {
                int pageNum = GetPageNumberFromOutline(chapter);
                if (pageNum > 0)
                    chapterStartPages.Add(pageNum);
            }

            // If no valid page numbers were found, abort
            if (chapterStartPages.Count == 0)
            {
                Console.WriteLine("Outline items do not contain page destinations.");
                return;
            }

            // Ensure the list is sorted (outline order may not be numeric)
            chapterStartPages.Sort();

            // Append a sentinel value (one past the last page) to simplify range calculation
            chapterStartPages.Add(srcDoc.Pages.Count + 1);

            // Split each chapter into its own PDF
            for (int i = 0; i < chapterStartPages.Count - 1; i++)
            {
                int startPage = chapterStartPages[i];                 // inclusive
                int endPage   = chapterStartPages[i + 1] - 1;         // inclusive
                string chapterFile = Path.Combine(outputDir, $"Chapter_{i + 1}.pdf");

                // Create a new document for the chapter (lifecycle rule)
                using (Document chapterDoc = new Document())
                {
                    // Add pages from startPage to endPage (page indexing is 1‑based)
                    for (int p = startPage; p <= endPage; p++)
                    {
                        chapterDoc.Pages.Add(srcDoc.Pages[p]);
                    }

                    // Save the chapter PDF (lifecycle rule)
                    chapterDoc.Save(chapterFile);
                }

                Console.WriteLine($"Saved chapter {i + 1}: {chapterFile} (pages {startPage}-{endPage})");
            }
        }
    }

    // Extracts the target page number from an outline (bookmark) item.
    // Handles both GoToAction and explicit destination subclasses.
    static int GetPageNumberFromOutline(OutlineItemCollection outline)
    {
        // Case 1: Action is a GoToAction (most common)
        if (outline.Action is GoToAction goTo)
        {
            // GoToAction exposes a Destination object that contains a Page reference.
            // The concrete Destination type varies, so we use reflection to obtain the Page.
            var dest = goTo.Destination;
            if (dest != null)
            {
                var pageProp = dest.GetType().GetProperty("Page");
                if (pageProp != null)
                {
                    var page = pageProp.GetValue(dest) as Page;
                    if (page != null)
                        return page.Number; // 1‑based page index
                }
            }
        }

        // Case 2: Destination is set directly on the outline (some PDFs use this pattern)
        if (outline.Destination != null)
        {
            var pageProp = outline.Destination.GetType().GetProperty("Page");
            if (pageProp != null)
            {
                var page = pageProp.GetValue(outline.Destination) as Page;
                return page?.Number ?? 0;
            }
        }

        // No recognizable page reference
        return 0;
    }
}
