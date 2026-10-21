using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "hyperlinked.pdf";

        // Page where the hyperlink annotation will be placed (1‑based index)
        const int annotationPage = 1;
        // Destination page number within the same document (1‑based index)
        const int destinationPage = 5;

        // Define the clickable rectangle (llx, lly, urx, ury)
        var linkRect = new Aspose.Pdf.Rectangle(100, 500, 200, 550);

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        var doc = new Document(inputPath);

        // Get the destination page object – Pages collection is 1‑based
        var destPage = doc.Pages[destinationPage];

        // Create a LinkAnnotation that points to a page inside the same document.
        // The constructor requires the page on which the annotation will appear and the rectangle.
        var linkAnnotation = new LinkAnnotation(doc.Pages[annotationPage], linkRect)
        {
            Action = new GoToAction(destPage)
        };

        // Add the annotation to the desired page
        doc.Pages[annotationPage].Annotations.Add(linkAnnotation);

        // Save the modified PDF
        doc.Save(outputPath);

        Console.WriteLine($"Hyperlink annotation added. Saved to '{outputPath}'.");
    }
}
