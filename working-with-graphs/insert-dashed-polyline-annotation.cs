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
            // Add a single page (1‑based indexing)
            doc.Pages.Add();
            Page page = doc.Pages[1];

            // Define the annotation rectangle (must enclose all vertices)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(50, 450, 350, 650);

            // Define the polyline vertices
            Aspose.Pdf.Point[] vertices = new Aspose.Pdf.Point[]
            {
                new Aspose.Pdf.Point(100, 500),
                new Aspose.Pdf.Point(150, 600),
                new Aspose.Pdf.Point(200, 550),
                new Aspose.Pdf.Point(250, 620),
                new Aspose.Pdf.Point(300, 500)
            };

            // Create the polyline annotation
            PolylineAnnotation polyline = new PolylineAnnotation(page, rect, vertices)
            {
                // Set the line color
                Color = Aspose.Pdf.Color.Blue,
                // Optional: set opacity (0.0 – 1.0)
                Opacity = 0.8
            };

            // Apply a dashed border style.
            // Border requires the parent annotation in its constructor.
            polyline.Border = new Border(polyline)
            {
                // Define dash pattern: 5 units on, 3 units off
                Dash = new Dash(5, 3)
            };

            // Add the annotation to the page
            page.Annotations.Add(polyline);

            // Save the PDF
            doc.Save("polyline_output.pdf");
        }

        Console.WriteLine("Polyline annotation with dashed style saved to 'polyline_output.pdf'.");
    }
}