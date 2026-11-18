using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

public static class PdfGraphicsHelper
{
    // Clones a drawing rectangle, changes its fill color, and adds the clone to the specified graph.
    public static void CloneRectangleChangeFillAndAdd(Graph graph, Aspose.Pdf.Drawing.Rectangle original, Aspose.Pdf.Color newFillColor)
    {
        // Clone position and size (left, bottom, width, height) – cast to float because Rectangle ctor expects float.
        var clonedRect = new Aspose.Pdf.Drawing.Rectangle(
            (float)original.Left,
            (float)original.Bottom,
            (float)original.Width,
            (float)original.Height);

        // Copy visual styling from the original via GraphInfo and replace the fill color.
        clonedRect.GraphInfo = new GraphInfo
        {
            Color = original.GraphInfo.Color,
            FillColor = newFillColor,
            LineWidth = original.GraphInfo.LineWidth,
            DashArray = original.GraphInfo.DashArray,
            DashPhase = original.GraphInfo.DashPhase,
            IsDoubled = original.GraphInfo.IsDoubled,
            RotationAngle = original.GraphInfo.RotationAngle,
            ScalingRateX = original.GraphInfo.ScalingRateX,
            ScalingRateY = original.GraphInfo.ScalingRateY,
            SkewAngleX = original.GraphInfo.SkewAngleX,
            SkewAngleY = original.GraphInfo.SkewAngleY
        };

        // Add the cloned rectangle to the graph.
        graph.Shapes.Add(clonedRect);
    }
}

class Program
{
    static void Main()
    {
        // Create a new PDF document and a page.
        var doc = new Document();
        var page = doc.Pages.Add();

        // Create a Graph that covers the whole page. Graph constructor expects float values.
        var graph = new Graph((float)page.PageInfo.Width, (float)page.PageInfo.Height);

        // Define the original rectangle (left, bottom, width, height).
        var original = new Aspose.Pdf.Drawing.Rectangle(100f, 500f, 200f, 100f);
        original.GraphInfo.Color = Aspose.Pdf.Color.Black; // Stroke color
        original.GraphInfo.LineWidth = 2f; // Stroke width
        original.GraphInfo.FillColor = Aspose.Pdf.Color.FromArgb(128, 255, 0, 0); // Semi‑transparent red

        // Add the original rectangle to the graph.
        graph.Shapes.Add(original);

        // Clone the rectangle, change its fill to semi‑transparent blue, and add it to the same graph.
        PdfGraphicsHelper.CloneRectangleChangeFillAndAdd(
            graph,
            original,
            Aspose.Pdf.Color.FromArgb(128, 0, 0, 255)
        );

        // Add the graph to the page.
        page.Paragraphs.Add(graph);

        // Save the document to a file (or a stream).
        using var ms = new MemoryStream();
        doc.Save(ms);
        File.WriteAllBytes("output.pdf", ms.ToArray());
    }
}
