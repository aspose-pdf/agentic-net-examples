using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "portfolio.pdf";
        const string outputPath = "portfolio_clean.pdf";

        // Descriptions (titles) of outline items to remove
        var descriptionsToDelete = new List<string>
        {
            "Old Project",
            "Deprecated Item",
            "Obsolete Section"
        };

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Collect titles of outline items that match the descriptions
            var titlesToDelete = new List<string>();
            foreach (OutlineItemCollection outlineItem in doc.Outlines)
            {
                // OutlineItemCollection.Title holds the display text of the outline entry
                if (descriptionsToDelete.Contains(outlineItem.Title))
                {
                    titlesToDelete.Add(outlineItem.Title);
                }
            }

            // Delete each matching outline item by title
            foreach (string title in titlesToDelete)
            {
                doc.Outlines.Delete(title);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Portfolio PDF saved to '{outputPath}'.");
    }
}