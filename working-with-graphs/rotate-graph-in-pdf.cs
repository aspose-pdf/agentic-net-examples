using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "rotated_graph.pdf";
        const double rotationAngle = 45; // rotation angle in degrees

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Work with the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Create a Graph element at position (100, 500) with size 200x200
            // Use the double‑parameter constructor (the float overload is obsolete)
            Graph graph = new Graph(100.0, 500.0);
            graph.Width = 200;
            graph.Height = 200;

            // Add a simple rectangle shape to the graph for visual reference
            // Fully qualify the drawing Rectangle to avoid ambiguity with Aspose.Pdf.Rectangle
            Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(0, 0, 200, 200);
            rect.GraphInfo.FillColor = Color.LightGray; // fill color
            rect.GraphInfo.Color = Color.Black;          // border color
            rect.GraphInfo.LineWidth = 1;                // border thickness

            // Apply rotation to the rectangle via its GraphInfo (Graph itself has no Transformations)
            rect.GraphInfo.RotationAngle = rotationAngle;

            graph.Shapes.Add(rect);

            // Add the graph to the page's content
            page.Paragraphs.Add(graph);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved rotated graph PDF to '{outputPath}'.");
    }
}
