using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Annotations; // needed for destination types

class Program
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

        try
        {
            using (Document doc = new Document(inputPath))
            {
                // Locate the bookmark titled "Table of Contents" (case‑insensitive)
                int tocStartPage = FindBookmarkPage(doc.Outlines, "Table of Contents");

                if (tocStartPage == -1)
                {
                    Console.WriteLine("Table of Contents bookmark not found. No page numbers added.");
                }
                else
                {
                    // Add page numbers starting after the TOC start page
                    for (int i = tocStartPage + 1; i <= doc.Pages.Count; i++)
                    {
                        Page page = doc.Pages[i];

                        TextFragment tf = new TextFragment(i.ToString());
                        tf.TextState.FontSize = 12;
                        tf.TextState.Font = FontRepository.FindFont("Arial");
                        tf.TextState.ForegroundColor = Aspose.Pdf.Color.Gray;
                        tf.HorizontalAlignment = HorizontalAlignment.Center;

                        // Position at bottom center of the page
                        double x = page.PageInfo.Width / 2;
                        double y = 20; // 20 points from the bottom edge
                        tf.Position = new Position(x, y);

                        page.Paragraphs.Add(tf);
                    }
                }

                doc.Save(outputPath);
            }

            Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    // Recursively searches an outline collection (root or child) for a title and returns its destination page number (1‑based) or -1.
    static int FindBookmarkPage(System.Collections.IEnumerable collection, string title)
    {
        foreach (OutlineItemCollection item in collection)
        {
            // Compare title (case‑insensitive)
            if (string.Equals(item.Title?.Trim(), title, StringComparison.OrdinalIgnoreCase))
            {
                // Try to obtain the page number from the Destination object
                int pageNumber = GetPageNumberFromDestination(item.Destination);
                if (pageNumber > 0)
                    return pageNumber;
            }

            // Recurse into child outlines (the item itself is enumerable of its children)
            int childResult = FindBookmarkPage(item, title);
            if (childResult != -1)
                return childResult;
        }
        return -1;
    }

    // Helper that extracts the page number from the various explicit destination types.
    static int GetPageNumberFromDestination(IAppointment destination)
    {
        if (destination == null)
            return -1;

        switch (destination)
        {
            case XYZExplicitDestination xyz:
                return xyz.PageNumber;
            case FitExplicitDestination fit:
                return fit.PageNumber;
            case FitHExplicitDestination fitH:
                return fitH.PageNumber;
            case FitVExplicitDestination fitV:
                return fitV.PageNumber;
            case FitRExplicitDestination fitR:
                return fitR.PageNumber;
            case FitBExplicitDestination fitB:
                return fitB.PageNumber;
            case NamedDestination _:
                // Named destinations do not expose a page number directly; return -1.
                return -1;
            default:
                return -1;
        }
    }
}
