using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Drawing; // for Color, Point

// Alias the Rectangle type that is required by the annotation constructors.
using PdfRect = Aspose.Pdf.Rectangle;

class Program
{
    static void Main()
    {
        // Paths for the input MHT file and the output PDF file.
        const string inputMhtPath = "input.mht";
        const string outputPdfPath = "output.pdf";

        if (!File.Exists(inputMhtPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputMhtPath}");
            return;
        }

        // Load the MHT file into a PDF document using MhtLoadOptions.
        using (Document pdfDoc = new Document(inputMhtPath, new MhtLoadOptions()))
        {
            // Ensure the document has at least one page.
            if (pdfDoc.Pages.Count == 0)
            {
                Console.Error.WriteLine("The loaded document contains no pages.");
                return;
            }

            // Work with the first page (Aspose.Pdf uses 1‑based indexing).
            Page page = pdfDoc.Pages[1];

            // -----------------------------------------------------------------
            // Add a square figure annotation.
            // -----------------------------------------------------------------
            // Define the rectangle that bounds the square.
            PdfRect squareRect = new PdfRect(100, 500, 300, 550);
            // SquareAnnotation expects a Page as the first argument.
            SquareAnnotation square = new SquareAnnotation(page, squareRect)
            {
                Color = Color.Red,          // Border color of the square.
                Contents = "Square figure annotation"   // Tooltip text.
            };
            // Add the annotation to the page's annotation collection.
            page.Annotations.Add(square);

            // -----------------------------------------------------------------
            // Add a circle figure annotation.
            // -----------------------------------------------------------------
            PdfRect circleRect = new PdfRect(350, 500, 450, 600);
            CircleAnnotation circle = new CircleAnnotation(page, circleRect)
            {
                Color = Color.Blue,
                Contents = "Circle figure annotation"
            };
            page.Annotations.Add(circle);

            // -----------------------------------------------------------------
            // Add a polygon figure annotation (example with a triangle).
            // -----------------------------------------------------------------
            // Define the polygon vertices as an array of Point objects.
            Point[] polygonVertices = new Point[]
            {
                new Point(500, 500),
                new Point(550, 600),
                new Point(600, 500)
            };
            // PolygonAnnotation constructor expects a Document and the vertices.
            PolygonAnnotation polygon = new PolygonAnnotation(pdfDoc, polygonVertices)
            {
                Color = Color.Green,
                Contents = "Polygon (triangle) figure annotation",
                // Set the bounding rectangle for the annotation.
                Rect = new PdfRect(500, 500, 600, 600)
            };
            page.Annotations.Add(polygon);

            // Save the modified document as a PDF.
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF with figure annotations saved to '{outputPdfPath}'.");
    }
}
