using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Input and output file paths
        const string outputPath = "graphics_output.pdf";

        // Create a new PDF document inside a using block for proper disposal
        using (Document doc = new Document())
        {
            // Add a blank page (1‑based indexing)
            Page page = doc.Pages.Add();

            // -----------------------------------------------------------------
            // macOS specific handling – Graph rendering requires libgdiplus.
            // If the application runs on macOS and libgdiplus is not present,
            // skip the Graph creation to avoid System.Drawing.Gdip init failure.
            // -----------------------------------------------------------------
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                Console.WriteLine("libgdiplus is required for Graph rendering on macOS; saving PDF without graphic shapes.");
                doc.Save(outputPath);
                return;
            }

            // Create a Graph container – this holds vector shapes
            // Width and height define the drawing area (in points)
            Graph graph = new Graph(500.0, 400.0); // use double literals as required by the API

            // ---------- Aspose.Pdf.Drawing.Rectangle ----------
            // Parameters: left, bottom, width, height (float values)
            Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(50.0F, 300.0F, 200.0F, 100.0F);
            rect.GraphInfo = new GraphInfo
            {
                FillColor = Color.LightGray,
                Color = Color.Black,
                LineWidth = 2
            };
            graph.Shapes.Add(rect);

            // ---------- Ellipse ----------
            // Parameters: left, bottom, width, height (float values)
            Ellipse ellipse = new Ellipse(300.0F, 300.0F, 150.0F, 100.0F);
            ellipse.GraphInfo = new GraphInfo
            {
                FillColor = Color.Yellow,
                Color = Color.Red,
                LineWidth = 1.5F
            };
            graph.Shapes.Add(ellipse);

            // ---------- Line ----------
            // Float array: { x1, y1, x2, y2 }
            float[] linePoints = { 100.0F, 200.0F, 400.0F, 150.0F };
            Line line = new Line(linePoints);
            line.GraphInfo = new GraphInfo
            {
                Color = Color.Blue,
                LineWidth = 2
            };
            graph.Shapes.Add(line);

            // Add the Graph (which contains all shapes) to the page
            page.Paragraphs.Add(graph);

            // Save the document (PDF format)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with graphics saved to '{outputPath}'.");
    }
}
