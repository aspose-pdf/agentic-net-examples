using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class InsertPageNumbersAfterToc
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // ------------------------------------------------------------
            // Detect the bookmark (outline) that represents the Table of Contents.
            // The outline title is searched case‑insensitively for the phrase
            // "Table of Contents". If found, its destination page is used.
            // ------------------------------------------------------------
            int tocPageNumber = 0; // 0 means not found; fallback to first page

            foreach (OutlineItemCollection outline in doc.Outlines)
            {
                if (outline.Title != null &&
                    outline.Title.IndexOf("Table of Contents", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    // Try to obtain the target page from the Action (GoToAction)
                    if (outline.Action is GoToAction goTo && goTo.Destination != null)
                    {
                        var pageProp = goTo.Destination.GetType().GetProperty("Page");
                        if (pageProp != null)
                        {
                            Page targetPage = pageProp.GetValue(goTo.Destination) as Page;
                            if (targetPage != null)
                            {
                                tocPageNumber = targetPage.Number;
                                break;
                            }
                        }
                    }

                    // If the Action did not give a page, try the Destination directly
                    if (outline.Destination != null)
                    {
                        var pageProp = outline.Destination.GetType().GetProperty("Page");
                        if (pageProp != null)
                        {
                            Page targetPage = pageProp.GetValue(outline.Destination) as Page;
                            if (targetPage != null)
                            {
                                tocPageNumber = targetPage.Number;
                                break;
                            }
                        }
                    }
                }
            }

            // If the TOC bookmark was not found, assume it starts on page 1.
            if (tocPageNumber == 0)
                tocPageNumber = 1;

            // ------------------------------------------------------------
            // Insert page numbers on all pages that follow the TOC.
            // Page numbers start at 1 for the first page after the TOC.
            // ------------------------------------------------------------
            int firstNumberedPage = tocPageNumber + 1;

            for (int i = firstNumberedPage; i <= doc.Pages.Count; i++)
            {
                // Calculate the logical page number (1‑based after TOC)
                int logicalNumber = i - tocPageNumber;

                PageNumberStamp stamp = new PageNumberStamp
                {
                    StartingNumber = logicalNumber,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Bottom,
                    BottomMargin = 20 // points from the bottom edge
                };

                doc.Pages[i].AddStamp(stamp);
            }

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Page numbers inserted. Output saved to '{outputPath}'.");
    }
}
