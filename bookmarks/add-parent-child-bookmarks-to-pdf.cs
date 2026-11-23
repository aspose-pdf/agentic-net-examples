using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // Required for XYZExplicitDestination

class Program
{
    static void Main()
    {
        const string inputPath = "sample.pdf";
        const string outputPath = "index.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Verify the document has at least 51 pages (Aspose.Pdf uses 1‑based indexing)
            if (doc.Pages.Count < 51)
            {
                Console.Error.WriteLine("The document does not contain at least 51 pages.");
                return;
            }

            // Create the parent bookmark "Index" on page 50
            OutlineItemCollection parent = new OutlineItemCollection(doc.Outlines)
            {
                Title = "Index",
                Destination = new XYZExplicitDestination(50, 0, 0, 1) // page number overload
            };
            doc.Outlines.Add(parent);

            // Create the child bookmark "A-Z" on page 51 and attach it to the parent
            OutlineItemCollection child = new OutlineItemCollection(doc.Outlines)
            {
                Title = "A-Z",
                Destination = new XYZExplicitDestination(51, 0, 0, 1) // page number overload
            };
            parent.Add(child);

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Bookmarks added and saved to '{outputPath}'.");
    }
}
