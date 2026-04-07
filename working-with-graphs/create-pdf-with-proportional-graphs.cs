using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "graph_pages.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            int pageCount = 3;

            for (int i = 1; i <= pageCount; i++)
            {
                // Add a new page (default size is A4)
                Page page = doc.Pages.Add();

                // Get page dimensions (points)
                Aspose.Pdf.Rectangle pageRect = page.Rect;
                double pageWidth = pageRect.Width;
                double pageHeight = pageRect.Height;

                // Define graph size as a proportion of the page size
                double graphWidth = pageWidth * 0.6;   // 60% of page width
                double graphHeight = pageHeight * 0.4; // 40% of page height

                // Position the graph roughly centered
                double graphLeft = (pageWidth - graphWidth) / 2;
                double graphTop = (pageHeight - graphHeight) / 2;

                // Create the graph (Graph constructor expects double values)
                Graph graph = new Graph(graphWidth, graphHeight)
                {
                    Left = graphLeft,
                    Top = graphTop
                };

                // ----- Add a rectangle shape -----
                // Use Aspose.Pdf.Drawing.Rectangle (float parameters)
                var rect = new Aspose.Pdf.Drawing.Rectangle(
                    0f,
                    0f,
                    (float)(graphWidth * 0.8),
                    (float)(graphHeight * 0.5));
                rect.GraphInfo = new GraphInfo
                {
                    FillColor = Color.LightGray,
                    Color = Color.Black,
                    LineWidth = 1f
                };
                graph.Shapes.Add(rect);

                // ----- Add an ellipse shape -----
                var ellipse = new Aspose.Pdf.Drawing.Ellipse(
                    (float)(graphWidth * 0.1),
                    (float)(graphHeight * 0.2),
                    (float)(graphWidth * 0.4),
                    (float)(graphHeight * 0.3));
                ellipse.GraphInfo = new GraphInfo
                {
                    FillColor = Color.Yellow,
                    Color = Color.Red,
                    LineWidth = 1.5f
                };
                graph.Shapes.Add(ellipse);

                // ----- Add a line shape -----
                float[] linePoints = new float[]
                {
                    0f, 0f,
                    (float)graphWidth, (float)graphHeight
                };
                var line = new Aspose.Pdf.Drawing.Line(linePoints);
                line.GraphInfo = new GraphInfo
                {
                    Color = Color.Red,
                    LineWidth = 2f
                };
                graph.Shapes.Add(line);

                // Add the graph to the page's paragraph collection
                page.Paragraphs.Add(graph);
            }

            // Save the document (no SaveOptions needed for PDF output)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with graphs saved to '{outputPath}'.");
    }
}
