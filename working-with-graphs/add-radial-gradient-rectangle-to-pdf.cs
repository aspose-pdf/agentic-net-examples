using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "gradient_rectangle.pdf";

        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Graph container (width, height)
            Graph graph = new Graph(500.0, 400.0);

            // Rectangle shape inside the graph (left, bottom, width, height)
            Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(0, 0, 300, 200);

            // Radial gradient: opaque red at centre → fully transparent at edges
            GradientRadialShading radial = new GradientRadialShading(
                Aspose.Pdf.Color.FromArgb(255, 255, 0, 0), // start (opaque red)
                Aspose.Pdf.Color.FromArgb(0,   255, 0, 0)  // end   (transparent red)
            );

            // Define gradient geometry (centre of rectangle)
            radial.Start = new Aspose.Pdf.Point(150, 100); // centre point
            radial.End   = new Aspose.Pdf.Point(150, 100); // same centre for radial
            radial.StartingRadius = 0;    // radius at centre
            radial.EndingRadius   = 150;  // radius reaching rectangle edges

            // Assign the gradient to the fill colour via PatternColorSpace
            Aspose.Pdf.Color fillColor = new Aspose.Pdf.Color();
            fillColor.PatternColorSpace = radial;

            // Set visual properties of the rectangle
            rect.GraphInfo = new GraphInfo
            {
                FillColor = fillColor,               // gradient fill
                Color     = Aspose.Pdf.Color.Black,  // stroke colour
                LineWidth = 1f
            };

            // Add rectangle to the graph and graph to the page
            graph.Shapes.Add(rect);
            page.Paragraphs.Add(graph);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
