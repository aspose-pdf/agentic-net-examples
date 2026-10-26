using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Define the circle bounds (lower‑left and upper‑right corners)
        double llx = 100; // lower‑left X
        double lly = 500; // lower‑left Y
        double urx = 200; // upper‑right X
        double ury = 600; // upper‑right Y

        // Load the PDF document
        Document pdf = new Document(inputPath);

        // Ensure the document has at least two pages
        if (pdf.Pages.Count < 2)
        {
            Console.Error.WriteLine("The PDF does not contain a page 2.");
            return;
        }

        // Get page 2 (1‑based indexing)
        Page page = pdf.Pages[2];

        // Create a circle annotation with green fill and 3 pt border width
        CircleAnnotation circle = new CircleAnnotation(page, new Rectangle(llx, lly, urx, ury));
        // Fill color for shape annotations is set via InteriorColor
        circle.InteriorColor = Color.Green;
        // Border must be created after the annotation instance exists
        circle.Border = new Border(circle) { Width = 3 };

        // Add the annotation to the page
        page.Annotations.Add(circle);

        // Save the modified PDF
        pdf.Save(outputPath);

        Console.WriteLine($"Circle annotation added to page 2 and saved as '{outputPath}'.");
    }
}