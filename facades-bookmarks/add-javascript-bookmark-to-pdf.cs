using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_js_bookmarks.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF document.
        Document doc = new Document(inputPath);

        // Create a root bookmark (outline item) and assign a JavaScript action.
        OutlineItemCollection rootBookmark = new OutlineItemCollection(doc.Outlines)
        {
            Title = "Root Bookmark",
            Action = new JavascriptAction("app.alert('Root bookmark clicked');")
        };

        // Create a child bookmark with its own JavaScript action.
        OutlineItemCollection childBookmark = new OutlineItemCollection(doc.Outlines)
        {
            Title = "Child Bookmark",
            Action = new JavascriptAction("app.alert('Child bookmark clicked');")
        };

        // Attach the child to the root using the Add method (OutlineItemCollection is its own child collection).
        rootBookmark.Add(childBookmark);

        // Add the root bookmark (which now contains the child) to the document's outline collection.
        doc.Outlines.Add(rootBookmark);

        // Save the modified PDF.
        doc.Save(outputPath);

        Console.WriteLine($"PDF with JavaScript bookmarks saved to '{outputPath}'.");
    }
}
