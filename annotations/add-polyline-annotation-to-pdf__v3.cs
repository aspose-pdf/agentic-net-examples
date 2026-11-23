using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text; // for Point

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "BoundaryPolyline.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure there is at least one page
            if (doc.Pages.Count == 0)
            {
                Console.Error.WriteLine("The document has no pages.");
                return;
            }

            // Work with the first page (adjust index as needed)
            Page page = doc.Pages[1];

            // Define the rectangle that bounds the polyline annotation
            // (coordinates are in points; lower‑left (llx,lly) to upper‑right (urx,ury))
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 400, 400, 600);

            // Define the polyline vertices
            Point[] vertices = new Point[]
            {
                new Point(120, 450),
                new Point(250, 550),
                new Point(380, 450)
            };

            // Create the polyline annotation – the constructor requires the vertices array
            PolylineAnnotation polyline = new PolylineAnnotation(page, rect, vertices)
            {
                Title = "Boundary",
                // Stroke color of the polyline
                Color = Aspose.Pdf.Color.FromRgb(0.6, 0.4, 0.2)
            };

            // Set the line width (border thickness) via the Border property.
            // Border requires the parent annotation in its constructor and does not expose a Color property.
            polyline.Border = new Border(polyline) { Width = 2 };

            // Add the annotation to the page
            page.Annotations.Add(polyline);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved annotated PDF to '{outputPath}'.");
    }
}
