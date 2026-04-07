using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Paths for input (optional) and output PDF files
        const string outputPath = "ellipse_gradient.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a Graph container (acts as a drawing canvas)
            // Width and height can be larger than the ellipse to give some margin
            Graph graph = new Graph(500, 800);

            // Create an ellipse: left, bottom, width, height
            Ellipse ellipse = new Ellipse(150, 300, 200, 150);

            // ---------- Configure gradient fill ----------
            // Create an axial gradient that goes from left (red) to right (blue)
            GradientAxialShading axialGradient = new GradientAxialShading(
                Aspose.Pdf.Color.Red,   // start color
                Aspose.Pdf.Color.Blue   // end color
            );

            // Define the start and end points of the gradient (in the ellipse's coordinate space)
            // Here the gradient runs horizontally across the ellipse
            axialGradient.Start = new Point(0, 0);          // left side of the ellipse
            axialGradient.End   = new Point(200, 0);        // right side of the ellipse

            // Wrap the gradient in a Color object via its PatternColorSpace property
            Aspose.Pdf.Color gradientFill = new Aspose.Pdf.Color();
            gradientFill.PatternColorSpace = axialGradient;

            // Assign the gradient fill to the ellipse's GraphInfo
            ellipse.GraphInfo = new GraphInfo
            {
                FillColor = gradientFill
            };
            // ------------------------------------------------

            // Add the ellipse to the graph's shape collection
            graph.Shapes.Add(ellipse);

            // Add the graph to the page's paragraph collection
            page.Paragraphs.Add(graph);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with gradient-filled ellipse saved to '{outputPath}'.");
    }
}