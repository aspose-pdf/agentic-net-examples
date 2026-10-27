using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_link.pdf";
        const int   targetPage = 3;               // page to navigate to (1‑based)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF and ensure deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Choose the page where the link annotation will be placed
            Page page = doc.Pages[1]; // first page (1‑based indexing)

            // Define the rectangle area for the clickable link (coordinates in points)
            // Fully qualify Rectangle to avoid ambiguity with System.Drawing
            Aspose.Pdf.Rectangle linkRect = new Aspose.Pdf.Rectangle(100, 700, 200, 720);

            // Create the link annotation on the chosen page
            LinkAnnotation link = new LinkAnnotation(page, linkRect);

            // Build a GoTo action that points directly to the target page
            // (Avoid using the Destination class which may be unavailable in some versions)
            link.Action = new GoToAction(doc.Pages[targetPage]);

            // Optional: visual appearance (border, color)
            link.Border = new Border(link) { Width = 1 };
            link.Color = Aspose.Pdf.Color.Blue;

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(link);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with internal link saved to '{outputPath}'.");
    }
}
