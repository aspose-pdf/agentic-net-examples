using System;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "polyline.pdf";

        // Create a new PDF document and add a blank page
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Define the annotation rectangle (position on the page)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 400, 200);

            // Define the polyline vertices (page coordinate system)
            Aspose.Pdf.Point[] vertices = new Aspose.Pdf.Point[]
            {
                new Aspose.Pdf.Point(120, 520),
                new Aspose.Pdf.Point(200, 580),
                new Aspose.Pdf.Point(300, 540),
                new Aspose.Pdf.Point(380, 600)
            };

            // Create the polyline annotation on the page
            PolylineAnnotation poly = new PolylineAnnotation(page, rect, vertices);

            // Set line color
            poly.Color = Aspose.Pdf.Color.Blue;

            // Apply a dashed line style via the Border.Dash property
            // Dash(onLength, offLength) defines the dash pattern
            poly.Border = new Border(poly) { Dash = new Dash(5, 3) };

            // Optional tooltip text
            poly.Contents = "Sample polyline with dashed style";

            // Add the annotation to the page
            page.Annotations.Add(poly);

            // Save the document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Polyline PDF saved to '{outputPath}'.");
    }
}