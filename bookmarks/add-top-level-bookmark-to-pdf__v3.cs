using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "sample.pdf";
        const string outputPath = "cover.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a top‑level bookmark (outline item) for page 1
            OutlineItemCollection coverBookmark = new OutlineItemCollection(doc.Outlines);
            coverBookmark.Title = "Cover";
            // Set the action to navigate to the first page
            coverBookmark.Action = new GoToAction(doc.Pages[1]);

            // Add the bookmark to the document's outline collection
            doc.Outlines.Add(coverBookmark);

            // Save the modified document as PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Bookmark added and saved to '{outputPath}'.");
    }
}
