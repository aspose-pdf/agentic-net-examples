using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "polyline_demo.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Define the annotation rectangle (position and size on the page)
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(80, 480, 320, 620);

            // Define the vertices of the polyline
            Point[] vertices = new Point[]
            {
                new Point(100, 500),
                new Point(150, 560),
                new Point(200, 520),
                new Point(250, 580),
                new Point(300, 540)
            };

            // Create the polyline annotation
            PolylineAnnotation polyline = new PolylineAnnotation(page, rect, vertices)
            {
                // Set the line color
                Color = Color.Blue
            };

            // Configure the border with a dashed style
            // Border requires the parent annotation in its constructor
            polyline.Border = new Border(polyline)
            {
                Width = 2,                     // Line thickness
                Dash = new Dash(4, 2)          // 4 units dash, 2 units gap
            };

            // Add the annotation to the page
            page.Annotations.Add(polyline);

            // Save the document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with polyline saved to '{outputPath}'.");
    }
}