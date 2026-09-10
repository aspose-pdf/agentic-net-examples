using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "ellipse_semi_transparent.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the source PDF
        using (Document doc = new Document(inputPath))
        {
            // Create a Graph container (required for vector shapes)
            Graph graph = new Graph(500, 400);

            // Define a semi‑transparent red color (alpha = 128 out of 255 ≈ 0.5)
            // Aspose.Pdf.Color provides a FromArgb method similar to System.Drawing.Color
            Color semiTransparentRed = Color.FromArgb(128, 255, 0, 0);

            // Create the ellipse shape
            Ellipse ellipse = new Ellipse(100, 200, 300, 150);
            ellipse.GraphInfo = new GraphInfo
            {
                FillColor = semiTransparentRed   // semi‑transparent fill
                // Stroke color can be set here if needed, e.g. Color = Color.Black
            };

            // Add the ellipse to the graph and the graph to the first page
            graph.Shapes.Add(ellipse);
            doc.Pages[1].Paragraphs.Add(graph);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Ellipse with semi‑transparent fill saved to '{outputPath}'.");
    }
}