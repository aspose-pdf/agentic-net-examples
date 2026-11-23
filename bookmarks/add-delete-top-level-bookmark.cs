using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "sample.pdf";
        const string outputPath = "legaltemp.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            int pageNumber = 70; // 1‑based index used by Aspose.Pdf
            if (pageNumber > doc.Pages.Count)
            {
                Console.Error.WriteLine($"Document has only {doc.Pages.Count} pages; cannot add bookmark to page {pageNumber}.");
                return;
            }

            // Create a top‑level bookmark (OutlineItemCollection is the bookmark node type)
            OutlineItemCollection bookmark = new OutlineItemCollection(doc.Outlines)
            {
                Title = "Legal",
                Open = true,
                Destination = new XYZExplicitDestination(doc.Pages[pageNumber], 0, 0, 0)
            };

            // Add the bookmark to the document's outline collection
            doc.Outlines.Add(bookmark);

            // Delete the bookmark we just added (Delete expects the bookmark title)
            doc.Outlines.Delete("Legal");

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved file: {outputPath}");
    }
}
