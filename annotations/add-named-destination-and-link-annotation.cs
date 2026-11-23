using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // GoToAction, LinkAnnotation, HighlightingMode, etc.

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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least 6 pages
            if (doc.Pages.Count < 6)
            {
                Console.Error.WriteLine("The document does not contain page 6.");
                return;
            }

            // Page indexing in Aspose.Pdf is 1‑based
            Page page6 = doc.Pages[6];

            // -------------------------------------------------
            // 1. Create a named destination "Section2" on page 6
            // -------------------------------------------------
            // Use an explicit XYZ destination (fit page) – coordinates (0,0) and zoom 1
            var xyzDest = new XYZExplicitDestination(page6, 0, 0, 1);
            doc.NamedDestinations.Add("Section2", xyzDest);

            // -------------------------------------------------
            // 2. Create a LinkAnnotation on page 6 that points to the named destination
            // -------------------------------------------------
            // Define the clickable rectangle (llx, lly, urx, ury) in user space units
            Aspose.Pdf.Rectangle linkRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create the annotation and assign a GoToAction referencing the named destination
            LinkAnnotation link = new LinkAnnotation(page6, linkRect)
            {
                Action = new GoToAction(doc, "Section2"),
                // Optional: visual feedback when the link is clicked
                Highlighting = HighlightingMode.Push
            };

            // Add the annotation to the page's annotation collection
            page6.Annotations.Add(link);

            // -------------------------------------------------
            // 3. Save the modified PDF
            // -------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with named destination and link: {outputPath}");
    }
}
