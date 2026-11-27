using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (IDisposable, so wrap in using)
        using (Document doc = new Document(inputPath))
        {
            // ------------------------------------------------------------
            // Create a parent bookmark (outline) for "Chapter 7" on page 15
            // ------------------------------------------------------------
            OutlineItemCollection chapter = new OutlineItemCollection(doc.Outlines)
            {
                Title = "Chapter 7",
                Action = new GoToAction(doc.Pages[15]) // Pages are 1‑based
            };

            // Add the parent bookmark to the document outline collection
            doc.Outlines.Add(chapter);

            // ------------------------------------------------------------
            // Create child bookmarks under the parent
            // ------------------------------------------------------------
            // NOTE: child items are also created with the *root* OutlineCollection (doc.Outlines)
            // and then added to the parent via chapter.Add(child).
            OutlineItemCollection section71 = new OutlineItemCollection(doc.Outlines)
            {
                Title = "Section 7.1",
                Action = new GoToAction(doc.Pages[16])
            };
            chapter.Add(section71);

            OutlineItemCollection section72 = new OutlineItemCollection(doc.Outlines)
            {
                Title = "Section 7.2",
                Action = new GoToAction(doc.Pages[17])
            };
            chapter.Add(section72);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Bookmarks added and saved to '{outputPath}'.");
    }
}
