using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // If the input file does not exist, create a simple one‑page PDF.
        if (!File.Exists(inputPath))
        {
            using (Document doc = new Document())
            {
                doc.Pages.Add();
                doc.Save(inputPath);
            }
        }

        // Open the PDF, modify it, and save.
        using (Document doc = new Document(inputPath))
        {
            // Get the first page.
            Page page = doc.Pages[1];

            // Create a Graph container that covers the whole page.
            Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

            // Create an ellipse (left, bottom, width, height).
            Ellipse ellipse = new Ellipse(100, 400, 200, 100);

            // Build a rotation matrix for 45 degrees (π/4 radians).
            Matrix rotationMatrix = Matrix.Rotation(Math.PI / 4);

            // Apply the rotation to the ellipse via its GraphInfo.
            // GraphInfo.RotationAngle expects degrees.
            ellipse.GraphInfo = new GraphInfo
            {
                FillColor = Color.LightGray,
                Color = Color.Black,
                LineWidth = 2,
                RotationAngle = 45 // rotate 45°
            };

            // (Optional) Demonstrate using the matrix to transform the bounding rectangle.
            // Rectangle rect = new Rectangle(100, 400, 300, 500);
            // Rectangle rotatedRect = rotationMatrix.Transform(rect);
            // ellipse.Left = rotatedRect.LLX;
            // ellipse.Bottom = rotatedRect.LLY;
            // ellipse.Width = rotatedRect.Width;
            // ellipse.Height = rotatedRect.Height;

            // Add the ellipse to the graph and the graph to the page.
            graph.Shapes.Add(ellipse);
            page.Paragraphs.Add(graph);

            // Save the updated PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Ellipse rotated 45° added to '{outputPath}'.");
    }
}