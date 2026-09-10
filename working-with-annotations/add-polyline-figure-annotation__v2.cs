using System;
using System.IO;
using System.Collections.Generic;
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

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Select the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Define a rectangle that covers the whole page.
            // This rectangle is required by the PolylineAnnotation constructor.
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(0, 0, page.PageInfo.Width, page.PageInfo.Height);

            // List of points that form the polyline
            List<Aspose.Pdf.Point> pointList = new List<Aspose.Pdf.Point>
            {
                new Aspose.Pdf.Point(100, 700),
                new Aspose.Pdf.Point(150, 650),
                new Aspose.Pdf.Point(200, 600),
                new Aspose.Pdf.Point(250, 550)
            };

            // Convert the list to an array as required by the constructor
            Aspose.Pdf.Point[] vertices = pointList.ToArray();

            // Create the polyline annotation using the constructor that accepts page, rectangle, and vertices
            PolylineAnnotation polyline = new PolylineAnnotation(page, rect, vertices)
            {
                Color = Aspose.Pdf.Color.Blue,   // Set line color
                Opacity = 0.8f,                  // Semi‑transparent appearance
                Contents = "Sample polyline"     // Optional tooltip text
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(polyline);

            // Save the modified PDF to the output file
            doc.Save(outputPath);
        }

        Console.WriteLine($"Polyline annotation added and saved to '{outputPath}'.");
    }
}