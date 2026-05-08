using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "portfolio.pdf";      // source PDF with outline items
        const string outputPdf = "reordered.pdf";      // result PDF

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Desired order of outline (bookmark) titles.
        // Adjust this array to reflect the sequence you need.
        string[] desiredOrder = new[]
        {
            "Executive Summary",
            "Financial Overview",
            "Market Analysis",
            "Risk Assessment",
            "Appendix"
        };

        using (Document doc = new Document(inputPdf))
        {
            // Get the document outline (bookmarks) collection.
            OutlineCollection outlines = doc.Outlines;

            // Preserve existing items in a list for later lookup.
            List<OutlineItemCollection> existingItems = new List<OutlineItemCollection>();
            foreach (OutlineItemCollection item in outlines)
                existingItems.Add(item);

            // Clear all current outline items.
            outlines.Clear();

            // Re‑add items following the desired order.
            foreach (string title in desiredOrder)
            {
                // Find the original item that matches the title.
                OutlineItemCollection original = existingItems
                    .FirstOrDefault(i => string.Equals(i.Title, title, StringComparison.Ordinal));

                if (original == null)
                {
                    // Title not found – skip or handle as needed.
                    Console.WriteLine($"Warning: outline item \"{title}\" not found in source PDF.");
                    continue;
                }

                // Create a new outline item attached to the current OutlineCollection.
                OutlineItemCollection newItem = new OutlineItemCollection(outlines)
                {
                    Title       = original.Title,
                    Destination = original.Destination,
                    Action      = original.Action,
                    Bold        = original.Bold,
                    Italic      = original.Italic,
                    Color       = original.Color,
                    Open        = original.Open
                };

                // Add the newly created item to the outline.
                outlines.Add(newItem);
            }

            // Save the reordered PDF.
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Reordered PDF saved to \"{outputPdf}\".");
    }
}