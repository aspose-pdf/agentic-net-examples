using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "circle_graph.pdf";

        // Circle parameters
        double centerX = 200;   // X coordinate of center
        double centerY = 300;   // Y coordinate of center
        double radius  = 50;    // Radius of the circle

        // Calculate bounding rectangle for the circle (ellipse with equal radii)
        double llx = centerX - radius; // lower‑left X
        double lly = centerY - radius; // lower‑left Y
        double urx = centerX + radius; // upper‑right X
        double ury = centerY + radius; // upper‑right Y

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page (pages are 1‑based)
            Page page = doc.Pages.Add();

            // Create a Graph container – a Graph is a paragraph that can hold drawing shapes
            Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

            // Create an ellipse shape using the bounding rectangle
            // Since width and height are equal, this renders as a circle
            Ellipse circle = new Ellipse(llx, lly, urx, ury);

            // Style the circle via its GraphInfo (fill, stroke, line width)
            circle.GraphInfo.FillColor = Aspose.Pdf.Color.LightBlue; // fill colour
            circle.GraphInfo.Color = Aspose.Pdf.Color.Black;        // border colour
            circle.GraphInfo.LineWidth = 1;                         // border width

            // Add the shape to the graph, then add the graph to the page
            graph.Shapes.Add(circle);
            page.Paragraphs.Add(graph);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with filled circle saved to '{outputPath}'.");
    }
}
