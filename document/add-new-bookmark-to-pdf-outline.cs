using System;
using System.IO;
using System.Drawing; // needed for bookmark color
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // for destination types

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_heading.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the existing PDF
        using (Document doc = new Document(inputPath))
        {
            // Create a new outline (bookmark) item.
            // The constructor requires the root OutlineCollection (doc.Outlines).
            OutlineItemCollection newBookmark = new OutlineItemCollection(doc.Outlines)
            {
                Title = "New Section",                     // bookmark title
                Bold  = true,                               // optional styling
                Color = System.Drawing.Color.Blue           // bookmark color uses System.Drawing.Color
            };

            // Set the destination of the bookmark to the first page (page 1) at default zoom.
            // XYZExplicitDestination(page, left, top, zoom)
            Page targetPage = doc.Pages[1]; // 1‑based indexing
            newBookmark.Destination = new XYZExplicitDestination(targetPage, 0, 0, 1);

            // Insert the new bookmark at the end of the outline hierarchy.
            // You can also use Insert(index, ...) to place it at a specific position.
            doc.Outlines.Add(newBookmark);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with new outline entry: '{outputPath}'.");
    }
}
