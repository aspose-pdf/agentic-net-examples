using System;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "graph.pdf";

        using (Document doc = new Document())
        {
            // Add a single page (Pages are 1‑based)
            Page page = doc.Pages.Add();

            // Lookup table mapping shape index to border color (RGB values in 0‑1 range)
            var colorLookup = new Dictionary<int, Color>
            {
                { 0, Color.FromRgb(1, 0, 0) }, // Red
                { 1, Color.FromRgb(0, 1, 0) }, // Green
                { 2, Color.FromRgb(0, 0, 1) }, // Blue
                { 3, Color.FromRgb(1, 1, 0) }, // Yellow
                { 4, Color.FromRgb(1, 0, 1) }  // Magenta
            };

            // Create a Graph that spans the whole page. The Graph constructor expects width and height (double).
            Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

            // Draw five circles, each using a border color from the lookup table
            for (int i = 0; i < 5; i++)
            {
                double centerX = 100 + i * 100; // Horizontal spacing
                double centerY = 600;           // Fixed vertical position
                double radius = 30;

                // Ellipse constructor: left, bottom, width, height (all double)
                double left   = centerX - radius;
                double bottom = centerY - radius;
                double width  = radius * 2;
                double height = radius * 2;

                Ellipse ellipse = new Ellipse(left, bottom, width, height)
                {
                    GraphInfo = new GraphInfo
                    {
                        Color = colorLookup[i],          // Border color
                        FillColor = Color.Transparent    // Transparent fill
                    }
                };

                // Add the shape to the graph
                graph.Shapes.Add(ellipse);
            }

            // Attach the graph to the page
            page.Paragraphs.Add(graph);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with graph saved to '{outputPath}'.");
    }
}
