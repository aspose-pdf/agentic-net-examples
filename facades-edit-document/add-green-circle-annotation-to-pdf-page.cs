using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_circle.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        Document doc = new Document(inputPath);

        // Ensure the document has at least two pages
        if (doc.Pages.Count < 2)
        {
            Console.Error.WriteLine("The PDF does not contain a second page.");
            return;
        }

        // Get page 2 (1‑based indexing)
        Page page = doc.Pages[2];

        // Create a circle annotation on page 2
        // Constructor requires the target page and a rectangle defining the bounds
        CircleAnnotation circle = new CircleAnnotation(
            page,
            new Aspose.Pdf.Rectangle(100, 500, 200, 600) // lower‑left (100,500), upper‑right (200,600)
        );

        // Set the interior (fill) colour to green – use InteriorColor, not FillColor
        circle.InteriorColor = Aspose.Pdf.Color.FromRgb(0, 255, 0);

        // Set a solid border with a width of 3 pt
        circle.Border = new Border(circle) { Width = 3 };
        // Optional: set border colour (default is black)
        circle.Color = Aspose.Pdf.Color.Black;

        // Add the annotation to the page
        page.Annotations.Add(circle);

        // Save the modified PDF
        doc.Save(outputPath);

        Console.WriteLine($"Circle annotation added and saved to '{outputPath}'.");
    }
}
