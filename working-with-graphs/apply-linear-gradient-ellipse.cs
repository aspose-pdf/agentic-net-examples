using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "ellipse_gradient.pdf";

        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Create a Graph that spans the page (size can be the page size or any container size you need)
            Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

            // Ellipse bounds (left, bottom, width, height)
            double left   = 100;   // LLX
            double bottom = 500;   // LLY
            double width  = 200;   // URX - LLX (300 - 100)
            double height = 200;   // URY - LLY (700 - 500)

            // Define an axial gradient shading from blue to green
            GradientAxialShading axialShading = new GradientAxialShading(
                Color.FromRgb(0f, 0f, 1f), // Blue
                Color.FromRgb(0f, 1f, 0f)  // Green
            );
            axialShading.Start = new Point(left, bottom);
            axialShading.End   = new Point(left + width, bottom + height);

            // Wrap the shading in a Color object that can be used as a fill
            Color gradientFill = new Color
            {
                PatternColorSpace = axialShading
            };

            // Create the ellipse shape and apply styling
            Ellipse ellipse = new Ellipse(left, bottom, width, height);
            ellipse.GraphInfo.FillColor = gradientFill;   // gradient fill
            ellipse.GraphInfo.Color = Color.Black;        // stroke color
            ellipse.GraphInfo.LineWidth = 1f;             // stroke width

            // Add the ellipse to the graph, then add the graph to the page
            graph.Shapes.Add(ellipse);
            page.Paragraphs.Add(graph);

            // Save the document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with gradient ellipse saved to '{outputPath}'.");
    }
}
