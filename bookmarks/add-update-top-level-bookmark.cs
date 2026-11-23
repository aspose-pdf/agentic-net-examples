using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "sample.pdf";
        const string outputPath = "dedicationupd.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Create a top‑level bookmark (OutlineItemCollection) pointing to page 1
            OutlineItemCollection bookmark = new OutlineItemCollection(doc.Outlines)
            {
                Title = "Dedication",
                Action = new GoToAction(doc.Pages[1])
            };

            // Add the bookmark to the document's outline collection
            doc.Outlines.Add(bookmark);

            // Update the bookmark's title
            bookmark.Title = "Dedication Updated";

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Updated PDF saved to '{outputPath}'.");
    }
}
