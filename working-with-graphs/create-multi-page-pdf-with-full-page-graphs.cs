using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing; // Graph, Line, Rectangle, etc.
using Aspose.Pdf.Text;   // TextFragment for titles

class Program
{
    static void Main()
    {
        const string outputPath = "multi_page_graph.pdf";
        const int pageCount = 3;

        // Create a new PDF document.
        using (Document doc = new Document())
        {
            for (int i = 1; i <= pageCount; i++)
            {
                // Add a new page.
                Page page = doc.Pages.Add();

                // Use the page size for the graph so it fills the whole page.
                double pageWidth = page.PageInfo.Width;
                double pageHeight = page.PageInfo.Height;

                // Create a Graph that occupies the full page.
                Graph graph = new Graph(pageWidth, pageHeight)
                {
                    Left = 0,
                    Top = 0,
                    Border = new BorderInfo(BorderSide.All, 1f, Color.Black),
                    Title = new TextFragment($"Sample Chart on Page {i}")
                };

                // ------------------------------------------------------------
                // Draw simple X and Y axes.
                // ------------------------------------------------------------
                Line xAxis = new Line(new float[] { 0, 0, (float)pageWidth, 0 });
                xAxis.GraphInfo.Color = Color.Black;
                graph.Shapes.Add(xAxis);

                Line yAxis = new Line(new float[] { 0, 0, 0, (float)pageHeight });
                yAxis.GraphInfo.Color = Color.Black;
                graph.Shapes.Add(yAxis);

                // ------------------------------------------------------------
                // Draw a very simple bar chart using rectangles.
                // ------------------------------------------------------------
                double[] data = new double[] { 10, 20, 30, 25, 15 };
                // Determine bar width and spacing based on page width.
                float margin = 40f; // left/right margin for the axes
                float usableWidth = (float)pageWidth - 2 * margin;
                float barSpacing = 10f;
                float barWidth = (usableWidth - (data.Length - 1) * barSpacing) / data.Length;
                float maxValue = 30f; // for scaling – in a real scenario compute from data
                float scaleY = ((float)pageHeight - 60f) / maxValue; // leave top margin for title

                for (int idx = 0; idx < data.Length; idx++)
                {
                    float barHeight = (float)data[idx] * scaleY;
                    float x = margin + idx * (barWidth + barSpacing);
                    // Rectangle(x, y, width, height) where y is the bottom coordinate.
                    var bar = new Aspose.Pdf.Drawing.Rectangle(x, 0, barWidth, barHeight);
                    bar.GraphInfo.FillColor = Color.SteelBlue;
                    bar.GraphInfo.Color = Color.Transparent; // no stroke
                    graph.Shapes.Add(bar);
                }

                // Add the graph to the page.
                page.Paragraphs.Add(graph);
            }

            // Save the document.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with {pageCount} pages created at '{outputPath}'.");
    }
}
