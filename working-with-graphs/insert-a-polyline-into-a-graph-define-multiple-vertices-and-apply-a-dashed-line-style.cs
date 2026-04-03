using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Drawing;   // for Border and Dash

class Program
{
    static void Main()
    {
        const string outputPath = "polyline_dashed.pdf";

        // Create a new PDF document and add a blank page
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Define the annotation rectangle (position and size on the page)
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 600);

            // Define the vertices of the polyline
            Aspose.Pdf.Point[] vertices = new Aspose.Pdf.Point[]
            {
                new Aspose.Pdf.Point(120, 520),
                new Aspose.Pdf.Point(150, 560),
                new Aspose.Pdf.Point(200, 540),
                new Aspose.Pdf.Point(250, 580)
            };

            // Create the polyline annotation on the page
            PolylineAnnotation polyline = new PolylineAnnotation(page, rect, vertices);

            // Set the annotation color (optional)
            polyline.Color = Aspose.Pdf.Color.Blue;

            // Apply a dashed border style
            // Border constructor requires the parent annotation instance
            polyline.Border = new Border(polyline)
            {
                // Define dash pattern: 3 units on, 2 units off
                Dash = new Dash(3, 2)
            };

            // Add the annotation to the page
            page.Annotations.Add(polyline);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with dashed polyline saved to '{outputPath}'.");
    }
}