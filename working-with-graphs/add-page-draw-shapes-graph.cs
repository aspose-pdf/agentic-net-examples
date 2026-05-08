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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF (lifecycle rule: use Document constructor)
        using (Document doc = new Document(inputPath))
        {
            // Add a new blank page (page indexing is 1‑based)
            Page newPage = doc.Pages.Add();

            // Create a Graph container (width, height in points)
            double pageWidth = newPage.Rect.Width;
            double pageHeight = newPage.Rect.Height;
            Graph graph = new Graph(pageWidth, pageHeight);

            // ----- Draw a rectangle -----
            // Use Aspose.Pdf.Drawing.Rectangle (not Aspose.Pdf.Rectangle)
            var rect = new Aspose.Pdf.Drawing.Rectangle(100f, 500f, 200f, 100f);
            rect.GraphInfo = new GraphInfo
            {
                FillColor = Color.LightGray,
                Color = Color.Black,
                LineWidth = 2f
            };
            graph.Shapes.Add(rect);

            // ----- Draw an ellipse -----
            var ellipse = new Ellipse(350f, 500f, 150f, 100f);
            ellipse.GraphInfo = new GraphInfo
            {
                FillColor = Color.Yellow,
                Color = Color.Red,
                LineWidth = 1.5f
            };
            graph.Shapes.Add(ellipse);

            // ----- Draw a line -----
            float[] linePoints = { 100f, 400f, 300f, 400f };
            var line = new Line(linePoints);
            line.GraphInfo = new GraphInfo
            {
                Color = Color.Blue,
                LineWidth = 2f
            };
            graph.Shapes.Add(line);

            // Add the graph to the page's paragraph collection
            newPage.Paragraphs.Add(graph);

            // Save the modified PDF (lifecycle rule: use Document.Save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
