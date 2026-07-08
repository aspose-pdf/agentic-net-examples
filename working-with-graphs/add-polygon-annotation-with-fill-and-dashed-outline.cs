using System;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Create a new PDF document and add a blank page
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Define the vertices of the polygon
            Point[] vertices = new Point[]
            {
                new Point(100, 500),
                new Point(200, 600),
                new Point(300, 500),
                new Point(200, 400)
            };

            // Create a polygon annotation on the page.
            // The rectangle argument can be a zero‑size rectangle because the shape is defined by the vertices.
            PolygonAnnotation polygon = new PolygonAnnotation(
                page,
                new Aspose.Pdf.Rectangle(0, 0, 0, 0),
                vertices);

            // Fill the polygon with a solid color (pattern fill is not directly supported; solid color is used here)
            polygon.InteriorColor = Aspose.Pdf.Color.LightGray;

            // Configure the outline (border) dash style
            Border border = new Border(polygon)
            {
                Width = 2,                     // Border width
                Dash = new Dash(5, 2)          // 5 units on, 2 units off
            };
            polygon.Border = border;

            // Optionally set the border color
            polygon.Color = Aspose.Pdf.Color.DarkBlue;

            // Add the polygon annotation to the page
            page.Annotations.Add(polygon);

            // Save the PDF
            doc.Save("PolygonWithPatternAndDash.pdf");
        }

        Console.WriteLine("PDF created with a polygon, fill color, and dashed outline.");
    }
}