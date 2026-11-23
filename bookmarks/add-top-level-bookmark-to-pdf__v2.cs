using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "sample.pdf";
        const string outputPath = "chapter3_parent.pdf";
        const int targetPage = 60; // 1‑based page index

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document contains the requested page
            if (targetPage > doc.Pages.Count)
            {
                Console.Error.WriteLine($"Document has only {doc.Pages.Count} pages; cannot add bookmark to page {targetPage}.");
                return;
            }

            // Create a top‑level bookmark (outline item) that points to page 60
            OutlineItemCollection chapter = new OutlineItemCollection(doc.Outlines);
            chapter.Title = "Chapter Three";
            // XYZExplicitDestination(page, left, top, zoom). Using 0 for left/top keeps the current view.
            chapter.Destination = new XYZExplicitDestination(doc.Pages[targetPage], 0, 0, 0);
            // Add the bookmark to the document's outline collection
            doc.Outlines.Add(chapter);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Bookmark added and saved to '{outputPath}'.");
    }
}
