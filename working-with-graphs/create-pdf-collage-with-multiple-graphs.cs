using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "collage.pdf";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a single page to host the collage
            Page page = doc.Pages.Add();

            // -------------------------------------------------
            // First graph: a light‑gray rectangle with a black border
            // -------------------------------------------------
            Graph rectGraph = new Graph(200.0, 100.0); // width, height (double)
            rectGraph.Left = 50;                     // X position on page
            rectGraph.Top = 700;                     // Y position on page (from bottom)

            var rectShape = new Aspose.Pdf.Drawing.Rectangle(0f, 0f, 200f, 100f);
            rectShape.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.LightGray,
                Color = Aspose.Pdf.Color.Black,
                LineWidth = 2f
            };
            rectGraph.Shapes.Add(rectShape);
            page.Paragraphs.Add(rectGraph);

            // -------------------------------------------------
            // Second graph: a yellow ellipse with a red outline
            // -------------------------------------------------
            Graph ellipseGraph = new Graph(150.0, 150.0);
            ellipseGraph.Left = 300;
            ellipseGraph.Top = 600;

            var ellipseShape = new Ellipse(0f, 0f, 150f, 150f);
            ellipseShape.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.Yellow,
                Color = Aspose.Pdf.Color.Red,
                LineWidth = 1.5f
            };
            ellipseGraph.Shapes.Add(ellipseShape);
            page.Paragraphs.Add(ellipseGraph);

            // -------------------------------------------------
            // Third graph: a thick red line
            // -------------------------------------------------
            Graph lineGraph = new Graph(250.0, 10.0);
            lineGraph.Left = 100;
            lineGraph.Top = 400;

            float[] linePoints = { 0f, 0f, 250f, 0f };
            var lineShape = new Line(linePoints);
            lineShape.GraphInfo = new GraphInfo
            {
                Color = Aspose.Pdf.Color.Red,
                LineWidth = 3f
            };
            lineGraph.Shapes.Add(lineShape);
            page.Paragraphs.Add(lineGraph);

            // -------------------------------------------------
            // Save the resulting PDF collage (guarded for non‑Windows platforms)
            // -------------------------------------------------
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPath);
                Console.WriteLine($"Collage PDF saved to '{outputPath}'.");
            }
            else
            {
                // On macOS/Linux Aspose.Pdf requires libgdiplus for rendering Graph objects.
                // Either install libgdiplus or skip saving the PDF on unsupported platforms.
                Console.WriteLine("libgdiplus is required for Graph rendering on this platform. Skipping PDF save.");
            }
        }
    }
}
