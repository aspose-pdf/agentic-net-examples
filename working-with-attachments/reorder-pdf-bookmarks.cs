using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // for GoToAction

class Program
{
    static void Main()
    {
        const string inputPath  = "portfolio.pdf";
        const string outputPath = "reordered_portfolio.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Example: desired new order of pages (1‑based indexing)
            int[] newOrder = { 3, 1, 2 }; // reorder bookmarks to page 3, then 1, then 2

            // Remove all existing outline (bookmark) items
            doc.Outlines.Delete();

            // Re‑add outline items in the specified order
            foreach (int pageNumber in newOrder)
            {
                // Create a new outline entry
                OutlineItemCollection outlineItem = new OutlineItemCollection(doc.Outlines);
                outlineItem.Title = $"Page {pageNumber}";

                // Set the action to navigate to the target page
                outlineItem.Action = new GoToAction(doc.Pages[pageNumber]);

                // Add the new outline entry to the document's outline collection
                doc.Outlines.Add(outlineItem);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Reordered PDF saved to '{outputPath}'.");
    }
}