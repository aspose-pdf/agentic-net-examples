using System;
using System.Collections.Generic;
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

        // Define the vertices of the polyline
        List<Point> vertices = new List<Point>
        {
            new Point(100, 700),
            new Point(150, 750),
            new Point(200, 720),
            new Point(250, 770)
        };

        // Define a rectangle that encloses the polyline (use fully qualified type to avoid ambiguity)
        Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(90, 690, 260, 780);

        // Open the PDF, add the annotation, and save
        using (Document doc = new Document(inputPath))
        {
            // Aspose.Pdf uses 1‑based page indexing
            Page page = doc.Pages[1];

            // Create the polyline annotation with the page, rectangle, and vertices
            PolylineAnnotation polyline = new PolylineAnnotation(page, rect, vertices.ToArray())
            {
                Color = Aspose.Pdf.Color.Blue,   // Set annotation color
                Opacity = 0.8f,                  // Semi‑transparent
                Contents = "Sample polyline"     // Optional tooltip text
            };

            // Add the annotation to the page
            page.Annotations.Add(polyline);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Polyline annotation added and saved to '{outputPath}'.");
    }
}