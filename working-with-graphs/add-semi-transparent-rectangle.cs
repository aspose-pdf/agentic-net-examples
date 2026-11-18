using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

// Alias the drawing Rectangle to avoid confusion with Aspose.Pdf.Rectangle (used for page geometry)
using DrawRect = Aspose.Pdf.Drawing.Rectangle;

class Program
{
    static void Main()
    {
        const string outputPath = "gradient_rectangle.pdf";

        // Ensure the output directory exists (disambiguate System.IO.Path)
        string outputDir = System.IO.Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
            Directory.CreateDirectory(outputDir);

        // Create a new PDF document
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Create a Graph container – it is a BaseParagraph that holds drawing shapes
            // Use the page size so the rectangle appears in page coordinates
            var graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

            // Define the rectangle (left, bottom, width, height)
            var rect = new DrawRect(100, 300, 200, 200);

            // Linear gradient from fully transparent black to fully opaque black
            var gradient = new GradientAxialShading(
                Color.FromArgb(0, 0, 0, 0),   // start: transparent black (alpha = 0)
                Color.FromArgb(255, 0, 0, 0) // end:   opaque black   (alpha = 255)
            )
            {
                // Gradient direction – from the lower‑left corner of the rectangle to the upper‑right
                Start = new Point(100, 300),
                End   = new Point(300, 500)
            };

            // Apply the gradient as the fill pattern
            rect.GraphInfo.FillColor = new Color { PatternColorSpace = gradient };
            // Optional: set a stroke (border) color and width
            rect.GraphInfo.Color = Color.Black;
            rect.GraphInfo.LineWidth = 1f;

            // Add the rectangle to the graph and the graph to the page
            graph.Shapes.Add(rect);
            page.Paragraphs.Add(graph);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with gradient rectangle saved to '{outputPath}'.");
    }
}
