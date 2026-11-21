using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "annotated.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document doc = new Document(inputPath);

        // Ensure the document has at least 6 pages
        if (doc.Pages.Count < 6)
        {
            Console.Error.WriteLine("The PDF does not contain a page 6.");
            return;
        }

        // Define the rectangle that encloses the diagram on page 6
        // (coordinates are in points; adjust as needed for the actual diagram)
        Rectangle diagramRect = new Rectangle(
            llx: 100,   // lower‑left X
            lly: 400,   // lower‑left Y
            urx: 500,   // upper‑right X
            ury: 800    // upper‑right Y
        );

        // Get page 6 (Aspose.Pdf pages are 1‑based)
        Page page = doc.Pages[6];

        // Create a circle annotation with a thick green outline
        CircleAnnotation circle = new CircleAnnotation(page, diagramRect)
        {
            Color = Color.Green // Border colour is taken from the annotation's Color property
        };

        // Assign a Border to the annotation – Border requires the owning annotation as a constructor argument
        circle.Border = new Border(circle)
        {
            Width = 5 // Width is an integer (points)
        };

        // Add the annotation to the page
        page.Annotations.Add(circle);

        // Save the modified PDF
        doc.Save(outputPath);

        Console.WriteLine($"Circle annotation added. Output saved to '{outputPath}'.");
    }
}
