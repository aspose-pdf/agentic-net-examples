using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Paths to the source PDF and the output PDF
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify that the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the existing PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Use the first page as the canvas for the ellipse
            Page page = doc.Pages[1];

            // Create a Graph container sized to the page
            Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

            // Define ellipse parameters (center and radii)
            double centerX          = 200; // X coordinate of the ellipse center
            double centerY          = 400; // Y coordinate of the ellipse center
            double horizontalRadius = 100; // Horizontal radius
            double verticalRadius   = 50;  // Vertical radius

            // Convert radii to left, bottom, width, height for the constructor
            double left   = centerX - horizontalRadius;
            double bottom = centerY - verticalRadius;
            double width  = horizontalRadius * 2;
            double height = verticalRadius * 2;

            // Create the ellipse shape
            Ellipse ellipse = new Ellipse(left, bottom, width, height);

            // Set the stroke (outline) color via GraphInfo
            ellipse.GraphInfo = new GraphInfo
            {
                Color = Aspose.Pdf.Color.Red // Stroke color
                // FillColor can be set here if a fill is desired
            };

            // Add the ellipse to the graph
            graph.Shapes.Add(ellipse);

            // Add the graph to the page's paragraph collection
            page.Paragraphs.Add(graph);

            // Save the modified PDF (lifecycle rule: save inside using)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Ellipse added and saved to '{outputPath}'.");
    }
}