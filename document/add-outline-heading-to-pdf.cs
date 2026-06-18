using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // GoToAction resides in this namespace

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

        // Open the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Access the document's outline (bookmarks) collection
            OutlineCollection outlines = doc.Outlines;

            // Create a new outline item (bookmark) that will act as a heading
            OutlineItemCollection newHeading = new OutlineItemCollection(outlines)
            {
                Title = "New Chapter Heading",                     // Text displayed in the bookmarks pane
                Bold  = true,                                      // Optional styling
                Italic = false,
                Color = System.Drawing.Color.DarkBlue,            // Title color (System.Drawing.Color is required here)
                // Define the action that jumps to a specific page when the bookmark is clicked.
                // Here we navigate to page 2 (1‑based indexing). Adjust as needed.
                Action = new GoToAction(doc.Pages[2])
            };

            // Add the new heading to the outline collection.
            // OutlineCollection does not provide an Insert method; use Add to append.
            // If a specific order is required, add then reorder the collection as needed.
            outlines.Add(newHeading);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with new outline heading saved to '{outputPath}'.");
    }
}
