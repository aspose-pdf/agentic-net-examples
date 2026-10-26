using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "graph.pdf";

        // Create a new PDF document inside a using block for deterministic disposal.
        using (Document doc = new Document())
        {
            const int pageCount = 3;

            for (int i = 1; i <= pageCount; i++)
            {
                // Add a new page.
                Page page = doc.Pages.Add();

                // Retrieve page dimensions (points). 1 point = 1/72 inch.
                float pageWidth  = (float)page.PageInfo.Width;
                float pageHeight = (float)page.PageInfo.Height;

                // -------------------------------------------------
                // Create a Graph container – all drawing shapes must be added to a Graph.
                // -------------------------------------------------
                Graph graph = new Graph(pageWidth, pageHeight);

                // -------------------------------------------------
                // Draw a centered rectangle (60% width, 40% height)
                // -------------------------------------------------
                float rectWidth  = pageWidth  * 0.6f;
                float rectHeight = pageHeight * 0.4f;
                float rectX      = (pageWidth  - rectWidth)  / 2f;
                float rectY      = (pageHeight - rectHeight) / 2f;

                Aspose.Pdf.Drawing.Rectangle rectShape = new Aspose.Pdf.Drawing.Rectangle(
                    rectX,          // left (lower‑left X)
                    rectY,          // bottom (lower‑left Y)
                    rectWidth,      // width
                    rectHeight);   // height
                rectShape.GraphInfo.FillColor = Aspose.Pdf.Color.Transparent;
                rectShape.GraphInfo.Color = Aspose.Pdf.Color.Blue; // border color
                rectShape.GraphInfo.LineWidth = 2f;
                graph.Shapes.Add(rectShape);

                // -------------------------------------------------
                // Draw a centered circle (radius = 20% of min dimension)
                // -------------------------------------------------
                float radius = Math.Min(pageWidth, pageHeight) * 0.20f;
                float centerX = pageWidth  / 2f;
                float centerY = pageHeight / 2f;

                // Ellipse constructor: left, bottom, width, height
                Ellipse circle = new Ellipse(
                    centerX - radius,
                    centerY - radius,
                    radius * 2f,
                    radius * 2f);
                circle.GraphInfo.FillColor = Aspose.Pdf.Color.Transparent;
                circle.GraphInfo.Color = Aspose.Pdf.Color.Red;
                circle.GraphInfo.LineWidth = 2f;
                graph.Shapes.Add(circle);

                // -------------------------------------------------
                // Draw a horizontal line across the middle of the page
                // -------------------------------------------------
                float lineY = pageHeight / 2f;
                Line hLine = new Line(new float[] { 0f, lineY, pageWidth, lineY });
                hLine.GraphInfo.Color = Aspose.Pdf.Color.Green;
                hLine.GraphInfo.LineWidth = 1.5f;
                graph.Shapes.Add(hLine);

                // -------------------------------------------------
                // Draw a vertical line at 25% width
                // -------------------------------------------------
                float lineX = pageWidth * 0.25f;
                Line vLine = new Line(new float[] { lineX, 0f, lineX, pageHeight });
                vLine.GraphInfo.Color = Aspose.Pdf.Color.Orange;
                vLine.GraphInfo.LineWidth = 1.5f;
                graph.Shapes.Add(vLine);

                // Add the graph (which now contains all shapes) to the page.
                page.Paragraphs.Add(graph);
            }

            // Save the document as PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with proportional graphs saved to '{outputPath}'.");
    }
}
