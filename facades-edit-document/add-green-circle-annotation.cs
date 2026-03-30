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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Get the second page (pages are 1‑based)
            Page page = doc.Pages[2];

            // Define the rectangle that bounds the circle annotation
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 600);

            // Create the circle annotation on the specified page
            CircleAnnotation circle = new CircleAnnotation(page, rect);

            // Set the interior (fill) color to green
            circle.InteriorColor = Aspose.Pdf.Color.Green;

            // Configure the border: 3 pt width (Border requires the parent annotation in its constructor)
            circle.Border = new Border(circle) { Width = 3 };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(circle);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Circle annotation added and saved to '{outputPath}'.");
    }
}