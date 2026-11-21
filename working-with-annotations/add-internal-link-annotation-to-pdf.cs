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
        const int    targetPage = 3;               // page to navigate to (1‑based)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the source PDF. The Document is wrapped in a using block for deterministic disposal.
        using (Document doc = new Document(inputPath))
        {
            // Choose the page where the link annotation will be placed.
            // Here we place it on the first page.
            Page page = doc.Pages[1];

            // Define the clickable rectangle (llx, lly, urx, ury) in points.
            // Use the fully qualified Aspose.Pdf.Rectangle to avoid ambiguity with System.Drawing.
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 700, 200, 750);

            // Create a link annotation that points to the target page within the same document.
            LinkAnnotation link = new LinkAnnotation(page, rect)
            {
                // GoToAction navigates to a page number (1‑based) in the same PDF.
                Action = new GoToAction(targetPage)
            };

            // Add the annotation to the page's annotation collection.
            page.Annotations.Add(link);

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Link annotation added. Output saved to '{outputPath}'.");
    }
}