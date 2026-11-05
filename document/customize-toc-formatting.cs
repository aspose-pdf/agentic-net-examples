using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using System.Drawing;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block to ensure deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Add a top‑level bookmark (outline) that points to the first page.
            // OutlineItemCollection constructor takes the parent collection (doc.Outlines).
            OutlineItemCollection bookmark = new OutlineItemCollection(doc.Outlines)
            {
                Title = "First Page",
                // Use an explicit destination; XYZExplicitDestination lives in Aspose.Pdf.Annotations.
                Destination = new XYZExplicitDestination(doc.Pages[1], 0, doc.Pages[1].Rect.Height, 1)
            };
            doc.Outlines.Add(bookmark);

            // Save the modified PDF (PDF format)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processing completed and saved to '{outputPath}'.");
    }
}
