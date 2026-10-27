using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "circle_gradient.pdf";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Create a Graph container that covers the whole page (use double overload as recommended)
            double graphWidth = page.PageInfo.Width;
            double graphHeight = page.PageInfo.Height;
            Graph graph = new Graph(graphWidth, graphHeight);

            // Create a circle: center at (300, 400) with radius 100
            Circle circle = new Circle(300f, 400f, 100f);

            // Apply a solid fill (gradient not available in this version of Aspose.Pdf)
            // If a future version adds FillColor2, it can be set here.
            circle.GraphInfo.FillColor = Aspose.Pdf.Color.FromRgb(1, 0, 0); // Red (start color)
            // circle.GraphInfo.FillColor2 = Aspose.Pdf.Color.FromRgb(0, 0, 1); // Blue (end color) – not supported in current API

            // Optional: set border color and width
            circle.GraphInfo.Color = Aspose.Pdf.Color.Black;
            circle.GraphInfo.LineWidth = 1f;

            // Add the circle to the graph and the graph to the page
            graph.Shapes.Add(circle);
            page.Paragraphs.Add(graph);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with circle saved to '{outputPath}'.");
    }
}
