using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // for GoToAction (inherits from Action)

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

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Create a new outline (bookmark) item.
            // The constructor requires the parent OutlineCollection (doc.Outlines).
            OutlineItemCollection newOutline = new OutlineItemCollection(doc.Outlines)
            {
                // Set the visible title of the bookmark
                Title = "New Chapter",

                // Define the destination – here we navigate to page 2 (1‑based index)
                // GoToAction is a concrete Action that points to a specific page.
                Action = new GoToAction(doc.Pages[2])
            };

            // Insert the new outline at the end of the existing outline collection.
            // You could also use Insert(index, ...) to place it at a specific position.
            doc.Outlines.Add(newOutline);

            // Save the updated PDF. The outline is automatically reflected in the bookmarks pane.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with new outline: {outputPath}");
    }
}