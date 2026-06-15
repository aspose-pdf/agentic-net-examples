using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

public class Program
{
    public static void Main()
    {
        // Create a sample PDF (self‑contained example)
        using (Document sampleDoc = new Document())
        {
            Page samplePage = sampleDoc.Pages.Add();
            sampleDoc.Save("input.pdf");
        }

        // Open the sample PDF and add a line graph with three series
        using (Document pdfDoc = new Document("input.pdf"))
        {
            Page page = pdfDoc.Pages[1];

            // Graph canvas (width: 500, height: 300)
            Graph graph = new Graph(500f, 300f);
            page.Paragraphs.Add(graph);

            // Series 1 – solid red line
            float[] points1 = new float[] { 50f, 250f, 450f, 250f };
            Line line1 = new Line(points1);
            line1.GraphInfo.Color = Color.Red;
            line1.GraphInfo.LineWidth = 2f;
            graph.Shapes.Add(line1);

            // Series 2 – dashed blue line
            float[] points2 = new float[] { 50f, 200f, 450f, 200f };
            Line line2 = new Line(points2);
            line2.GraphInfo.Color = Color.Blue;
            line2.GraphInfo.LineWidth = 2f;
            line2.GraphInfo.DashArray = new int[] { 5, 3 };
            graph.Shapes.Add(line2);

            // Series 3 – dotted green line
            float[] points3 = new float[] { 50f, 150f, 450f, 150f };
            Line line3 = new Line(points3);
            line3.GraphInfo.Color = Color.Green;
            line3.GraphInfo.LineWidth = 2f;
            line3.GraphInfo.DashArray = new int[] { 1, 2 };
            graph.Shapes.Add(line3);

            pdfDoc.Save("output.pdf");
        }
    }
}