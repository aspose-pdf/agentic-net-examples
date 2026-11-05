using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Choose the page where the new bookmark should point.
            // Aspose.Pdf uses 1‑based page indexing.
            int targetPageNumber = 1; // change as needed
            if (targetPageNumber < 1 || targetPageNumber > doc.Pages.Count)
            {
                Console.Error.WriteLine("Target page number is out of range.");
                return;
            }

            // Create a new outline (bookmark) item using the document model API
            OutlineItemCollection newItem = new OutlineItemCollection(doc.Outlines)
            {
                Title = "New Section Heading",
                // Use a GoToAction to point to the chosen page (top of the page)
                Action = new GoToAction(doc.Pages[targetPageNumber])
            };

            // Insert the new outline item at the desired position.
            // OutlineCollection does not provide an Insert method, so we rebuild the collection.
            // Desired position: second top‑level bookmark (index 1, zero‑based).
            const int desiredIndex = 1;
            List<OutlineItemCollection> current = new List<OutlineItemCollection>();
            foreach (OutlineItemCollection item in doc.Outlines)
                current.Add(item);

            // Ensure the index is within the valid range.
            int insertPos = Math.Max(0, Math.Min(desiredIndex, current.Count));
            current.Insert(insertPos, newItem);

            // Clear existing outlines and re‑add them in the new order.
            doc.Outlines.Clear();
            foreach (OutlineItemCollection item in current)
                doc.Outlines.Add(item);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with new heading bookmark to '{outputPath}'.");
    }
}
