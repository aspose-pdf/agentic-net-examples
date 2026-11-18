using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_chart.pdf";

        // ---------------------------------------------------------------
        // Ensure the input PDF exists – create a minimal placeholder if it does not.
        // ---------------------------------------------------------------
        if (!File.Exists(inputPath))
        {
            using (var placeholder = new Document())
            {
                placeholder.Pages.Add();
                placeholder.Save(inputPath);
            }
        }

        // Load PDF bytes (replace with your own source as needed)
        byte[] pdfBytes = File.ReadAllBytes(inputPath);

        using (var ms = new MemoryStream(pdfBytes))
        using (var doc = new Document(ms))
        {
            // Ensure at least one page exists
            if (doc.Pages.Count == 0)
                doc.Pages.Add();

            Page page = doc.Pages[1];

            // ---------------------------------------------------------------------
            // 1. Create a Graph that will act as a column chart
            // ---------------------------------------------------------------------
            const float graphWidth = 400f;
            const float graphHeight = 250f;
            const float graphLeft = 50f;
            const float graphTop = 500f; // Y‑coordinate of the top edge of the graph

            // Use the double‑based constructor as the float overload is obsolete
            var graph = new Graph((double)graphWidth, (double)graphHeight)
            {
                Left = graphLeft,
                Top = graphTop,
                Border = new BorderInfo(BorderSide.All, 1f, Color.Black)
            };

            // ---------------------------------------------------------------------
            // 2. Draw axes
            // ---------------------------------------------------------------------
            var xAxis = new Line(new float[] { 0, 0, graphWidth, 0 });
            xAxis.GraphInfo.Color = Color.Black;
            var yAxis = new Line(new float[] { 0, 0, 0, graphHeight });
            yAxis.GraphInfo.Color = Color.Black;
            graph.Shapes.Add(xAxis);
            graph.Shapes.Add(yAxis);

            // ---------------------------------------------------------------------
            // 3. Sample data for the column chart
            // ---------------------------------------------------------------------
            double[] values = { 30, 55, 80, 45 };
            string[] categories = { "Q1", "Q2", "Q3", "Q4" };
            int barCount = values.Length;
            float barWidth = graphWidth / (barCount * 2); // space between bars
            float maxValue = 100f; // assumed maximum for scaling

            for (int i = 0; i < barCount; i++)
            {
                // Height of the bar proportional to the value
                float barHeight = (float)(values[i] / maxValue) * graphHeight;
                float left = (i * 2 + 1) * barWidth;

                // Drawing rectangle for the bar (Aspose.Pdf.Drawing.Rectangle)
                var bar = new Aspose.Pdf.Drawing.Rectangle(left, 0, barWidth, barHeight);
                bar.GraphInfo.FillColor = Color.SteelBlue;
                bar.GraphInfo.Color = Color.Transparent; // no border line
                graph.Shapes.Add(bar);

                // Category label (placed below the X‑axis on the page)
                var catLabel = new TextFragment(categories[i])
                {
                    TextState = { FontSize = 10, FontStyle = FontStyles.Regular },
                    Position = new Position(
                        graphLeft + left + barWidth / 2,
                        graphTop - graphHeight - 15) // 15 points below the axis
                };
                page.Paragraphs.Add(catLabel);
            }

            // ---------------------------------------------------------------------
            // 4. Y‑axis numeric labels (0, 25, 50, 75, 100)
            // ---------------------------------------------------------------------
            for (int i = 0; i <= 4; i++)
            {
                float y = i * graphHeight / 4;
                var yLabel = new TextFragment((i * 25).ToString())
                {
                    TextState = { FontSize = 10 },
                    Position = new Position(
                        graphLeft - 30,
                        graphTop - graphHeight + y - 5)
                };
                page.Paragraphs.Add(yLabel);
            }

            // ---------------------------------------------------------------------
            // 5. Title and subtitle (outside the graph area)
            // ---------------------------------------------------------------------
            var title = new TextFragment("Quarterly Sales")
            {
                Position = new Position(graphLeft, graphTop + 20),
                TextState = { FontSize = 14, FontStyle = FontStyles.Bold }
            };
            page.Paragraphs.Add(title);

            var subtitle = new TextFragment("Values in thousands")
            {
                Position = new Position(graphLeft, graphTop - graphHeight - 30),
                TextState = { FontSize = 10, FontStyle = FontStyles.Italic }
            };
            page.Paragraphs.Add(subtitle);

            // ---------------------------------------------------------------------
            // 6. Add the graph to the page and save
            // ---------------------------------------------------------------------
            page.Paragraphs.Add(graph);
            doc.Save(outputPath);
        }
    }
}
