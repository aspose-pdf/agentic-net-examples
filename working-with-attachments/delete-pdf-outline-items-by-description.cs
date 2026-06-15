using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "portfolio.pdf";
        const string outputPath = "portfolio_cleaned.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Text to match in the outline (portfolio item) description
            const string descriptionToDelete = "Obsolete Project";

            // Collect titles of outline items whose Title contains the description text
            var titlesToDelete = new System.Collections.Generic.List<string>();
            foreach (OutlineItemCollection outline in doc.Outlines)
            {
                // OutlineItemCollection represents a top‑level outline entry
                // Its Title property holds the visible text
                if (outline.Title != null && outline.Title.Contains(descriptionToDelete, StringComparison.OrdinalIgnoreCase))
                {
                    titlesToDelete.Add(outline.Title);
                }

                // Also check any child items recursively
                CollectChildTitles(outline, descriptionToDelete, titlesToDelete);
            }

            // Delete the matching outline items by title
            foreach (string title in titlesToDelete)
            {
                // Delete(string) removes the outline entry with the specified title
                doc.Outlines.Delete(title);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }

    // Recursively walk child outline items and collect matching titles
    private static void CollectChildTitles(OutlineItemCollection parent, string match, System.Collections.Generic.List<string> list)
    {
        foreach (OutlineItemCollection child in parent)
        {
            if (child.Title != null && child.Title.Contains(match, StringComparison.OrdinalIgnoreCase))
            {
                list.Add(child.Title);
            }

            // Recurse into deeper levels
            CollectChildTitles(child, match, list);
        }
    }
}