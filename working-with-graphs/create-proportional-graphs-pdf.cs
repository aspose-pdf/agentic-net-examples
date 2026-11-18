using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            int totalPages = 3; // Number of pages with graphs

            // Generate each page
            for (int i = 1; i <= totalPages; i++)
            {
                // Add a new page (pages are 1‑based)
                Page page = doc.Pages.Add();

                // Retrieve page dimensions (points)
                double pageWidth = page.PageInfo.Width;
                double pageHeight = page.PageInfo.Height;

                // Define a margin proportional to page size (10% of width)
                double margin = pageWidth * 0.10;

                // -------------------------------------------------
                // Create a Graph container that will hold all drawing shapes
                // -------------------------------------------------
                Graph graph = new Graph(pageWidth, pageHeight);

                // -------------------------------------------------
                // Draw X‑axis line
                // -------------------------------------------------
                Line xAxis = new Line(new float[] {
                    (float)margin,
                    (float)margin,
                    (float)(pageWidth - margin),
                    (float)margin
                });
                xAxis.GraphInfo.Color = Aspose.Pdf.Color.Black;
                graph.Shapes.Add(xAxis);

                // -------------------------------------------------
                // Draw Y‑axis line
                // -------------------------------------------------
                Line yAxis = new Line(new float[] {
                    (float)margin,
                    (float)margin,
                    (float)margin,
                    (float)(pageHeight - margin)
                });
                yAxis.GraphInfo.Color = Aspose.Pdf.Color.Black;
                graph.Shapes.Add(yAxis);

                // -------------------------------------------------
                // Draw a rectangle (e.g., a bar) sized proportionally
                // -------------------------------------------------
                double barWidth = pageWidth * 0.20;   // 20% of page width
                double barHeight = pageHeight * 0.30; // 30% of page height
                double barX = margin + (pageWidth - 2 * margin - barWidth) / 2;
                double barY = margin; // start at the X‑axis

                Aspose.Pdf.Drawing.Rectangle bar = new Aspose.Pdf.Drawing.Rectangle(
                    (float)barX,
                    (float)barY,
                    (float)barWidth,
                    (float)barHeight);
                bar.GraphInfo.FillColor = Aspose.Pdf.Color.LightBlue;
                bar.GraphInfo.Color = Aspose.Pdf.Color.DarkBlue;
                graph.Shapes.Add(bar);

                // -------------------------------------------------
                // Draw a circle (ellipse) sized proportionally
                // -------------------------------------------------
                double radius = Math.Min(pageWidth, pageHeight) * 0.10; // 10% of smaller dimension
                double cx = pageWidth - margin - radius; // center X
                double cy = pageHeight - margin - radius; // center Y

                Ellipse circle = new Ellipse(
                    (float)(cx - radius),
                    (float)(cy - radius),
                    (float)(cx + radius),
                    (float)(cy + radius));
                circle.GraphInfo.FillColor = Aspose.Pdf.Color.LightGreen;
                circle.GraphInfo.Color = Aspose.Pdf.Color.DarkGreen;
                graph.Shapes.Add(circle);

                // Add the completed graph to the page
                page.Paragraphs.Add(graph);
            }

            // Save the resulting PDF
            doc.Save("graph.pdf");
        }

        Console.WriteLine("PDF with proportional graphs saved as 'graph.pdf'.");
    }
}
