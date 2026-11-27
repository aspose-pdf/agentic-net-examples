using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // for explicit destination types

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string bookmarkTitle = "Editor Reference";
        const int bookmarkPage = 14; // Aspose.Pdf uses 1‑based page numbers

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF with the high‑level Document API.
            Document pdf = new Document(inputPath);

            // Ensure the requested page exists.
            if (bookmarkPage < 1 || bookmarkPage > pdf.Pages.Count)
            {
                Console.Error.WriteLine($"Page {bookmarkPage} is out of range. Document has {pdf.Pages.Count} pages.");
                return;
            }

            // Create a bookmark (outline item). In Aspose.Pdf an OutlineItemCollection is both the node and the collection.
            OutlineItemCollection bookmark = new OutlineItemCollection(pdf.Outlines)
            {
                Title = bookmarkTitle,
                // XYZExplicitDestination(page, left, top, zoom) – jump to the top‑left of the page.
                Destination = new XYZExplicitDestination(pdf.Pages[bookmarkPage], 0, pdf.Pages[bookmarkPage].PageInfo.Height, 0)
            };

            // Add the bookmark to the document's outline collection.
            pdf.Outlines.Add(bookmark);

            // Save the modified PDF.
            pdf.Save(outputPath);

            Console.WriteLine($"Bookmark added and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
