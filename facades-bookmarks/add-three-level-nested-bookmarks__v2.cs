using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "bookmarked.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document.
        Document doc = new Document(inputPath);

        // ----- Create the bookmark hierarchy -----
        // Top‑level bookmark "Series A" pointing to page 1.
        OutlineItemCollection seriesA = new OutlineItemCollection(doc.Outlines);
        seriesA.Title = "Series A";
        seriesA.Action = new GoToAction(doc.Pages[1]); // page numbers are 1‑based, pass a Page object
        doc.Outlines.Add(seriesA);

        // Child bookmark "Episode 1" under "Series A" pointing to page 2.
        OutlineItemCollection episode1 = new OutlineItemCollection(doc.Outlines);
        episode1.Title = "Episode 1";
        episode1.Action = new GoToAction(doc.Pages[2]);
        seriesA.Add(episode1);

        // Sub‑child bookmark "Scene 1.1" under "Episode 1" pointing to page 3.
        OutlineItemCollection scene11 = new OutlineItemCollection(doc.Outlines);
        scene11.Title = "Scene 1.1";
        scene11.Action = new GoToAction(doc.Pages[3]);
        episode1.Add(scene11);
        // ------------------------------------------

        // Save the modified PDF.
        doc.Save(outputPath);
        Console.WriteLine($"Bookmarks added and saved to '{outputPath}'.");
    }
}
