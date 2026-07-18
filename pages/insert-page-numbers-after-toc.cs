using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;   // for OutlineItemCollection (core API)
using Aspose.Pdf.Text;          // for PageNumberStamp (inherits from TextStamp)

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_numbered.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF document
        using (Document doc = new Document(inputPath))
        {
            // ------------------------------------------------------------
            // 1. Detect the page that contains the Table of Contents (TOC)
            // ------------------------------------------------------------
            // The core API stores bookmarks in the Outlines collection.
            // We look for a bookmark whose title contains "Table of Contents".
            // If found, we try to obtain the destination page number.
            // If the bookmark structure is different, fallback to page 1.
            // ------------------------------------------------------------
            int tocPageNumber = 1; // default fallback

            if (doc.Outlines != null && doc.Outlines.Count > 0)
            {
                foreach (OutlineItemCollection outline in doc.Outlines)
                {
                    if (outline.Title != null &&
                        outline.Title.IndexOf("Table of Contents", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        // The Destination of a bookmark is usually a GoToAction.
                        // Its Destination property points to an explicit destination that
                        // holds a reference to the target page.
                        if (outline.Destination is GoToAction goTo && goTo.Destination != null)
                        {
                            // Many explicit destination types expose a Page property.
                            // Use reflection to obtain it safely without depending on a specific subclass.
                            var destPageProp = goTo.Destination.GetType().GetProperty("Page");
                            if (destPageProp != null)
                            {
                                var pageObj = destPageProp.GetValue(goTo.Destination) as Page;
                                if (pageObj != null)
                                {
                                    // Pages collection is 1‑based, so we can get the index directly.
                                    tocPageNumber = doc.Pages.IndexOf(pageObj);
                                    // IndexOf returns 0‑based, add 1 to match the collection indexing.
                                    tocPageNumber += 1;
                                }
                            }
                        }
                        break; // stop after the first matching bookmark
                    }
                }
            }

            // ------------------------------------------------------------
            // 2. Add page numbers to all pages after the TOC page.
            // ------------------------------------------------------------
            // PageNumberStamp replaces the '#' character with the actual page number.
            // Adding the same stamp to each page works because the stamp evaluates the
            // current page number at the time of stamping.
            // ------------------------------------------------------------
            for (int i = tocPageNumber + 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Create a stamp with the desired format.
                PageNumberStamp pageNumberStamp = new PageNumberStamp("Page #");

                // Optional: adjust appearance (font size, alignment, etc.)
                pageNumberStamp.HorizontalAlignment = HorizontalAlignment.Center;
                pageNumberStamp.VerticalAlignment   = VerticalAlignment.Bottom;
                pageNumberStamp.BottomMargin       = 20; // distance from bottom edge

                // Add the stamp to the current page.
                page.AddStamp(pageNumberStamp);
            }

            // ------------------------------------------------------------
            // 3. Save the modified document.
            // ------------------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"Page numbers added. Output saved to '{outputPath}'.");
    }
}