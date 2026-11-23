using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // for XYZExplicitDestination

class Program
{
    static void Main()
    {
        const string inputPath = "sample.pdf";
        const string outputPath = "updateddest.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least 2 bookmarks and 10 pages
            if (doc.Outlines.Count < 2)
            {
                Console.Error.WriteLine("The PDF does not contain a bookmark at index 2.");
                return;
            }
            if (doc.Pages.Count < 10)
            {
                Console.Error.WriteLine("The PDF does not contain page 10.");
                return;
            }

            // Access the bookmark at index 2 (OutlineCollection is 1‑based)
            OutlineItemCollection bookmark = doc.Outlines[2];

            // Set its destination to page 10. Use XYZExplicitDestination (page, left, top, zoom).
            // Left, Top set to 0 (origin) and zoom to 1 (100%). Adjust as needed.
            bookmark.Destination = new XYZExplicitDestination(doc.Pages[10], 0, 0, 1);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Bookmark updated and saved to '{outputPath}'.");
    }
}
