using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "portfolio.pdf";
        const string outputPath = "portfolio_cleaned.pdf";
        const string matchText  = "Obsolete Item"; // description text to match

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (using rule: document-disposal-with-using)
        using (Document doc = new Document(inputPath))
        {
            // Collect outline items whose title matches the target text
            List<OutlineItemCollection> itemsToDelete = new List<OutlineItemCollection>();

            foreach (OutlineItemCollection outline in doc.Outlines)
            {
                // The OutlineItemCollection.Title holds the visible text of the bookmark entry
                if (!string.IsNullOrEmpty(outline.Title) &&
                    outline.Title.Contains(matchText, StringComparison.OrdinalIgnoreCase))
                {
                    itemsToDelete.Add(outline);
                }
            }

            // Delete the matching outline items – Delete expects the title string, not the object
            foreach (OutlineItemCollection item in itemsToDelete)
            {
                doc.Outlines.Delete(item.Title);
            }

            // Save the modified PDF (using rule: document-disposal-with-using)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}
