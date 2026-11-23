using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "StarPolygon.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least 13 pages (1‑based indexing)
            if (doc.Pages.Count < 13)
            {
                Console.Error.WriteLine("The document does not contain page 13.");
                return;
            }

            // Get page 13
            Page page = doc.Pages[13];

            // Define the vertices of a simple 5‑point star.
            Point[] starVertices = new Point[]
            {
                new Point(200, 700), // top point
                new Point(230, 620),
                new Point(310, 620),
                new Point(245, 570),
                new Point(270, 490),
                new Point(200, 540),
                new Point(130, 490),
                new Point(155, 570),
                new Point(90, 620),
                new Point(170, 620)
            };

            // Create the polygon annotation using the Document‑based constructor.
            // The annotation will be added to the desired page later.
            PolygonAnnotation star = new PolygonAnnotation(doc, starVertices);

            // Set visual appearance
            star.InteriorColor = Color.FromRgb(1.0, 0.84, 0.0); // gold fill
            star.Color = Color.Black;                         // border colour
            star.Border = new Border(star) { Width = 1 };

            // Add the annotation to page 13
            page.Annotations.Add(star);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Star polygon added and saved to '{outputPath}'.");
    }
}
