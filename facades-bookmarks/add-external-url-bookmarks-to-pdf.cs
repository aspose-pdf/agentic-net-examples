using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // required for action types

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document pdf = new Document(inputPath);

        // Add external URL bookmarks (level 1 in the outline hierarchy)
        AddExternalBookmark(pdf, "Google", "https://www.google.com");
        AddExternalBookmark(pdf, "Stack Overflow", "https://stackoverflow.com");
        AddExternalBookmark(pdf, "Aspose PDF Docs", "https://docs.aspose.com/pdf/net/");

        // Save the PDF with the new bookmarks
        pdf.Save(outputPath);

        Console.WriteLine($"Bookmarks added and saved to '{outputPath}'.");
    }

    /// <summary>
    /// Adds a top‑level bookmark that points to an external URL.
    /// </summary>
    /// <param name="doc">The PDF document.</param>
    /// <param name="title">The text displayed in the bookmark pane.</param>
    /// <param name="url">The external web address.</param>
    private static void AddExternalBookmark(Document doc, string title, string url)
    {
        // Create a new outline item (bookmark)
        OutlineItemCollection bookmark = new OutlineItemCollection(doc.Outlines)
        {
            Title = title,
            Action = new GoToURIAction(url) // Opens the URL in a browser when the bookmark is clicked
        };

        // Add the bookmark to the document's outline collection (top level)
        doc.Outlines.Add(bookmark);
    }
}