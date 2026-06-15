using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "polyline_annotated.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (using statement ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Choose the page where the annotation will be placed (first page in this example)
            Page page = doc.Pages[1];

            // Define the rectangle that bounds the annotation.
            // This rectangle can be larger than the polyline; the actual vertices define the shape.
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 400, 800);

            // Define a list of points for the polyline.
            List<Aspose.Pdf.Point> pointList = new List<Aspose.Pdf.Point>
            {
                new Aspose.Pdf.Point(120, 750),
                new Aspose.Pdf.Point(200, 700),
                new Aspose.Pdf.Point(280, 750),
                new Aspose.Pdf.Point(360, 700)
            };

            // Convert the list to an array as required by the constructor.
            Aspose.Pdf.Point[] vertices = pointList.ToArray();

            // Create the polyline annotation.
            PolylineAnnotation polyline = new PolylineAnnotation(page, rect, vertices)
            {
                // Optional visual properties
                Color = Aspose.Pdf.Color.Blue,
                Opacity = 0.7f,
                Contents = "Sample polyline annotation"
            };

            // Add the annotation to the page's annotation collection.
            page.Annotations.Add(polyline);

            // Save the modified document.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Polyline annotation added and saved to '{outputPath}'.");
    }
}