using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "gradient_rectangle.pdf";

        // Create a placeholder PDF if the input does not exist.
        if (!File.Exists(inputPath))
        {
            using (Document tmp = new Document())
            {
                tmp.Pages.Add();
                tmp.Save(inputPath);
            }
        }

        // Load the document.
        using (Document doc = new Document(inputPath))
        {
            // Aspose.Pdf pages are 1‑based.
            Page page = doc.Pages[1];

            // Create a drawing rectangle (lower‑left x,y and upper‑right x,y).
            Aspose.Pdf.Drawing.Rectangle rectShape = new Aspose.Pdf.Drawing.Rectangle(100, 500, 300, 700);

            // Build a vertical axial gradient shading with alpha channel.
            // Gradient from transparent blue (alpha 0) at the bottom to opaque blue (alpha 255) at the top.
            GradientAxialShading axialGradient = new GradientAxialShading(
                Aspose.Pdf.Color.FromArgb(0, 0, 0, 255),   // Transparent blue
                Aspose.Pdf.Color.FromArgb(255, 0, 0, 255) // Opaque blue
            );
            // Define gradient direction (bottom to top of the rectangle).
            axialGradient.Start = new Aspose.Pdf.Point(100, 500); // lower‑left corner
            axialGradient.End = new Aspose.Pdf.Point(100, 700);   // upper‑left corner

            // Apply the gradient as the rectangle's fill via PatternColorSpace.
            rectShape.GraphInfo = new GraphInfo
            {
                FillColor = new Aspose.Pdf.Color { PatternColorSpace = axialGradient },
                Color = Aspose.Pdf.Color.Black, // Border color
                LineWidth = 1
            };

            // Shapes must be placed inside a Graph container.
            Graph graph = new Graph(500, 800); // size large enough to contain the rectangle
            graph.Shapes.Add(rectShape);
            page.Paragraphs.Add(graph);

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with gradient rectangle saved to '{outputPath}'.");
    }
}
