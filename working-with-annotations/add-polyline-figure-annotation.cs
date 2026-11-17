using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_polyline.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least one page
            if (doc.Pages.Count == 0)
            {
                Console.Error.WriteLine("The PDF does not contain any pages.");
                return;
            }

            // Work with the first page (you can choose any page index, 1‑based)
            Page page = doc.Pages[1];

            // Define the bounding rectangle for the annotation (lower‑left X/Y, upper‑right X/Y)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(
                llx: 80,   // lower‑left X
                lly: 480,  // lower‑left Y
                urx: 320,  // upper‑right X
                ury: 720   // upper‑right Y
            );

            // Define the vertices of the custom shape (a simple triangle, closed)
            Point[] vertices = new Point[]
            {
                new Point(100, 500),
                new Point(200, 700),
                new Point(300, 500),
                new Point(100, 500) // close the shape
            };

            // Create a polyline annotation with the required vertices parameter
            PolylineAnnotation polyline = new PolylineAnnotation(page, rect, vertices);

            // Set visual appearance
            polyline.Color = Color.Blue; // line color
            // Border requires the parent annotation in its constructor
            polyline.Border = new Border(polyline)
            {
                Style = BorderStyle.Solid,
                Width = 2
            };

            // Optional title and contents (tooltip)
            polyline.Title = "Diagram Shape";
            polyline.Contents = "Custom polyline illustrating a diagram.";

            // Add the annotation to the page
            page.Annotations.Add(polyline);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PolyLine annotation added and saved to '{outputPath}'.");
    }
}
