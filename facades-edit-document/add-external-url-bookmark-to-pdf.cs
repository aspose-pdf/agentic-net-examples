using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // Actions like GoToURIAction now live here

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_bookmarked.pdf";
        const string bookmarkTitle = "Visit Example.org";
        const string externalUrl = "https://example.org";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document.
        Document doc = new Document(inputPath);

        // Create a top‑level bookmark that opens an external URL.
        OutlineItemCollection bookmark = new OutlineItemCollection(doc.Outlines)
        {
            Title = bookmarkTitle,
            Action = new GoToURIAction(externalUrl)
        };

        // Add the bookmark to the document's outline collection.
        doc.Outlines.Add(bookmark);

        // Save the modified PDF.
        doc.Save(outputPath);

        Console.WriteLine($"Bookmark added and saved to '{outputPath}'.");
    }
}
