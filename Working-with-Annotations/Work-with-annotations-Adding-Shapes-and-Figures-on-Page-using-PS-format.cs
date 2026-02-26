using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "AnnotatedDocument.ps";

        // Create a new PDF document and add a blank page
        using (Document doc = new Document())
        {
            // Add a new page (first page will be at index 1)
            Page page = doc.Pages.Add();

            // ---------- Add a Polygon Annotation ----------
            // Define polygon vertices
            Point[] polygonVertices = new Point[]
            {
                new Point(100, 700),
                new Point(200, 750),
                new Point(300, 700),
                new Point(250, 600),
                new Point(150, 600)
            };
            // Create a rectangle that roughly bounds the polygon
            Aspose.Pdf.Rectangle polygonRect = new Aspose.Pdf.Rectangle(90, 590, 310, 760);
            // Instantiate the polygon annotation
            PolygonAnnotation polygon = new PolygonAnnotation(page, polygonRect, polygonVertices);
            // Set visual properties
            polygon.Color = Aspose.Pdf.Color.Blue;
            polygon.Border = new Border(polygon) { Width = 2 };
            polygon.Contents = "Sample Polygon";
            // Add to the page's annotation collection
            page.Annotations.Add(polygon);

            // ---------- Add a Square Annotation ----------
            Aspose.Pdf.Rectangle squareRect = new Aspose.Pdf.Rectangle(350, 700, 450, 800);
            SquareAnnotation square = new SquareAnnotation(page, squareRect);
            square.Color = Aspose.Pdf.Color.Red;
            square.Border = new Border(square) { Width = 2 };
            square.Contents = "Sample Square";
            page.Annotations.Add(square);

            // ---------- Add a Stamp Annotation (as a figure) ----------
            Aspose.Pdf.Rectangle stampRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            StampAnnotation stamp = new StampAnnotation(page, stampRect);
            stamp.Color = Aspose.Pdf.Color.Green;
            stamp.Border = new Border(stamp) { Width = 1 };
            stamp.Contents = "Figure Stamp";
            page.Annotations.Add(stamp);

            // Save the document as PostScript (PS) using explicit save options
            // Document.Save(string) without options always writes PDF, so we must pass PsSaveOptions
            var psOptions = new PsSaveOptions(); // PsSaveOptions resides in Aspose.Pdf namespace
            doc.Save(outputPath, psOptions);
        }

        Console.WriteLine($"PDF with annotations saved as PostScript to '{outputPath}'.");
    }
}