using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_bookmarked.pdf";
        const string helpUrl = "https://example.com/documentation";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document using the core Document API.
        Document doc = new Document(inputPath);

        // Create a top‑level bookmark (outline item) that opens the online documentation URL.
        OutlineItemCollection helpBookmark = new OutlineItemCollection(doc.Outlines)
        {
            Title = "Help",
            Action = new GoToURIAction(helpUrl)
        };

        // Add the bookmark to the document's outline collection.
        doc.Outlines.Add(helpBookmark);

        // Save the modified PDF.
        doc.Save(outputPath);

        Console.WriteLine($"Bookmark added and saved to '{outputPath}'.");
    }
}