using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "line_chart.pdf";

        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Graph size and margins
            const float graphWidth = 500f;
            const float graphHeight = 300f;
            const float marginLeft = 50f;
            const float marginBottom = 50f;

            // Create the graph container
            Graph graph = new Graph(graphWidth, graphHeight);
            graph.Border = new BorderInfo(BorderSide.All, 1);
            graph.Title = new TextFragment("Line Chart with Three Series");

            // Draw axes
            Line xAxis = new Line(new float[] { marginLeft, marginBottom, marginLeft + graphWidth, marginBottom });
            xAxis.GraphInfo.Color = Color.Black;
            graph.Shapes.Add(xAxis);

            Line yAxis = new Line(new float[] { marginLeft, marginBottom, marginLeft, marginBottom + graphHeight });
            yAxis.GraphInfo.Color = Color.Black;
            graph.Shapes.Add(yAxis);

            // Data series
            double[] xValues = { 1, 2, 3, 4, 5 };
            double[] ySeries1 = { 10, 20, 15, 30, 25 };
            double[] ySeries2 = { 15, 25, 20, 35, 30 };
            double[] ySeries3 = { 5, 15, 10, 20, 15 };

            // Simple linear scaling (add some head‑room for Y)
            double xMin = 1, xMax = 5;
            double yMin = 0, yMax = 40;
            double xScale = graphWidth / (xMax - xMin);
            double yScale = graphHeight / (yMax - yMin);

            // Helper lambdas to convert data points to graph coordinates
            float ToX(double x) => (float)(marginLeft + (x - xMin) * xScale);
            float ToY(double y) => (float)(marginBottom + (y - yMin) * yScale);

            // Series 1 – solid blue line
            graph.Shapes.Add(CreateLine(xValues, ySeries1, ToX, ToY, Color.Blue, null));

            // Series 2 – dashed red line
            graph.Shapes.Add(CreateLine(xValues, ySeries2, ToX, ToY, Color.Red, new int[] { 4, 2 }));

            // Series 3 – dotted green line
            graph.Shapes.Add(CreateLine(xValues, ySeries3, ToX, ToY, Color.Green, new int[] { 1, 2 }));

            // Add the graph to the page
            page.Paragraphs.Add(graph);

            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }

    private static Line CreateLine(double[] xVals, double[] yVals,
        Func<double, float> toX, Func<double, float> toY,
        Color color, int[] dashArray)
    {
        if (xVals.Length != yVals.Length)
            throw new ArgumentException("X and Y arrays must have the same length.");

        float[] points = new float[xVals.Length * 2];
        for (int i = 0; i < xVals.Length; i++)
        {
            points[i * 2] = toX(xVals[i]);
            points[i * 2 + 1] = toY(yVals[i]);
        }

        Line line = new Line(points);
        line.GraphInfo.Color = color;
        line.GraphInfo.LineWidth = 2f;
        if (dashArray != null)
            line.GraphInfo.DashArray = dashArray;

        return line;
    }
}
