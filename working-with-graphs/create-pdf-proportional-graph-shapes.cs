using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Output PDF path
        const string outputPath = "graph_pages.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Define number of pages to generate
            int pageCount = 3;

            for (int i = 1; i <= pageCount; i++)
            {
                // Add a new blank page (A4 size by default)
                Page page = doc.Pages.Add();

                // Get page dimensions (width and height in points)
                // Aspose.Pdf.Rectangle is used for page coordinates
                Aspose.Pdf.Rectangle pageRect = page.Rect;
                double pageWidth  = pageRect.URX - pageRect.LLX;
                double pageHeight = pageRect.URY - pageRect.LLY;

                // Create a Graph that fills the whole page
                Graph graph = new Graph(pageWidth, pageHeight)
                {
                    // Optional: set a border around the graph (visible for debugging)
                    Border = new BorderInfo(BorderSide.All, 0.5f, Color.Gray)
                };

                // Example shape: a rectangle that occupies 50% width and 30% height,
                // positioned at 10% from left and 10% from the bottom of the page
                double rectLeft   = pageWidth * 0.10;
                double rectBottom = pageHeight * 0.10;
                double rectWidth  = pageWidth * 0.50;
                double rectHeight = pageHeight * 0.30;
                var rectShape = new Aspose.Pdf.Drawing.Rectangle(
                    (float)rectLeft,
                    (float)rectBottom,
                    (float)rectWidth,
                    (float)rectHeight);
                rectShape.GraphInfo = new GraphInfo
                {
                    FillColor = Color.LightBlue,
                    Color = Color.DarkBlue,
                    LineWidth = 1f
                };
                graph.Shapes.Add(rectShape);

                // Example shape: an ellipse that occupies 30% width and 20% height,
                // positioned at 60% from left and 60% from the bottom
                double ellipseLeft   = pageWidth * 0.60;
                double ellipseBottom = pageHeight * 0.60;
                double ellipseWidth  = pageWidth * 0.30;
                double ellipseHeight = pageHeight * 0.20;
                var ellipse = new Ellipse(
                    (float)ellipseLeft,
                    (float)ellipseBottom,
                    (float)ellipseWidth,
                    (float)ellipseHeight);
                ellipse.GraphInfo = new GraphInfo
                {
                    FillColor = Color.Yellow,
                    Color = Color.OrangeRed,
                    LineWidth = 1.5f
                };
                graph.Shapes.Add(ellipse);

                // Example shape: a diagonal line across the page
                float[] linePoints =
                {
                    (float)pageRect.LLX, (float)pageRect.LLY, // start at lower‑left corner
                    (float)pageRect.URX, (float)pageRect.URY  // end at upper‑right corner
                };
                var line = new Line(linePoints);
                line.GraphInfo = new GraphInfo
                {
                    Color = Color.Green,
                    LineWidth = 2f
                };
                graph.Shapes.Add(line);

                // Add the graph to the page's paragraph collection
                page.Paragraphs.Add(graph);
            }

            // Save the document (PDF format)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with graphs saved to '{outputPath}'.");
    }
}
