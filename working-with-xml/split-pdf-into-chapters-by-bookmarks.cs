using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // for GoToAction and explicit destination types

class Program
{
    static void Main()
    {
        const string inputPdfPath = "large_document.pdf";
        const string outputFolder = "Chapters";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the source PDF (generated from XML)
        using (Document sourceDoc = new Document(inputPdfPath))
        {
            // Try to use bookmarks (Outlines) as chapter markers.
            // If no outlines exist, fall back to one‑page‑per‑chapter split.
            if (sourceDoc.Outlines.Count > 0)
            {
                // Iterate top‑level outline items (assumed to represent chapters)
                for (int i = 0; i < sourceDoc.Outlines.Count; i++)
                {
                    OutlineItemCollection current = sourceDoc.Outlines[i];

                    // Resolve the start page of the current chapter
                    int startPage = ResolvePageNumber(current);

                    // Resolve the end page (page before the next chapter starts, or last page)
                    int endPage;
                    if (i + 1 < sourceDoc.Outlines.Count)
                    {
                        OutlineItemCollection next = sourceDoc.Outlines[i + 1];
                        endPage = ResolvePageNumber(next) - 1;
                    }
                    else
                    {
                        endPage = sourceDoc.Pages.Count;
                    }

                    // Sanitize chapter title for file name
                    string rawTitle = string.IsNullOrEmpty(current.Title) ? $"Chapter_{i + 1}" : current.Title;
                    string safeTitle = MakeFileNameSafe(rawTitle);

                    string outputPath = Path.Combine(outputFolder, $"{safeTitle}.pdf");

                    // Create a new PDF containing the page range for this chapter
                    using (Document chapterDoc = new Document())
                    {
                        for (int p = startPage; p <= endPage; p++)
                        {
                            // Pages collection is 1‑based
                            chapterDoc.Pages.Add(sourceDoc.Pages[p]);
                        }

                        chapterDoc.Save(outputPath);
                    }

                    Console.WriteLine($"Saved chapter '{rawTitle}' → {outputPath}");
                }
            }
            else
            {
                // No bookmarks – split each page into its own PDF file
                for (int pageNum = 1; pageNum <= sourceDoc.Pages.Count; pageNum++)
                {
                    string outputPath = Path.Combine(outputFolder, $"Page_{pageNum}.pdf");

                    using (Document singlePageDoc = new Document())
                    {
                        singlePageDoc.Pages.Add(sourceDoc.Pages[pageNum]);
                        singlePageDoc.Save(outputPath);
                    }

                    Console.WriteLine($"Saved page {pageNum} → {outputPath}");
                }
            }
        }
    }

    // Helper: replace characters illegal in Windows file names
    static string MakeFileNameSafe(string name)
    {
        foreach (char c in Path.GetInvalidFileNameChars())
            name = name.Replace(c, '_');
        return name;
    }

    // Helper: extract the page number from an outline entry.
    // Works for explicit destinations (XYZ, Fit, FitH, etc.) and for GoToAction destinations.
    static int ResolvePageNumber(OutlineItemCollection outline)
    {
        // 1. Direct Destination (most common when bookmarks were created programmatically)
        if (outline.Destination != null)
        {
            // Cast to the concrete explicit‑destination types that expose PageNumber
            if (outline.Destination is XYZExplicitDestination xyz) return xyz.PageNumber;
            if (outline.Destination is FitExplicitDestination fit) return fit.PageNumber;
            if (outline.Destination is FitHExplicitDestination fith) return fith.PageNumber;
            if (outline.Destination is FitVExplicitDestination fitv) return fitv.PageNumber;
            if (outline.Destination is FitRExplicitDestination fitr) return fitr.PageNumber;
            // NamedDestination does not expose a page number directly – fall back to 1
        }

        // 2. Action‑based destination (e.g., GoToAction)
        if (outline.Action is GoToAction goTo && goTo.Destination != null)
        {
            if (goTo.Destination is XYZExplicitDestination xyz) return xyz.PageNumber;
            if (goTo.Destination is FitExplicitDestination fit) return fit.PageNumber;
            if (goTo.Destination is FitHExplicitDestination fith) return fith.PageNumber;
            if (goTo.Destination is FitVExplicitDestination fitv) return fitv.PageNumber;
            if (goTo.Destination is FitRExplicitDestination fitr) return fitr.PageNumber;
        }

        // Fallback – if we cannot determine, assume first page
        return 1;
    }
}
