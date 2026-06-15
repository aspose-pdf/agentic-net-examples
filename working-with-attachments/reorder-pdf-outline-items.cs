using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "portfolio.pdf";
        const string outputPath = "portfolio_reordered.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Access the document outline (bookmarks)
            OutlineCollection outlines = doc.Outlines;

            // Desired order of outline titles
            string[] desiredOrder = { "Projects", "Experience", "Education", "Contact" };

            // Store existing outline items in a dictionary keyed by title.
            // Use dynamic to avoid compile‑time dependency on the OutlineItem type.
            var existingItems = new Dictionary<string, dynamic>(StringComparer.OrdinalIgnoreCase);
            foreach (var item in outlines)
            {
                existingItems[item.Title] = item;
            }

            // Delete all current outline items (rule: OutlineCollection.Delete())
            outlines.Delete();

            // Re‑add items in the required sequence
            foreach (string title in desiredOrder)
            {
                if (existingItems.TryGetValue(title, out var item))
                {
                    // Add the outline item back to the collection
                    outlines.Add(item);
                }
            }

            // Save the modified PDF (lifecycle rule: use Document.Save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Reordered PDF saved to '{outputPath}'.");
    }
}
