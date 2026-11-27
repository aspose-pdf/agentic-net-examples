using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // needed for XYZExplicitDestination

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document using the core Document API (preferred for bookmark manipulation).
        Document doc = new Document(inputPath);

        // ----- Create the parent bookmark "Chapter 9" on page 6 -----
        // Use the XYZExplicitDestination overload that takes a page number.
        OutlineItemCollection chapter = new OutlineItemCollection(doc.Outlines);
        chapter.Title = "Chapter 9";
        // Page numbers are 1‑based; doc.Pages[6] is the 6th page.
        chapter.Destination = new XYZExplicitDestination(doc.Pages[6].Number, 0, doc.Pages[6].Rect.Height, 1.0f);
        // Add the top‑level bookmark to the document outline collection.
        doc.Outlines.Add(chapter);

        // ----- Create child bookmark "Section 9.1" on page 7 -----
        OutlineItemCollection section1 = new OutlineItemCollection(doc.Outlines);
        section1.Title = "Section 9.1";
        section1.Destination = new XYZExplicitDestination(doc.Pages[7].Number, 0, doc.Pages[7].Rect.Height, 1.0f);
        // Nest it under the parent bookmark.
        chapter.Add(section1);

        // ----- Create child bookmark "Section 9.2" on page 8 -----
        OutlineItemCollection section2 = new OutlineItemCollection(doc.Outlines);
        section2.Title = "Section 9.2";
        section2.Destination = new XYZExplicitDestination(doc.Pages[8].Number, 0, doc.Pages[8].Rect.Height, 1.0f);
        chapter.Add(section2);

        // Save the modified PDF.
        doc.Save(outputPath);

        Console.WriteLine($"Bookmarks added and saved to '{outputPath}'.");
    }
}
