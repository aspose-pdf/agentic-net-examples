using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

public static class PdfGenerator
{
    /// <summary>
    /// Creates a PDF document in memory, adds a simple column chart drawn with Graph primitives,
    /// and returns the PDF as a byte array.
    /// </summary>
    public static byte[] CreatePdfWithGraph()
    {
        // MemoryStream will hold the PDF data.
        using (MemoryStream ms = new MemoryStream())
        {
            // Create a new PDF document.
            using (Document doc = new Document())
            {
                // Add a page to the document.
                Page page = doc.Pages.Add();

                // Define chart dimensions and position.
                const double chartWidth = 500.0;   // use double for Graph constructor
                const double chartHeight = 300.0;
                const float chartLeft = 100f; // X position on the page
                const float chartTop = 500f;  // Y position on the page (from bottom)

                // Create a Graph which will act as the chart container.
                Graph chart = new Graph(chartWidth, chartHeight)
                {
                    Left = chartLeft,
                    Top = chartTop,
                    Border = new BorderInfo(BorderSide.All, 1f, Aspose.Pdf.Color.Black),
                    Title = new TextFragment("Sample Column Chart")
                };

                // Sample data points.
                double[] values = { 10, 20, 30, 25, 15 };
                double maxValue = 30; // could be calculated: values.Max()
                int barCount = values.Length;

                // Simple margins inside the graph.
                const float marginLeft = 40f;
                const float marginBottom = 30f;
                const float usableWidth = (float)chartWidth - marginLeft - 20f; // right margin 20
                const float usableHeight = (float)chartHeight - marginBottom - 20f; // top margin 20

                float barWidth = usableWidth / barCount * 0.7f; // 70% of slot width
                float barSpacing = (usableWidth / barCount) * 0.3f; // remaining 30% as spacing
                float scale = usableHeight / (float)maxValue;

                // Draw X and Y axes.
                var xAxis = new Line(new float[] { marginLeft, marginBottom, marginLeft + usableWidth, marginBottom });
                xAxis.GraphInfo.Color = Aspose.Pdf.Color.Black;
                chart.Shapes.Add(xAxis);

                var yAxis = new Line(new float[] { marginLeft, marginBottom, marginLeft, marginBottom + usableHeight });
                yAxis.GraphInfo.Color = Aspose.Pdf.Color.Black;
                chart.Shapes.Add(yAxis);

                // Draw bars.
                for (int i = 0; i < barCount; i++)
                {
                    float barLeft = marginLeft + i * (barWidth + barSpacing) + barSpacing / 2;
                    float barHeight = (float)(values[i] * scale);
                    // Use fully‑qualified drawing rectangle to avoid ambiguity with Aspose.Pdf.Rectangle
                    Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(barLeft, marginBottom, barWidth, barHeight);
                    rect.GraphInfo.FillColor = Aspose.Pdf.Color.SteelBlue;
                    rect.GraphInfo.Color = Aspose.Pdf.Color.Transparent; // no border line
                    chart.Shapes.Add(rect);

                    // Optional: add value label above each bar.
                    var label = new TextFragment(values[i].ToString())
                    {
                        TextState = { FontSize = 10, Font = FontRepository.FindFont("Helvetica"), ForegroundColor = Aspose.Pdf.Color.Black }
                    };
                    // Position the label – TextFragment.Position expects a Position object.
                    label.Position = new Position(chartLeft + barLeft, chartTop + marginBottom + barHeight + 5);
                    page.Paragraphs.Add(label);
                }

                // Add the chart (graph) to the page.
                page.Paragraphs.Add(chart);

                // Save the PDF into the MemoryStream.
                doc.Save(ms);
            }

            // Return the PDF bytes.
            return ms.ToArray();
        }
    }
}

class Program
{
    static void Main()
    {
        byte[] pdfBytes = PdfGenerator.CreatePdfWithGraph();
        Console.WriteLine($"PDF generated in memory, size = {pdfBytes.Length} bytes");
        // For demonstration purposes write the file to disk (optional).
        File.WriteAllBytes("SampleChart.pdf", pdfBytes);
    }
}
