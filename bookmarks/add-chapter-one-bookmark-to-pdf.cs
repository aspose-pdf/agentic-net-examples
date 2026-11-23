using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // for XYZExplicitDestination

class Program
{
    static void Main()
    {
        const string inputPath = "sample.pdf";
        const string outputPath = "chapter1.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Verify the document has at least 5 pages (Aspose.Pdf uses 1‑based indexing)
            if (doc.Pages.Count < 5)
            {
                Console.Error.WriteLine("The document contains fewer than 5 pages.");
                return;
            }

            // Create a top‑level bookmark titled "Chapter One" that links to page 5
            OutlineItemCollection bookmark = new OutlineItemCollection(doc.Outlines);
            bookmark.Title = "Chapter One";
            // XYZExplicitDestination(page, left, top, zoom). 0 for left/top/zoom keeps defaults.
            bookmark.Destination = new XYZExplicitDestination(doc.Pages[5], 0, 0, 0);
            doc.Outlines.Add(bookmark);

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Bookmark added and saved to '{outputPath}'.");
    }
}
