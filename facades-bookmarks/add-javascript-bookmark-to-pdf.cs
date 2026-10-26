using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // for JavascriptAction

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "bookmarked_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        Document pdfDocument = new Document(inputPath);

        // ------------------------------------------------------------
        // Create a top‑level bookmark (outline item) that runs JavaScript
        // when the user clicks it.
        // ------------------------------------------------------------
        OutlineItemCollection rootBookmark = new OutlineItemCollection(pdfDocument.Outlines)
        {
            Title = "Show Message",
            Action = new JavascriptAction("app.alert('Root bookmark clicked');")
        };
        pdfDocument.Outlines.Add(rootBookmark);

        // ------------------------------------------------------------
        // Create a child bookmark under the root bookmark.
        // ------------------------------------------------------------
        OutlineItemCollection childBookmark = new OutlineItemCollection(pdfDocument.Outlines)
        {
            Title = "Child Action",
            Action = new JavascriptAction("app.alert('Child bookmark clicked');")
        };
        // Add the child to the parent bookmark (use Add, not Outlines)
        rootBookmark.Add(childBookmark);

        // Save the modified PDF with the new JavaScript bookmarks
        pdfDocument.Save(outputPath);

        Console.WriteLine($"PDF with JavaScript bookmarks saved to '{outputPath}'.");
    }
}