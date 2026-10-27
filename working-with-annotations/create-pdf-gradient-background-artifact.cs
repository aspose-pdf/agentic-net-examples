using System;
using System.IO;
using Aspose.Pdf;                     // Core PDF classes
using Aspose.Pdf.Drawing;            // Graph, Rectangle shape, colors

class Program
{
    static void Main()
    {
        // Output file path
        const string outputPath = "gradient_background.pdf";

        // Ensure the output directory exists
        string outputDir = System.IO.Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
            Directory.CreateDirectory(outputDir);

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a single page (default size A4)
            Page page = doc.Pages.Add();

            // -----------------------------------------------------------------
            // Create a Graph that will act as the background and fill it with a
            // simulated linear gradient (multiple thin rectangles). The Graph is
            // added as the first paragraph so it appears behind any subsequent
            // content.
            // -----------------------------------------------------------------
            double pageWidth = page.PageInfo.Width;
            double pageHeight = page.PageInfo.Height;

            // Graph dimensions must be double (float overload is obsolete)
            var backgroundGraph = new Graph(pageWidth, pageHeight);

            // Gradient definition – start and end colors (RGB values 0‑1)
            const double startR = 0.9, startG = 0.9, startB = 1.0; // Light blue
            const double endR   = 0.5, endG   = 0.5, endB   = 0.8; // Darker blue

            const int steps = 100; // Number of thin rectangles to simulate the gradient
            double stepHeight = pageHeight / steps;

            for (int i = 0; i < steps; i++)
            {
                double t = (double)i / (steps - 1); // interpolation factor 0‑1
                double r = startR + (endR - startR) * t;
                double g = startG + (endG - startG) * t;
                double b = startB + (endB - startB) * t;

                // Create a rectangle that spans the full width and a slice of the height
                var rect = new Aspose.Pdf.Drawing.Rectangle(
                    0f,
                    (float)(i * stepHeight),
                    (float)pageWidth,
                    (float)((i + 1) * stepHeight));

                // Apply the interpolated fill color; no border
                rect.GraphInfo.FillColor = Aspose.Pdf.Color.FromRgb(r, g, b);
                rect.GraphInfo.Color = Aspose.Pdf.Color.Transparent;
                rect.GraphInfo.LineWidth = 0;

                backgroundGraph.Shapes.Add(rect);
            }

            // Insert the graph as the first paragraph so it becomes the page background
            page.Paragraphs.Insert(0, backgroundGraph);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with gradient background saved to '{outputPath}'.");
    }
}
