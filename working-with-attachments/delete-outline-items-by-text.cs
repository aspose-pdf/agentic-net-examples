using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "portfolio.pdf";
        const string outputPath = "portfolio_cleaned.pdf";
        const string matchText  = "Obsolete Item Description";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Get the collection of outline items (bookmarks)
            OutlineCollection outlines = doc.Outlines;

            // Collect titles of outline items whose description matches the target text
            var titlesToDelete = new System.Collections.Generic.List<string>();

            foreach (OutlineItemCollection item in outlines)
            {
                // The Title property holds the visible text of the outline entry
                if (item.Title != null && item.Title.Contains(matchText, StringComparison.OrdinalIgnoreCase))
                {
                    titlesToDelete.Add(item.Title);
                }
            }

            // Delete the matching outline items by title
            foreach (string title in titlesToDelete)
            {
                outlines.Delete(title);
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}