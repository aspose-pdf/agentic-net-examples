using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_collapsed.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document doc = new Document(inputPath);

        // Collapse (close) specific bookmarks by title.
        CollapseBookmark(doc.Outlines, "Chapter 1");
        CollapseBookmark(doc.Outlines, "Appendix");

        // Save the updated PDF with the modified bookmark states
        doc.Save(outputPath);

        Console.WriteLine($"Bookmarks collapsed and saved to '{outputPath}'.");
    }

    // Recursively searches outline items and sets their open state to false when the title matches.
    private static void CollapseBookmark(IEnumerable<OutlineItemCollection> outlines, string title)
    {
        foreach (var item in outlines)
        {
            if (item.Title.Equals(title, StringComparison.OrdinalIgnoreCase))
            {
                // "Open" property controls the expanded/collapsed state. false = collapsed.
                item.Open = false;
            }

            // Recurse into child outlines, if any
            if (item.Count > 0)
            {
                CollapseBookmark(item, title);
            }
        }
    }
}
