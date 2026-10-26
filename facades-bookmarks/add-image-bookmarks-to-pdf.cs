using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // for GoToAction, OutlineItemCollection, ExplicitDestinationType

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_image_bookmarks.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Collect bookmark data: title, page number, left, top, zoom
        var bookmarks = new List<(string Title, int Page, double Left, double Top, double Zoom)>();
        int imageCounter = 1;

        // Load the PDF to inspect images
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                Page page = doc.Pages[pageNum];

                // Iterate over all images on the page
                foreach (XImage img in page.Resources.Images)
                {
                    // Title for the bookmark
                    string title = $"Image {imageCounter} (Page {pageNum})";

                    // Coordinates are not directly available for resource images.
                    // Use (0,0) as a placeholder; the bookmark will jump to the page.
                    bookmarks.Add((title, pageNum, 0, 0, 0));

                    imageCounter++;
                }
            }

            // Add bookmarks using the modern OutlineItemCollection API
            foreach (var bm in bookmarks)
            {
                // Create a GoToAction that points to the page (and optionally coordinates/zoom)
                // ExplicitDestinationType.XYZ allows specifying left, top and zoom values.
                var action = new GoToAction(doc.Pages[bm.Page], ExplicitDestinationType.XYZ, bm.Left, bm.Top, bm.Zoom);

                var outline = new OutlineItemCollection(doc.Outlines)
                {
                    Title = bm.Title,
                    Action = action
                };

                doc.Outlines.Add(outline);
            }

            // Save the PDF with the new bookmarks
            doc.Save(outputPath);
        }

        Console.WriteLine($"Bookmarks added. Output saved to '{outputPath}'.");
    }
}
