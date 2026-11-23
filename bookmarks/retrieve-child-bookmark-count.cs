using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "sample.pdf";
        const string outputPath = "childcount.txt";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(pdfPath))
        {
            // The top‑level bookmarks are in doc.Outlines (type OutlineCollection)
            // Each bookmark node is an OutlineItemCollection
            OutlineItemCollection chapterOne = null;
            foreach (OutlineItemCollection item in doc.Outlines)
            {
                if (string.Equals(item.Title, "Chapter One", StringComparison.OrdinalIgnoreCase))
                {
                    chapterOne = item;
                    break;
                }
            }

            int childCount = chapterOne?.Count ?? 0; // Count gives the number of direct child bookmarks

            // Write the count to a text file
            File.WriteAllText(outputPath, childCount.ToString());
            Console.WriteLine($"Child bookmark count under \"Chapter One\": {childCount}");
        }
    }
}
