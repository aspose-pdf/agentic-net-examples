using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text; // for TextFragment if needed

class Program
{
    static void Main()
    {
        const string outputPath = "graph_output.pdf";

        // Ensure the Document is disposed properly
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Create a Graph with width and height, then position it using Left/Top
            Graph graph = new Graph(400, 300)
            {
                Left = 50,   // X position on the page
                Top = 200    // Y position on the page (from bottom of page)
            };

            // Rectangle with solid fill (red)
            var solidRect = new Aspose.Pdf.Drawing.Rectangle(0, 0, 150, 100);
            solidRect.GraphInfo.FillColor = Color.FromArgb(255, 204, 51, 51); // solid red
            solidRect.GraphInfo.Color = Color.Black; // border color
            graph.Shapes.Add(solidRect);

            // Ellipse with linear gradient fill (green to blue)
            var linearEllipse = new Ellipse(200, 0, 150, 100);
            linearEllipse.GraphInfo.FillColor = new Color
            {
                PatternColorSpace = new GradientAxialShading(Color.Green, Color.Blue)
                {
                    Start = new Point(200, 0),
                    End = new Point(350, 100)
                }
            };
            linearEllipse.GraphInfo.Color = Color.Black; // optional border
            graph.Shapes.Add(linearEllipse);

            // Rectangle with hatch‑like effect (yellow background with semi‑transparent black overlay)
            var hatchRect = new Aspose.Pdf.Drawing.Rectangle(0, 150, 150, 100);
            // Base fill – solid yellow
            hatchRect.GraphInfo.FillColor = Color.FromArgb(255, 255, 255, 0);
            // Overlay a semi‑transparent black rectangle to simulate a hatch pattern
            var hatchOverlay = new Aspose.Pdf.Drawing.Rectangle(0, 150, 150, 100);
            hatchOverlay.GraphInfo.FillColor = Color.FromArgb(100, 0, 0, 0); // 100/255 opacity black
            hatchOverlay.GraphInfo.Color = Color.Transparent; // no border for overlay
            // Add overlay first so it appears on top of the base rectangle
            graph.Shapes.Add(hatchRect);
            graph.Shapes.Add(hatchOverlay);

            // Ellipse with radial gradient fill (yellow to blue)
            var radialEllipse = new Ellipse(200, 150, 150, 100);
            radialEllipse.GraphInfo.FillColor = new Color
            {
                PatternColorSpace = new GradientRadialShading(Color.Yellow, Color.Blue)
                {
                    Start = new Point(275, 200), // centre of the ellipse
                    End = new Point(275, 200),
                    StartingRadius = 0,
                    EndingRadius = 75 // half of the smaller dimension (width/2)
                }
            };
            radialEllipse.GraphInfo.Color = Color.Black;
            graph.Shapes.Add(radialEllipse);

            // Add the completed graph to the page
            page.Paragraphs.Add(graph);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with graph saved to '{outputPath}'.");
    }
}
