using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Output PDF path
        const string outputPath = "ProportionalGraphs.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Define number of pages to generate
            const int pageCount = 3;

            for (int i = 1; i <= pageCount; i++)
            {
                // Add a new page
                Page page = doc.Pages.Add();

                // Get page dimensions (width and height in points)
                // Aspose.Pdf.Rectangle represents the page media box
                Aspose.Pdf.Rectangle pageRect = page.Rect;
                double pageWidth = pageRect.URX - pageRect.LLX;
                double pageHeight = pageRect.URY - pageRect.LLY;

                // Create a Graph that fills the whole page
                Graph graph = new Graph(pageWidth, pageHeight);

                // Optional: set a border for the graph (visible around the page)
                // The Border class is not available in all Aspose.Pdf versions, so this step is omitted.
                // If a Border class is present, you could enable it like this:
                // graph.Border = new Border(graph) { Width = 1, Color = Color.Gray };

                // -------------------------
                // Add a rectangle (50% of page width, 30% of page height)
                // Use Aspose.Pdf.Drawing.Rectangle for shape (not Aspose.Pdf.Rectangle)
                double rectWidth = pageWidth * 0.5;
                double rectHeight = pageHeight * 0.3;
                var rect = new Aspose.Pdf.Drawing.Rectangle(0f, 0f, (float)rectWidth, (float)rectHeight)
                {
                    GraphInfo = new GraphInfo
                    {
                        FillColor = Color.LightBlue,
                        Color = Color.DarkBlue,
                        LineWidth = 2f
                    }
                };
                graph.Shapes.Add(rect);

                // -------------------------
                // Add an ellipse (30% of page width, 20% of page height) positioned next to the rectangle
                double ellipseWidth = pageWidth * 0.3;
                double ellipseHeight = pageHeight * 0.2;
                var ellipse = new Ellipse((float)(rectWidth + 20), 0f, (float)ellipseWidth, (float)ellipseHeight)
                {
                    GraphInfo = new GraphInfo
                    {
                        FillColor = Color.LightGreen,
                        Color = Color.Green,
                        LineWidth = 1.5f
                    }
                };
                graph.Shapes.Add(ellipse);

                // -------------------------
                // Add a line spanning the width of the page at 70% of page height
                float[] linePoints = new float[]
                {
                    0f,
                    (float)(pageHeight * 0.7),
                    (float)pageWidth,
                    (float)(pageHeight * 0.7)
                };
                var line = new Line(linePoints)
                {
                    GraphInfo = new GraphInfo
                    {
                        Color = Color.Red,
                        LineWidth = 2f
                    }
                };
                graph.Shapes.Add(line);

                // Add the graph to the page's paragraphs collection
                page.Paragraphs.Add(graph);
            }

            // Save the document (PDF format)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with proportional graphs saved to '{outputPath}'.");
    }
}
