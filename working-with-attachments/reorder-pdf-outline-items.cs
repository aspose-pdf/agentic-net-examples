using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf; // Core PDF API

class ReorderPortfolioOutlines
{
    static void Main()
    {
        const string inputPath = "portfolio.pdf";
        const string outputPath = "portfolio_reordered.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document (using the standard load constructor)
        using (Document doc = new Document(inputPath))
        {
            // ---------------------------------------------------------------
            // 1. Extract current top‑level outline items into a list for manipulation
            // ---------------------------------------------------------------
            List<OutlineItemCollection> originalItems = new List<OutlineItemCollection>();
            foreach (OutlineItemCollection item in doc.Outlines)
            {
                originalItems.Add(item);
            }

            // ---------------------------------------------------------------
            // 2. Define the desired order.
            //    Example: reverse the sequence – replace with any custom logic.
            // ---------------------------------------------------------------
            originalItems.Reverse();

            // ---------------------------------------------------------------
            // 3. Remove all existing outline items from the document.
            // ---------------------------------------------------------------
            doc.Outlines.Clear();

            // ---------------------------------------------------------------
            // 4. Re‑add the items in the new sequence.
            //    We create a fresh OutlineItemCollection for each entry and copy the
            //    relevant properties (Title, Action, Destination, visual style, etc.).
            // ---------------------------------------------------------------
            foreach (OutlineItemCollection original in originalItems)
            {
                // Create a new outline entry based on the original one.
                OutlineItemCollection newItem = new OutlineItemCollection(doc.Outlines)
                {
                    Title = original.Title,
                    Action = original.Action,
                    Destination = original.Destination,
                    Color = original.Color,
                    Open = original.Open,
                    Bold = original.Bold,
                    Italic = original.Italic
                };

                // Add the newly created outline to the document's root outline collection.
                doc.Outlines.Add(newItem);

                // NOTE: If the original outline had child items, they would need to be
                // copied recursively. This example focuses on top‑level items only.
            }

            // ---------------------------------------------------------------
            // 5. Save the modified PDF.
            // ---------------------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"Reordered PDF saved to '{outputPath}'.");
    }
}
