using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // for explicit destination types

class Program
{
    static void Main()
    {
        const string inputPath = "sample.pdf";
        const string outputPath = "reset.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document with deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Delete all existing bookmarks (outlines)
            doc.Outlines.Clear();

            // Create a new top‑level bookmark on page 1
            OutlineItemCollection newBookmark = new OutlineItemCollection(doc.Outlines);
            newBookmark.Title = "New Start";
            // Use a FitExplicitDestination to jump to the whole page (page index is 1‑based)
            newBookmark.Destination = new FitExplicitDestination(doc.Pages[1]);
            // Add the bookmark to the document's outline collection
            doc.Outlines.Add(newBookmark);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Bookmarks reset and saved to '{outputPath}'.");
    }
}
