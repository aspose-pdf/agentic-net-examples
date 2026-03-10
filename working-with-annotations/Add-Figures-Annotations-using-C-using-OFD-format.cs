using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputOfd = "input.ofd";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputOfd))
        {
            Console.Error.WriteLine($"File not found: {inputOfd}");
            return;
        }

        // Load OFD (input‑only format) using OfdLoadOptions
        using (Document doc = new Document(inputOfd, new OfdLoadOptions()))
        {
            // Ensure the document has at least one page
            if (doc.Pages.Count == 0)
            {
                Console.Error.WriteLine("The document contains no pages.");
                return;
            }

            // Use the first page for demonstration
            Page page = doc.Pages[1];

            // ----- Square Annotation -----
            Rectangle squareRect = new Rectangle(100, 500, 200, 600);
            SquareAnnotation square = new SquareAnnotation(page, squareRect)
            {
                Color = Color.Blue,
                Contents = "Square figure",
                Title = "Square"
            };
            page.Annotations.Add(square);

            // ----- Circle Annotation -----
            Rectangle circleRect = new Rectangle(250, 500, 350, 600);
            CircleAnnotation circle = new CircleAnnotation(page, circleRect)
            {
                Color = Color.Green,
                Contents = "Circle figure",
                Title = "Circle"
            };
            page.Annotations.Add(circle);

            // ----- Polygon Annotation (triangle) -----
            // Define vertices of the triangle
            Point[] polygonVertices = new Point[]
            {
                new Point(400, 500),
                new Point(450, 600),
                new Point(350, 600)
            };
            // Bounding rectangle that contains the vertices
            Rectangle polygonRect = new Rectangle(350, 500, 450, 600);
            PolygonAnnotation polygon = new PolygonAnnotation(page, polygonRect, polygonVertices)
            {
                Color = Color.Red,
                Contents = "Polygon figure",
                Title = "Polygon"
            };
            page.Annotations.Add(polygon);

            // ----- Polyline Annotation (open shape) -----
            Point[] polylineVertices = new Point[]
            {
                new Point(500, 500),
                new Point(550, 550),
                new Point(600, 500)
            };
            // Bounding rectangle for the polyline
            Rectangle polylineRect = new Rectangle(500, 500, 600, 550);
            PolylineAnnotation polyline = new PolylineAnnotation(page, polylineRect, polylineVertices)
            {
                Color = Color.Orange,
                Contents = "Polyline figure",
                Title = "Polyline"
            };
            page.Annotations.Add(polyline);

            // OFD cannot be saved; export as PDF instead
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Figure annotations added and saved to '{outputPdf}'.");
    }
}
