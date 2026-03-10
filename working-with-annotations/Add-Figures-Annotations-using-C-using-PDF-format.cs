using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "figures_annotated.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Ensure we have at least one page (page indexing is 1‑based)
            if (doc.Pages.Count < 1)
            {
                Console.Error.WriteLine("Document contains no pages.");
                return;
            }

            // Work with the first page
            Page page = doc.Pages[1];

            // ---------- Square Annotation ----------
            // Rectangle coordinates: llx, lly, urx, ury (in points)
            Rectangle squareRect = new Rectangle(100, 500, 200, 600);
            SquareAnnotation square = new SquareAnnotation(page, squareRect)
            {
                Color = Color.Blue,               // border color
                InteriorColor = Color.LightBlue, // fill color
                Contents = "Square Figure Annotation",
                Title = "Square"
            };
            page.Annotations.Add(square);

            // ---------- Circle Annotation ----------
            Rectangle circleRect = new Rectangle(250, 500, 350, 600);
            CircleAnnotation circle = new CircleAnnotation(page, circleRect)
            {
                Color = Color.Green,
                InteriorColor = Color.LightGreen,
                Contents = "Circle Figure Annotation",
                Title = "Circle"
            };
            page.Annotations.Add(circle);

            // ---------- Polygon Annotation ----------
            // Define the vertices of the polygon (absolute page coordinates)
            Point[] polygonVertices = new Point[]
            {
                new Point(120, 380),
                new Point(180, 420),
                new Point(260, 380),
                new Point(200, 340)
            };
            // The constructor expects a Page, a bounding rectangle and the vertices array.
            Rectangle polyRect = new Rectangle(100, 350, 300, 450);
            PolygonAnnotation polygon = new PolygonAnnotation(page, polyRect, polygonVertices)
            {
                Color = Color.Red,
                InteriorColor = Color.Pink,
                Contents = "Polygon Figure Annotation",
                Title = "Polygon"
            };
            page.Annotations.Add(polygon);

            // ---------- Polyline Annotation ----------
            Point[] polylineVertices = new Point[]
            {
                new Point(360, 380),
                new Point(420, 420),
                new Point(480, 380),
                new Point(540, 420)
            };
            Rectangle polylineRect = new Rectangle(350, 350, 550, 450);
            // The constructor also expects a Page, not a Document.
            PolylineAnnotation polyline = new PolylineAnnotation(page, polylineRect, polylineVertices)
            {
                Color = Color.Orange,
                Contents = "Polyline Figure Annotation",
                Title = "Polyline"
            };
            page.Annotations.Add(polyline);

            // Save the modified PDF (PDF format – no SaveOptions needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Figure annotations added and saved to '{outputPath}'.");
    }
}
