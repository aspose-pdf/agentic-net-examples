using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPath))
        {
            // Create a new top‑level bookmark (outline item).
            // The constructor takes the parent collection (doc.Outlines).
            OutlineItemCollection bookmark = new OutlineItemCollection(doc.Outlines)
            {
                // Title displayed in the bookmarks pane.
                Title = "Chapter 1 – Introduction",
                // Set Open to false so the bookmark starts collapsed.
                Open = false
            };

            // Define the destination for the bookmark – navigate to page 2.
            // Use GoToAction which is the correct way to set a page destination.
            bookmark.Action = new GoToAction(doc.Pages[2]);

            // Add the bookmark to the document's outline collection.
            doc.Outlines.Add(bookmark);

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with collapsed bookmark to '{outputPath}'.");
    }
}