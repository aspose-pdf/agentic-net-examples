using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using System.Drawing;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_bookmarked.pdf";
        const string parentBookmarkTitle = "Chapter 1";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (core API provides full outline manipulation)
        Document doc = new Document(inputPath);

        // Find the existing parent bookmark (outline) named "Chapter 1"
        OutlineItemCollection parent = null;
        foreach (OutlineItemCollection item in doc.Outlines)
        {
            if (item.Title == parentBookmarkTitle)
            {
                parent = item;
                break;
            }
        }

        if (parent == null)
        {
            Console.Error.WriteLine($"Parent bookmark '{parentBookmarkTitle}' not found.");
            return;
        }

        // Helper to create a child outline that points to a specific page.
        // Aspose.Pdf pages are 1‑based; doc.Pages[pageNumber] returns the page.
        OutlineItemCollection CreateChild(string title, int pageNumber)
        {
            var child = new OutlineItemCollection(doc.Outlines)
            {
                Title = title,
                // Destination that jumps to the top of the target page (no zoom/offset)
                Destination = new XYZExplicitDestination(doc.Pages[pageNumber], 0, 0, 0)
            };
            return child;
        }

        // Add child bookmarks (sub‑sections) under the found parent.
        parent.Add(CreateChild("Section 1.1", 2)); // page 2
        parent.Add(CreateChild("Section 1.2", 5)); // page 5
        parent.Add(CreateChild("Section 1.3", 8)); // page 8

        // Save the updated PDF with the new bookmark hierarchy.
        doc.Save(outputPath);
        Console.WriteLine($"Bookmarks added and saved to '{outputPath}'.");
    }
}
