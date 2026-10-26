using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "graph.pdf";

        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Create a graph of size 300x300 and position it at (50,400)
            Graph graph = new Graph(300f, 300f);
            graph.Left = 50f;
            graph.Top = 400f;

            // Define an arc (center at 150,150, radius 100) with start and sweep angles
            Arc arc = new Arc(150f, 150f, 100f, 30f, 120f);
            arc.GraphInfo.Color = Aspose.Pdf.Color.Black; // stroke color (optional)
            arc.GraphInfo.LineWidth = 1f;

            // Create a radial gradient fill
            GradientRadialShading radial = new GradientRadialShading(
                Aspose.Pdf.Color.FromRgb(0.9f, 0.2f, 0.2f), // light red
                Aspose.Pdf.Color.FromRgb(0.2f, 0.2f, 0.9f)  // blue
            );
            // Apply the gradient as the fill color of the arc
            arc.GraphInfo.FillColor = new Aspose.Pdf.Color { PatternColorSpace = radial };

            // Add the arc to the graph
            graph.Shapes.Add(arc);

            // Add the graph to the page
            page.Paragraphs.Add(graph);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with filled arc saved to {outputPath}");
    }
}
