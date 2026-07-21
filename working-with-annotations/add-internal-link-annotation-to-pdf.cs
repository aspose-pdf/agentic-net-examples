using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_link.pdf";
        const int targetPageNumber = 3; // page to navigate to (1‑based)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF and ensure deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Choose the page where the link annotation will be placed
            Page sourcePage = doc.Pages[1]; // first page (1‑based index)

            // Define the clickable rectangle (llx, lly, urx, ury) in user space units
            Aspose.Pdf.Rectangle linkRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create the link annotation on the chosen page
            LinkAnnotation link = new LinkAnnotation(sourcePage, linkRect);
            link.Color = Aspose.Pdf.Color.Blue;
            // Border must be created after the annotation instance exists because it requires the parent annotation
            link.Border = new Border(link) { Width = 1 };

            // Set the action to navigate to the target page within the same document
            Page targetPage = doc.Pages[targetPageNumber];
            link.Action = new GoToAction(targetPage);

            // Add the annotation to the page's annotation collection
            sourcePage.Annotations.Add(link);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Link annotation added. Saved to '{outputPath}'.");
    }
}
