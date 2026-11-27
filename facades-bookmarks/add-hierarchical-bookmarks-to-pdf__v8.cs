using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_bookmarked.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Bind the PDF with PdfBookmarkEditor and work on the underlying Document
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            editor.BindPdf(inputPath);
            Document doc = editor.Document; // underlying Document instance

            // ---------- Build bookmark hierarchy using OutlineItemCollection ----------
            // Parent bookmark (level 1)
            OutlineItemCollection parent = new OutlineItemCollection(doc.Outlines)
            {
                Title = "Parent Bookmark",
                Action = new GoToAction(doc.Pages[1]), // go to first page
                Open = true
            };

            // ----- First child (level 2) -----
            OutlineItemCollection child1 = new OutlineItemCollection(doc.Outlines)
            {
                Title = "Child 1",
                Action = new GoToAction(doc.Pages[1]),
                Open = true
            };
            // Sub‑children of Child 1 (level 3)
            OutlineItemCollection child1Sub1 = new OutlineItemCollection(doc.Outlines)
            {
                Title = "Child 1 - Sub 1",
                Action = new GoToAction(doc.Pages[1])
            };
            OutlineItemCollection child1Sub2 = new OutlineItemCollection(doc.Outlines)
            {
                Title = "Child 1 - Sub 2",
                Action = new GoToAction(doc.Pages[1])
            };
            child1.Add(child1Sub1);
            child1.Add(child1Sub2);

            // ----- Second child (level 2) -----
            OutlineItemCollection child2 = new OutlineItemCollection(doc.Outlines)
            {
                Title = "Child 2",
                Action = new GoToAction(doc.Pages[1]),
                Open = true
            };
            // Sub‑children of Child 2 (level 3)
            OutlineItemCollection child2Sub1 = new OutlineItemCollection(doc.Outlines)
            {
                Title = "Child 2 - Sub 1",
                Action = new GoToAction(doc.Pages[1])
            };
            OutlineItemCollection child2Sub2 = new OutlineItemCollection(doc.Outlines)
            {
                Title = "Child 2 - Sub 2",
                Action = new GoToAction(doc.Pages[1])
            };
            child2.Add(child2Sub1);
            child2.Add(child2Sub2);

            // Assemble the hierarchy
            parent.Add(child1);
            parent.Add(child2);
            doc.Outlines.Add(parent);

            // Save the PDF with the new bookmark hierarchy
            editor.Save(outputPath);
        }

        Console.WriteLine($"Bookmarks added and saved to '{outputPath}'.");
    }
}
