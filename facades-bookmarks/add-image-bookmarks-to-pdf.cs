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
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (Document implements IDisposable)
        using (Document doc = new Document(inputPath))
        {
            int imageIndex = 0;

            // Pages are 1‑based in Aspose.Pdf
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                Page page = doc.Pages[pageNum];

                // Enumerate images that are stored in the page resources
                foreach (XImage img in page.Resources.Images)
                {
                    imageIndex++;
                    string title = $"Image {imageIndex} (Page {pageNum})";

                    // Destination – use the page's MediaBox. XYZExplicitDestination expects
                    // left, top and zoom (zoom = 0 means "use the current zoom").
                    double left = page.MediaBox.LLX;
                    double top  = page.MediaBox.URY; // top coordinate of the page
                    double zoom = 0;                 // default zoom

                    // Create a top‑level bookmark (outline item)
                    OutlineItemCollection bookmark = new OutlineItemCollection(doc.Outlines)
                    {
                        Title = title,
                        Destination = new XYZExplicitDestination(page, left, top, zoom)
                    };

                    // Add the bookmark to the document's outline collection
                    doc.Outlines.Add(bookmark);
                }
            }

            // Save the PDF with the newly added bookmarks
            doc.Save(outputPath);
        }

        Console.WriteLine($"Bookmarks added for images and saved to '{outputPath}'.");
    }
}
