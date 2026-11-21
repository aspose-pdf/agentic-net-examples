using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "line_chart.pdf";

        // Ensure the output directory exists
        string outDir = System.IO.Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outDir) && !Directory.Exists(outDir))
            Directory.CreateDirectory(outDir);

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to host the graph
            Page page = doc.Pages.Add();

            // Graph dimensions (width, height)
            const float graphWidth = 500f;
            const float graphHeight = 300f;
            Graph graph = new Graph(graphWidth, graphHeight)
            {
                Border = new BorderInfo(BorderSide.All, Color.Gray),
                Title = new TextFragment("Line Graph with Distinct Stroke Styles")
            };

            // ------------------------------------------------------------
            // Draw X and Y axes (simple black lines)
            // ------------------------------------------------------------
            // X‑axis
            var xAxis = new Aspose.Pdf.Drawing.Line(new float[] { 0, 0, graphWidth, 0 });
            xAxis.GraphInfo.Color = Color.Black;
            graph.Shapes.Add(xAxis);

            // Y‑axis
            var yAxis = new Aspose.Pdf.Drawing.Line(new float[] { 0, 0, 0, graphHeight });
            yAxis.GraphInfo.Color = Color.Black;
            graph.Shapes.Add(yAxis);

            // ------------------------------------------------------------
            // Helper to convert data points to graph coordinates
            // ------------------------------------------------------------
            // Simple linear scaling: X values 1‑5 map to 0‑graphWidth, Y values 0‑40 map to 0‑graphHeight
            float[] ScaleX(double[] xs)
            {
                float[] result = new float[xs.Length * 2]; // x,y pairs
                for (int i = 0; i < xs.Length; i++)
                {
                    // leave a small left margin (20) so the line does not sit on the Y‑axis
                    float x = (float)((xs[i] - 1) / 4.0 * (graphWidth - 20)) + 20;
                    result[i * 2] = x;
                }
                return result;
            }

            float[] ScaleY(double[] ys)
            {
                float[] result = new float[ys.Length];
                for (int i = 0; i < ys.Length; i++)
                {
                    // invert Y because PDF origin is bottom‑left for Graph
                    float y = (float)(ys[i] / 40.0 * graphHeight);
                    result[i] = y;
                }
                return result;
            }

            // ------------------------------------------------------------
            // Series 1 – solid line (blue)
            // ------------------------------------------------------------
            double[] s1x = { 1, 2, 3, 4, 5 };
            double[] s1y = { 10, 20, 15, 25, 30 };
            float[] s1Points = new float[s1x.Length * 2];
            float[] s1xScaled = ScaleX(s1x);
            float[] s1yScaled = ScaleY(s1y);
            for (int i = 0; i < s1x.Length; i++)
            {
                s1Points[i * 2] = s1xScaled[i];
                s1Points[i * 2 + 1] = s1yScaled[i];
            }
            var line1 = new Aspose.Pdf.Drawing.Line(s1Points);
            line1.GraphInfo.Color = Color.Blue; // solid – no DashArray needed
            graph.Shapes.Add(line1);

            // ------------------------------------------------------------
            // Series 2 – dashed line (red)
            // ------------------------------------------------------------
            double[] s2x = { 1, 2, 3, 4, 5 };
            double[] s2y = { 15, 25, 20, 30, 35 };
            float[] s2Points = new float[s2x.Length * 2];
            float[] s2xScaled = ScaleX(s2x);
            float[] s2yScaled = ScaleY(s2y);
            for (int i = 0; i < s2x.Length; i++)
            {
                s2Points[i * 2] = s2xScaled[i];
                s2Points[i * 2 + 1] = s2yScaled[i];
            }
            var line2 = new Aspose.Pdf.Drawing.Line(s2Points);
            line2.GraphInfo.Color = Color.Red;
            line2.GraphInfo.DashArray = new int[] { 5, 3 }; // dash pattern
            graph.Shapes.Add(line2);

            // ------------------------------------------------------------
            // Series 3 – dotted line (green)
            // ------------------------------------------------------------
            double[] s3x = { 1, 2, 3, 4, 5 };
            double[] s3y = { 5, 15, 10, 20, 25 };
            float[] s3Points = new float[s3x.Length * 2];
            float[] s3xScaled = ScaleX(s3x);
            float[] s3yScaled = ScaleY(s3y);
            for (int i = 0; i < s3x.Length; i++)
            {
                s3Points[i * 2] = s3xScaled[i];
                s3Points[i * 2 + 1] = s3yScaled[i];
            }
            var line3 = new Aspose.Pdf.Drawing.Line(s3Points);
            line3.GraphInfo.Color = Color.Green;
            line3.GraphInfo.DashArray = new int[] { 1, 2 }; // dotted pattern
            graph.Shapes.Add(line3);

            // Add the completed graph to the page
            page.Paragraphs.Add(graph);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Line chart PDF saved to '{outputPath}'.");
    }
}
