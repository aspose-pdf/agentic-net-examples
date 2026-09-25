using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // Required for Font class

class FontComparison
{
    static void Main()
    {
        const string pdfPathA = "documentA.pdf";
        const string pdfPathB = "documentB.pdf";

        if (!File.Exists(pdfPathA) || !File.Exists(pdfPathB))
        {
            Console.Error.WriteLine("One or both PDF files not found.");
            return;
        }

        // Load both PDFs inside using blocks for deterministic disposal
        using (Document docA = new Document(pdfPathA))
        using (Document docB = new Document(pdfPathB))
        {
            // Compare page counts first
            if (docA.Pages.Count != docB.Pages.Count)
            {
                Console.WriteLine($"Page count differs: A={docA.Pages.Count}, B={docB.Pages.Count}");
            }

            int maxPages = Math.Max(docA.Pages.Count, docB.Pages.Count);

            // Iterate using 1‑based indexing (Aspose.Pdf uses 1‑based page indexes)
            for (int pageIndex = 1; pageIndex <= maxPages; pageIndex++)
            {
                bool hasPageA = pageIndex <= docA.Pages.Count;
                bool hasPageB = pageIndex <= docB.Pages.Count;

                if (!hasPageA || !hasPageB)
                {
                    Console.WriteLine($"Page {pageIndex}: Exists only in {(hasPageA ? "A" : "B")}");
                    continue;
                }

                // Build dictionaries of fonts for each page: key = font name, value = IsEmbedded flag
                var fontsA = BuildFontDictionary(docA.Pages[pageIndex]);
                var fontsB = BuildFontDictionary(docB.Pages[pageIndex]);

                // Detect fonts present only in A
                foreach (var kvp in fontsA)
                {
                    if (!fontsB.ContainsKey(kvp.Key))
                    {
                        Console.WriteLine($"Page {pageIndex}: Font '{kvp.Key}' present only in document A.");
                    }
                    else if (fontsB[kvp.Key] != kvp.Value)
                    {
                        Console.WriteLine($"Page {pageIndex}: Font '{kvp.Key}' embedding differs (A: {(kvp.Value ? "Embedded" : "Not Embedded")}, B: {(fontsB[kvp.Key] ? "Embedded" : "Not Embedded")}).");
                    }
                }

                // Detect fonts present only in B
                foreach (var kvp in fontsB)
                {
                    if (!fontsA.ContainsKey(kvp.Key))
                    {
                        Console.WriteLine($"Page {pageIndex}: Font '{kvp.Key}' present only in document B.");
                    }
                }
            }
        }
    }

    // Helper: extracts font information from a page into a dictionary
    private static Dictionary<string, bool> BuildFontDictionary(Page page)
    {
        var dict = new Dictionary<string, bool>(StringComparer.OrdinalIgnoreCase);
        // Font collection may be empty; iterate safely
        foreach (Font font in page.Resources.Fonts)
        {
            // Use FontName as identifier; store embedding status
            string name = font.FontName ?? "UnnamedFont";
            bool isEmbedded = font.IsEmbedded;
            // If the same font appears multiple times, keep the first embedding status (they should be consistent)
            if (!dict.ContainsKey(name))
            {
                dict[name] = isEmbedded;
            }
        }
        return dict;
    }
}