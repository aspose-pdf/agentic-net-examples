using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "polyline_dashed.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Define the annotation rectangle (position and size on the page)
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 400, 800);

            // Define the polyline vertices (points are in page coordinate space)
            Aspose.Pdf.Point[] vertices = new Aspose.Pdf.Point[]
            {
                new Aspose.Pdf.Point(120, 520),
                new Aspose.Pdf.Point(200, 600),
                new Aspose.Pdf.Point(300, 550),
                new Aspose.Pdf.Point(380, 720)
            };

            // Create the polyline annotation
            PolylineAnnotation polyline = new PolylineAnnotation(page, rect, vertices)
            {
                // Optional: set the annotation color
                Color = Aspose.Pdf.Color.Blue,
                // Optional: add a popup text
                Contents = "Dashed polyline annotation"
            };

            // Create a Border object – it requires the parent annotation in the constructor
            Border border = new Border(polyline)
            {
                // Set border width (thickness)
                Width = 2,
                // Apply a dashed pattern: 3 units on, 2 units off
                Dash = new Dash(3, 2)
            };

            // Assign the border to the annotation
            polyline.Border = border;

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(polyline);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Polyline with dashed style saved to '{outputPath}'.");
    }
}