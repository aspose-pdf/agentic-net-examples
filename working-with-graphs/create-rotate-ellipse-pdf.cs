using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Input and output PDF paths
        const string inputPath  = "input.pdf";
        const string outputPath = "rotated_ellipse.pdf";

        // Ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Open the existing PDF (or create a new one if you prefer)
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (add a page if the document is empty)
            Page page = doc.Pages.Count > 0 ? doc.Pages[1] : doc.Pages.Add();

            // Create a Graph container (size is arbitrary, it defines the drawing canvas)
            Graph graph = new Graph(400, 300);

            // Create an ellipse: left=100, bottom=100, width=200, height=100
            Ellipse ellipse = new Ellipse(100, 100, 200, 100);

            // Set visual properties via GraphInfo
            ellipse.GraphInfo = new GraphInfo
            {
                FillColor = Color.LightBlue,
                Color = Color.DarkBlue,
                LineWidth = 2,
                // Rotate the ellipse 45 degrees
                RotationAngle = 45
            };

            // Alternatively, demonstrate creation of a rotation matrix (not directly applied to the shape)
            // Matrix rotationMatrix = Matrix.Rotation(Math.PI / 4); // 45 degrees in radians
            // The matrix can be used for other transformations if needed.

            // Add the ellipse to the graph
            graph.Shapes.Add(ellipse);

            // Add the graph to the page's paragraphs collection
            page.Paragraphs.Add(graph);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Ellipse rotated 45° and saved to '{outputPath}'.");
    }
}