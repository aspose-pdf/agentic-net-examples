using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "sample.pdf";
        const string outputPath = "glossary.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Verify the document has at least 45 pages (Aspose.Pdf uses 1‑based indexing)
            if (doc.Pages.Count < 45)
            {
                Console.Error.WriteLine("The document contains fewer than 45 pages.");
                return;
            }

            // Retrieve page 45
            Page targetPage = doc.Pages[45];

            // Create a top‑level bookmark (outline item) that points to page 45
            OutlineItemCollection bookmark = new OutlineItemCollection(doc.Outlines)
            {
                Title = "Glossary",
                // Destination that jumps to the top of the page (XYZExplicitDestination)
                Destination = new XYZExplicitDestination(targetPage, 0, targetPage.PageInfo.Height, 0),
                Open = true // expanded when the PDF is opened
            };

            // Add the bookmark to the document's outline collection
            doc.Outlines.Add(bookmark);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Bookmark added and saved to '{outputPath}'.");
    }
}
