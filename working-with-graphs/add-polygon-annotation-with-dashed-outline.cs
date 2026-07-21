using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Define the rectangle that bounds the polygon annotation (can be zero-sized)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(0, 0, 0, 0);

            // Define polygon vertices (example: a triangle)
            Aspose.Pdf.Point[] vertices = new Aspose.Pdf.Point[]
            {
                new Aspose.Pdf.Point(100, 500),
                new Aspose.Pdf.Point(200, 700),
                new Aspose.Pdf.Point(300, 500)
            };

            // Create the polygon annotation on the page
            PolygonAnnotation polygon = new PolygonAnnotation(page, rect, vertices);

            // Set interior fill color (pattern fill is not directly supported; using solid color)
            polygon.InteriorColor = Aspose.Pdf.Color.LightGray;

            // Set outline color
            polygon.Color = Aspose.Pdf.Color.DarkBlue;

            // Configure the border with a dash pattern
            // Border constructor requires the parent annotation
            Border border = new Border(polygon)
            {
                // Define dash pattern: 5 units on, 3 units off
                Dash = new Dash(5, 3)
            };
            polygon.Border = border;

            // Optionally set other appearance properties
            polygon.Opacity = 0.8f; // semi‑transparent outline

            // Add the annotation to the page
            page.Annotations.Add(polygon);

            // Save the PDF
            const string outputPath = "PolygonGraph.pdf";
            doc.Save(outputPath);
            Console.WriteLine($"PDF saved to '{outputPath}'.");
        }
    }
}