using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "filled_curve.pdf";

        using (var doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Create a Graph that spans the whole page (or any desired size)
            var graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

            // Bezier curve points: start, control1, control2, end
            float[] bezier = new float[]
            {
                100f, 500f,   // start point
                200f, 600f,   // first control point
                300f, 400f,   // second control point
                400f, 550f    // end point
            };

            // Create the curve shape
            var curve = new Curve(bezier);

            // Styling via GraphInfo (stroke, fill, opacity, thickness)
            // Semi‑transparent light‑blue fill (alpha 128 = 50 % opacity)
            curve.GraphInfo.FillColor = Color.FromArgb(128, 51, 153, 204); // #3399CC
            // Stroke (border) color and thickness
            curve.GraphInfo.Color = Color.Black;
            curve.GraphInfo.LineWidth = 2f;

            // Add the curve to the graph and the graph to the page
            graph.Shapes.Add(curve);
            page.Paragraphs.Add(graph);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with filled curve saved to '{outputPath}'.");
    }
}
