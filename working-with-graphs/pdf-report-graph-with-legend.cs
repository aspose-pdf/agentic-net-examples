using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "Report.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a single page to the document
            Page page = doc.Pages.Add();

            // Define graph dimensions (width, height) in points
            double graphWidth = 400;
            double graphHeight = 200;

            // Create a Graph container (constructor expects double values)
            Graph graph = new Graph(graphWidth, graphHeight)
            {
                // Position the graph on the page (left, top)
                Left = 50,
                Top  = 500
            };

            // Sample data categories with heights and colors
            // Category A
            Aspose.Pdf.Drawing.Rectangle rectA = new Aspose.Pdf.Drawing.Rectangle(
                0f,          // left (float)
                0f,          // bottom (float)
                50f,         // width (float)
                60f);        // height (float)
            rectA.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.LightBlue,
                Color     = Aspose.Pdf.Color.Black,
                LineWidth = 1f   // float literal
            };
            graph.Shapes.Add(rectA);

            // Category B
            Aspose.Pdf.Drawing.Rectangle rectB = new Aspose.Pdf.Drawing.Rectangle(
                70f,
                0f,
                50f,
                100f);
            rectB.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.LightGreen,
                Color     = Aspose.Pdf.Color.Black,
                LineWidth = 1f
            };
            graph.Shapes.Add(rectB);

            // Category C
            Aspose.Pdf.Drawing.Rectangle rectC = new Aspose.Pdf.Drawing.Rectangle(
                140f,
                0f,
                50f,
                40f);
            rectC.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.LightCoral,
                Color     = Aspose.Pdf.Color.Black,
                LineWidth = 1f
            };
            graph.Shapes.Add(rectC);

            // Add the graph to the page
            page.Paragraphs.Add(graph);

            // ---------- Legend ----------
            // Position for legend entries (below the graph)
            double legendX = graph.Left;
            double legendY = graph.Top - 30;

            // Helper to add a legend entry: colored box + label
            void AddLegendEntry(string label, Aspose.Pdf.Color color, double offsetY)
            {
                // Small colored rectangle (10x10 points)
                Aspose.Pdf.Drawing.Rectangle box = new Aspose.Pdf.Drawing.Rectangle(
                    (float)legendX,
                    (float)(legendY - offsetY),
                    10f,
                    10f);
                box.GraphInfo = new GraphInfo
                {
                    FillColor = color,
                    Color     = Aspose.Pdf.Color.Black,
                    LineWidth = 0.5f
                };
                graph.Shapes.Add(box);

                // Text label positioned next to the box
                TextFragment tf = new TextFragment(label)
                {
                    Position = new Position(legendX + 15, legendY - offsetY)
                };
                tf.TextState.FontSize = 10;
                tf.TextState.ForegroundColor = Aspose.Pdf.Color.Black;
                page.Paragraphs.Add(tf);
            }

            // Add legend entries for each category
            AddLegendEntry("Category A", Aspose.Pdf.Color.LightBlue,   0);
            AddLegendEntry("Category B", Aspose.Pdf.Color.LightGreen, 15);
            AddLegendEntry("Category C", Aspose.Pdf.Color.LightCoral, 30);

            // Save the PDF report
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF report saved to '{outputPath}'.");
    }
}
