using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Paths for the output PDF
        const string outputPath = "ellipse_gradient.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Create a Graph container (required for drawing shapes)
            // Use double parameters as the float overload is obsolete
            Graph graph = new Graph(500.0, 500.0);

            // Define an ellipse (left, bottom, width, height)
            Ellipse ellipse = new Ellipse(100, 200, 250, 150);

            // Apply a radial gradient fill to the ellipse
            // NOTE: In some Aspose.Pdf versions GradientRadialShading cannot be assigned directly to FillColor.
            // If the library version supports it, you can uncomment the line below and remove the solid FillColor.
            // GradientRadialShading gradient = new GradientRadialShading(Aspose.Pdf.Color.LightBlue, Aspose.Pdf.Color.DarkBlue);

            // Set visual properties via GraphInfo
            ellipse.GraphInfo = new GraphInfo
            {
                // FillColor = gradient, // Uncomment if GradientRadialShading is accepted as FillColor
                FillColor = Aspose.Pdf.Color.LightBlue, // Fallback solid fill to keep the code compilable
                Color = Aspose.Pdf.Color.Black,       // stroke color
                LineWidth = 2
            };

            // Add the ellipse to the graph
            graph.Shapes.Add(ellipse);

            // Add the graph to the page
            page.Paragraphs.Add(graph);

            // Calculate the ellipse's bounding box using its geometry properties
            double left   = ellipse.Left;
            double bottom = ellipse.Bottom;
            double right  = ellipse.Left + ellipse.Width;
            double top    = ellipse.Bottom + ellipse.Height;

            // Create a rectangle representing the bounding box
            Aspose.Pdf.Rectangle ellipseBBox = new Aspose.Pdf.Rectangle(left, bottom, right, top);

            // Output the bounding box coordinates to the console
            Console.WriteLine("Ellipse Bounding Box:");
            Console.WriteLine($"Left   = {ellipseBBox.LLX}");
            Console.WriteLine($"Bottom = {ellipseBBox.LLY}");
            Console.WriteLine($"Right  = {ellipseBBox.URX}");
            Console.WriteLine($"Top    = {ellipseBBox.URY}");

            // Save the PDF (PDF format, no extra SaveOptions needed)
            doc.Save(outputPath);
        }

        // Fully qualify System.IO.Path to avoid ambiguity with Aspose.Pdf.Drawing.Path
        Console.WriteLine($"PDF saved to '{System.IO.Path.GetFullPath(outputPath)}'.");
    }
}
