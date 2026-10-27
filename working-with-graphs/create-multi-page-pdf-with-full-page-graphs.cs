using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "multi_page_graph.pdf";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            int pageCount = 3; // Number of pages to generate

            for (int i = 1; i <= pageCount; i++)
            {
                // Add a new page (default size, e.g., A4)
                Page page = doc.Pages.Add();

                // Retrieve the page dimensions (points)
                double pageWidth = page.PageInfo.Width;
                double pageHeight = page.PageInfo.Height;

                // Create a Graph that occupies the whole page (use double ctor as the float ctor is obsolete)
                Graph graph = new Graph(pageWidth, pageHeight)
                {
                    // Position the graph at the origin of the page
                    Left = 0,
                    Top = 0,
                    // Optional visual styling for the graph container
                    Border = new BorderInfo(BorderSide.All, 1f, Color.Black),
                    // Background colour for the graph area
                    GraphInfo = new GraphInfo { FillColor = Color.LightGray }
                };

                // Set a title for the graph (appears above the drawing area)
                graph.Title = new TextFragment($"Page {i} Chart");

                // Sample data series – you can replace this with any data you need
                double[] sampleValues = new double[] { 30, 20, 50 };
                DrawBarChart(graph, sampleValues);

                // Insert the graph into the page's content.
                page.Paragraphs.Add(graph);
            }

            // Save the multi‑page PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }

    /// <summary>
    /// Draws a simple vertical bar chart inside the supplied graph.
    /// The chart is scaled to the graph's width and height.
    /// </summary>
    private static void DrawBarChart(Graph graph, double[] values)
    {
        if (values == null || values.Length == 0)
            return;

        // Determine scaling factors based on graph size
        float graphWidth = (float)graph.Width;
        float graphHeight = (float)graph.Height;
        double maxValue = 0;
        foreach (double v in values)
            if (v > maxValue) maxValue = v;
        if (maxValue == 0) maxValue = 1; // avoid division by zero

        // Bar layout calculations
        int barCount = values.Length;
        float barSpacing = graphWidth / (barCount * 2 + 1); // space between bars
        float barWidth = barSpacing; // make bar width equal to spacing for a balanced look
        float currentX = barSpacing; // start after initial spacing

        for (int i = 0; i < barCount; i++)
        {
            // Height of the bar proportional to the value
            float barHeight = (float)(values[i] / maxValue * (graphHeight * 0.8)); // leave 20% top margin

            // Create a rectangle representing the bar. (x, y) is lower‑left corner.
            // In Aspose.Pdf.Drawing.Rectangle the parameters are: lowerLeftX, lowerLeftY, upperRightX, upperRightY
            Aspose.Pdf.Drawing.Rectangle bar = new Aspose.Pdf.Drawing.Rectangle(currentX, 0, currentX + barWidth, barHeight);
            bar.GraphInfo.FillColor = Color.SteelBlue;
            bar.GraphInfo.Color = Color.Black; // border colour
            bar.GraphInfo.LineWidth = 0.5f;

            // Optional label under each bar
            var label = new TextFragment($"{values[i]}");
            label.TextState.Font = FontRepository.FindFont("Helvetica");
            label.TextState.FontSize = 10;
            label.TextState.ForegroundColor = Color.Black;
            // Position the label centred under the bar
            label.Position = new Position(currentX + barWidth / 2, -15);
            bar.Text = label;

            graph.Shapes.Add(bar);
            currentX += barWidth + barSpacing; // move to next bar position
        }
    }
}
