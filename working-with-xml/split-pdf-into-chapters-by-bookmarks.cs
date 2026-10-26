using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class SplitPdfByChapter
{
    static void Main()
    {
        const string inputPath = "large_from_xml.pdf";
        const string outputDir = "Chapters";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        try
        {
            // Load the source PDF
            using (Document srcDoc = new Document(inputPath))
            {
                // Get the top‑level outline items (assumed to represent chapters)
                OutlineCollection outlines = srcDoc.Outlines;

                if (outlines == null || outlines.Count == 0)
                {
                    Console.WriteLine("No outline items (chapters) found in the document.");
                    return;
                }

                // Build arrays of chapter start pages and titles
                int[] chapterStartPages = new int[outlines.Count];
                string[] chapterTitles = new string[outlines.Count];

                for (int i = 0; i < outlines.Count; i++)
                {
                    // Each item in the collection is an OutlineItemCollection (a bookmark node)
                    OutlineItemCollection item = outlines[i];
                    chapterTitles[i] = SanitizeFileName(item.Title ?? $"Chapter_{i + 1}");

                    // Try to extract the page number from the destination (explicit destinations)
                    int pageNum = 1; // default fallback
                    if (item.Destination != null)
                    {
                        var dest = item.Destination;
                        // Most explicit destinations expose a "Page" property
                        var pageProp = dest.GetType().GetProperty("Page");
                        if (pageProp != null)
                        {
                            var pageObj = pageProp.GetValue(dest) as Page;
                            if (pageObj != null)
                                pageNum = pageObj.Number;
                        }
                    }
                    chapterStartPages[i] = pageNum;
                }

                // Iterate chapters and extract page ranges
                for (int i = 0; i < outlines.Count; i++)
                {
                    int startPage = chapterStartPages[i];
                    int endPage = (i + 1 < outlines.Count) ? chapterStartPages[i + 1] - 1 : srcDoc.Pages.Count;

                    // Guard against invalid ranges
                    if (startPage < 1) startPage = 1;
                    if (endPage > srcDoc.Pages.Count) endPage = srcDoc.Pages.Count;
                    if (endPage < startPage) continue;

                    using (Document chapterDoc = new Document())
                    {
                        // Copy pages from source to the new document
                        for (int p = startPage; p <= endPage; p++)
                        {
                            chapterDoc.Pages.Add(srcDoc.Pages[p]);
                        }

                        string outPath = Path.Combine(outputDir, $"{chapterTitles[i]}.pdf");
                        chapterDoc.Save(outPath);
                        Console.WriteLine($"Saved chapter '{chapterTitles[i]}' to '{outPath}'.");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    // Helper to make a safe file name
    static string SanitizeFileName(string name)
    {
        foreach (char c in Path.GetInvalidFileNameChars())
        {
            name = name.Replace(c, '_');
        }
        return name.Trim();
    }
}
