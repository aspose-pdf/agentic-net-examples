using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "graph_pages.pdf";
        const int pageCount = 3; // number of pages to create

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            for (int i = 1; i <= pageCount; i++)
            {
                // Add a new page (default A4 size)
                Page page = doc.Pages.Add();

                // Determine page dimensions (points)
                double pageWidth  = page.Rect.URX - page.Rect.LLX;
                double pageHeight = page.Rect.URY - page.Rect.LLY;

                // Create a Graph that occupies the whole page (Graph constructor uses double)
                Graph graph = new Graph(pageWidth, pageHeight);

                // ---------- Rectangle (80% width, 30% height, centered horizontally, 10% from bottom) ----------
                double rectWidth  = pageWidth  * 0.8;
                double rectHeight = pageHeight * 0.3;
                double rectX      = (pageWidth - rectWidth) / 2;
                double rectY      = pageHeight * 0.1; // distance from bottom

                var rect = new Aspose.Pdf.Drawing.Rectangle(
                    (float)rectX,
                    (float)rectY,
                    (float)rectWidth,
                    (float)rectHeight);
                rect.GraphInfo = new GraphInfo
                {
                    FillColor = Color.LightGray,
                    Color     = Color.Black,
                    LineWidth = 1f
                };
                graph.Shapes.Add(rect);

                // ---------- Ellipse (50% width, 20% height, centered horizontally, positioned at 50% height) ----------
                double ellipseWidth  = pageWidth  * 0.5;
                double ellipseHeight = pageHeight * 0.2;
                double ellipseX      = (pageWidth - ellipseWidth) / 2;
                double ellipseY      = pageHeight * 0.5;

                var ellipse = new Ellipse(
                    (float)ellipseX,
                    (float)ellipseY,
                    (float)ellipseWidth,
                    (float)ellipseHeight);
                ellipse.GraphInfo = new GraphInfo
                {
                    FillColor = Color.Yellow,
                    Color     = Color.Red,
                    LineWidth = 1.5f
                };
                graph.Shapes.Add(ellipse);

                // ---------- Diagonal Line (from 10% to 90% of page dimensions) ----------
                float[] linePoints = new float[]
                {
                    (float)(pageWidth  * 0.1), (float)(pageHeight * 0.1),
                    (float)(pageWidth  * 0.9), (float)(pageHeight * 0.9)
                };
                var line = new Line(linePoints);
                line.GraphInfo = new GraphInfo
                {
                    Color     = Color.Blue,
                    LineWidth = 2f
                };
                graph.Shapes.Add(line);

                // Add the graph to the page's paragraph collection
                page.Paragraphs.Add(graph);
            }

            // Save the document as PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with graphs saved to '{outputPath}'.");
    }
}