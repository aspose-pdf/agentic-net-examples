using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "GraphicsExample.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page (1‑based indexing)
            Page page = doc.Pages.Add();

            // macOS (or Linux) does not have GDI+ by default. Graph rendering requires libgdiplus.
            // If the current OS is macOS we skip the Graph creation to avoid a TypeInitializationException.
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX) || RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                Console.WriteLine("libgdiplus is required for Graph rendering on this platform; saving PDF without graphic objects.");
                doc.Save(outputPath);
                return;
            }

            // Create a Graph container (width, height) – use double literals as the constructor now expects double
            Graph graph = new Graph(400.0, 200.0);

            // ----- Rectangle shape -----
            // Constructor: left, bottom, width, height (float literals)
            Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(50.0F, 100.0F, 200.0F, 100.0F);
            rect.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.LightGray,
                Color = Aspose.Pdf.Color.Black,
                LineWidth = 2
            };
            graph.Shapes.Add(rect);

            // ----- Circle shape (drawn as an Ellipse with equal width/height) -----
            Aspose.Pdf.Drawing.Ellipse circle = new Aspose.Pdf.Drawing.Ellipse(300.0F, 100.0F, 100.0F, 100.0F);
            circle.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.Yellow,
                Color = Aspose.Pdf.Color.Red,
                LineWidth = 1.5F
            };
            graph.Shapes.Add(circle);

            // ----- Line shape -----
            // Constructor takes a float array { x1, y1, x2, y2 }
            float[] linePoints = new float[] { 100.0F, 300.0F, 350.0F, 350.0F };
            Line line = new Line(linePoints);
            line.GraphInfo = new GraphInfo
            {
                Color = Aspose.Pdf.Color.Blue,
                LineWidth = 3
            };
            graph.Shapes.Add(line);

            // Add the Graph (which contains all shapes) to the page
            page.Paragraphs.Add(graph);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine("PDF with graphic objects created successfully.");
    }
}
