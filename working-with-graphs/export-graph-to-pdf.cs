using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "graph_output.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Create a Graph container – use double parameters as the float overload is obsolete
            Graph graph = new Graph(500.0, 400.0);
            // Position the graph on the page (optional – defaults to (0,0))
            graph.Left = 50;   // points from the left edge of the page
            graph.Top = 50;    // points from the bottom edge of the page

            // Add a title to the graph
            graph.Title = new TextFragment("Sample Graph");

            // Simple bar‑chart data (X values are implicit, Y values are the heights)
            double[] yValues = { 10, 30, 20, 50, 40 };
            double yMax = 100;               // axis maximum (used for scaling)
            float barWidth = 40f;            // width of each bar (points)
            float barSpacing = 20f;          // space between bars (points)
            float originX = 20f;             // left margin inside the graph surface
            float originY = 20f;             // bottom margin inside the graph surface
            float graphHeight = (float)graph.Height - 2 * originY; // drawable height

            for (int i = 0; i < yValues.Length; i++)
            {
                // Calculate bar height proportionally to the maximum Y value
                float barHeight = (float)(yValues[i] / yMax) * graphHeight;
                // X position of the current bar
                float barX = originX + i * (barWidth + barSpacing);
                // Y position (bottom of the bar) – graph origin is at Bottom
                float barY = originY;

                // Create the rectangle that represents the bar (use fully‑qualified type to avoid ambiguity)
                Aspose.Pdf.Drawing.Rectangle bar = new Aspose.Pdf.Drawing.Rectangle(barX, barY, barWidth, barHeight);
                // Set visual style via GraphInfo
                bar.GraphInfo.FillColor = Color.SteelBlue;
                bar.GraphInfo.Color = Color.Black; // stroke colour
                bar.GraphInfo.LineWidth = 1f;

                // Add a label under the bar (optional)
                TextFragment label = new TextFragment($"{i + 1}");
                label.TextState.Font = FontRepository.FindFont("Helvetica");
                label.TextState.FontSize = 10;
                // Attach the label to the bar
                bar.Text = label;

                // Add the bar to the graph
                graph.Shapes.Add(bar);
            }

            // Add the graph to the page
            page.Paragraphs.Add(graph);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Graph exported to '{outputPath}'.");
    }
}
