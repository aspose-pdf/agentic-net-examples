using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // Added for GoToURIAction

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_bookmark.pdf";
        const string bookmarkTitle = "Example Site";
        const string url = "https://example.org";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document doc = new Document(inputPath);

        // Create a bookmark (outline item) that opens an external URL
        OutlineItemCollection bookmark = new OutlineItemCollection(doc.Outlines)
        {
            Title = bookmarkTitle,
            Action = new GoToURIAction(url)
        };

        // Add the bookmark to the document's outlines collection
        doc.Outlines.Add(bookmark);

        // Save the updated PDF
        doc.Save(outputPath);

        Console.WriteLine($"Bookmark added and saved to '{outputPath}'.");
    }
}
