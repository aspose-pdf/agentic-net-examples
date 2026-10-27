using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

public static class PdfGraphHelper
{
    /// <summary>
    /// Builds a simple predefined bar‑chart like graph on the supplied PDF page and returns the <see cref="Graph"/> instance.
    /// The implementation uses only core Aspose.Pdf APIs (no Facades) and therefore complies with the hard namespace restriction.
    /// </summary>
    /// <param name="page">The page on which the graph will be placed.</param>
    /// <returns>The <see cref="Graph"/> object that contains the drawn shapes.</returns>
    public static Graph CreatePredefinedGraph(Page page)
    {
        // ---------------------------------------------------------------------
        // 1. Define the area that will contain the graph (left, bottom, width, height).
        // ---------------------------------------------------------------------
        float left   = 50f;   // distance from the left side of the page
        float bottom = 150f;  // distance from the bottom of the page
        float gWidth = 500f;  // total width of the graph area
        float gHeight = 350f; // total height of the graph area

        // ---------------------------------------------------------------------
        // 2. Create a Graph container. The Graph constructor expects width & height –
        //    it does NOT take a Page or a Rectangle.
        // ---------------------------------------------------------------------
        Graph graph = new Graph(gWidth, gHeight);

        // ---------------------------------------------------------------------
        // 3. Add a title (TextFragment) – positioned above the graph area.
        // ---------------------------------------------------------------------
        TextFragment title = new TextFragment("Sample Sales Chart");
        title.Position = new Position(left + gWidth / 2, bottom + gHeight + 30);
        title.HorizontalAlignment = HorizontalAlignment.Center;
        title.TextState.Font = FontRepository.FindFont("Times New Roman");
        title.TextState.FontSize = 14;
        title.TextState.FontStyle = FontStyles.Bold;
        page.Paragraphs.Add(title);

        // ---------------------------------------------------------------------
        // 4. Draw X and Y axes using Line shapes.
        // ---------------------------------------------------------------------
        // X‑axis
        Line xAxis = new Line(new float[] { left, bottom, left + gWidth, bottom });
        xAxis.GraphInfo = new GraphInfo { Color = Color.Black, LineWidth = 1 };
        graph.Shapes.Add(xAxis);

        // Y‑axis
        Line yAxis = new Line(new float[] { left, bottom, left, bottom + gHeight });
        yAxis.GraphInfo = new GraphInfo { Color = Color.Black, LineWidth = 1 };
        graph.Shapes.Add(yAxis);

        // ---------------------------------------------------------------------
        // 5. Prepare data for the bar chart.
        // ---------------------------------------------------------------------
        string[] quarters = { "Q1", "Q2", "Q3", "Q4" };
        double[] values   = { 15000, 20000, 18000, 22000 };
        double maxValue    = 25000; // a round number a little above the highest value

        // Bar dimensions
        float barWidth = gWidth / (quarters.Length * 2); // leave a gap equal to bar width
        float gap      = barWidth;                       // gap between bars

        // ---------------------------------------------------------------------
        // 6. Draw each bar and its label.
        // ---------------------------------------------------------------------
        for (int i = 0; i < quarters.Length; i++)
        {
            // Calculate bar height proportionally.
            float barHeight = (float)(values[i] / maxValue) * gHeight;

            // Bottom‑left corner of the bar.
            float barLeft   = left + gap / 2 + i * (barWidth + gap);
            float barBottom = bottom;

            // Create the rectangle that represents the bar.
            Aspose.Pdf.Drawing.Rectangle bar = new Aspose.Pdf.Drawing.Rectangle(barLeft, barBottom, barWidth, barHeight);
            bar.GraphInfo = new GraphInfo
            {
                FillColor = Color.FromRgb(0.2f, 0.6f, 0.8f), // same colour as the original example
                Color     = Color.Black,
                LineWidth = 0.5f
            };
            graph.Shapes.Add(bar);

            // Add the quarter label under the bar.
            TextFragment label = new TextFragment(quarters[i]);
            label.Position = new Position(barLeft + barWidth / 2, bottom - 20);
            label.HorizontalAlignment = HorizontalAlignment.Center;
            label.TextState.Font = FontRepository.FindFont("Helvetica");
            label.TextState.FontSize = 12;
            page.Paragraphs.Add(label);
        }

        // ---------------------------------------------------------------------
        // 7. Add axis titles.
        // ---------------------------------------------------------------------
        // X‑axis title
        TextFragment xTitle = new TextFragment("Quarter");
        xTitle.Position = new Position(left + gWidth / 2, bottom - 45);
        xTitle.HorizontalAlignment = HorizontalAlignment.Center;
        xTitle.TextState.Font = FontRepository.FindFont("Helvetica");
        xTitle.TextState.FontSize = 12;
        page.Paragraphs.Add(xTitle);

        // Y‑axis title – rotated 90° counter‑clockwise.
        TextFragment yTitle = new TextFragment("Revenue (USD)");
        yTitle.Position = new Position(left - 40, bottom + gHeight / 2);
        yTitle.HorizontalAlignment = HorizontalAlignment.Center;
        yTitle.TextState.Font = FontRepository.FindFont("Helvetica");
        yTitle.TextState.FontSize = 12;
        yTitle.TextState.Rotation = 90; // rotate the text
        page.Paragraphs.Add(yTitle);

        // ---------------------------------------------------------------------
        // 8. Finally, add the Graph container to the page.
        // ---------------------------------------------------------------------
        page.Paragraphs.Add(graph);

        // Return the Graph instance for further manipulation if required.
        return graph;
    }
}

class Program
{
    static void Main()
    {
        // Create a new PDF document.
        Document doc = new Document();
        // Add a blank page.
        Page page = doc.Pages.Add();

        // Build the predefined graph on the page.
        PdfGraphHelper.CreatePredefinedGraph(page);

        // Save the PDF (adjust the path as needed).
        doc.Save("PredefinedGraph.pdf");
    }
}
