using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "sample.pdf";
        const string outputPdf = "sample_with_bookmark.pdf";
        const string hierarchyPath = "hierarchy_abstract.txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Verify that page 2 exists (Aspose.Pdf uses 1‑based indexing)
            if (doc.Pages.Count < 2)
            {
                Console.Error.WriteLine("The document has fewer than 2 pages.");
                return;
            }

            // Create a destination that points to the top of page 2.
            // XYZExplicitDestination ctor requires page, x, y, zoom.
            // y coordinate is measured from the bottom, so we use the page height.
            var page2 = doc.Pages[2];
            var destination = new XYZExplicitDestination(page2, 0, page2.Rect.Height, 1);

            // Create a top‑level bookmark (OutlineItemCollection) and assign the destination
            var bookmark = new OutlineItemCollection(doc.Outlines)
            {
                Title = "Abstract",
                Destination = destination,
                Open = true // Expanded when the PDF is opened
            };

            // Add the bookmark to the document's root outline collection
            doc.Outlines.Add(bookmark);

            // Save the modified PDF (optional, can be omitted if not needed)
            doc.Save(outputPdf);

            // Export the entire bookmark hierarchy to a text file
            using (var writer = new StreamWriter(hierarchyPath))
            {
                foreach (OutlineItemCollection topItem in doc.Outlines)
                {
                    WriteOutline(topItem, writer, 0);
                }
            }
        }

        Console.WriteLine("Bookmark added and hierarchy exported successfully.");
    }

    // Recursive helper to write outline items with indentation representing hierarchy depth
    private static void WriteOutline(OutlineItemCollection item, StreamWriter writer, int level)
    {
        string indent = new string(' ', level * 2);
        writer.WriteLine($"{indent}{item.Title}");
        foreach (OutlineItemCollection child in item)
        {
            WriteOutline(child, writer, level + 1);
        }
    }
}
