using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Paths for the generated PDF
        const string outputPath = "RectangleOpacity.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Create a Graph container (size matches the page)
            Aspose.Pdf.Drawing.Graph graph = new Aspose.Pdf.Drawing.Graph(page.PageInfo.Width, page.PageInfo.Height);

            // Define a rectangle shape (position and size)
            // Constructor: left, bottom, width, height
            Aspose.Pdf.Drawing.Rectangle rectShape = new Aspose.Pdf.Drawing.Rectangle(100, 500, 200, 100);

            // Desired fill color: semi‑transparent red (50% opacity)
            // Alpha = 128 (0‑255 range), Red = 255, Green = 0, Blue = 0
            Aspose.Pdf.Color semiTransparentRed = Aspose.Pdf.Color.FromArgb(128, 255, 0, 0);

            // Apply visual properties via GraphInfo
            rectShape.GraphInfo = new Aspose.Pdf.GraphInfo
            {
                FillColor = semiTransparentRed,   // Fill with 50 % opacity
                Color = Aspose.Pdf.Color.Black,  // Stroke color (optional)
                LineWidth = 1                     // Stroke width (optional)
            };

            // Add the rectangle to the graph
            graph.Shapes.Add(rectShape);

            // Add the graph to the page's content
            page.Paragraphs.Add(graph);

            // ---- Verification ----
            // The rectangle's GraphInfo.FillColor should match the ARGB value we set.
            bool fillMatches = rectShape.GraphInfo.FillColor.Equals(semiTransparentRed);
            Console.WriteLine($"Fill opacity set correctly: {fillMatches}");

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}