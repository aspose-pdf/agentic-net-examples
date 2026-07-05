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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Create a Graph container (width, height) to hold vector shapes
            // Use double literals as required by the newer constructor
            Graph graph = new Graph(400.0, 200.0);

            // Example shape: a rectangle (use Aspose.Pdf.Drawing.Rectangle, not Aspose.Pdf.Rectangle)
            var rect = new Aspose.Pdf.Drawing.Rectangle(50f, 50f, 100f, 50f);
            // Set visual properties via GraphInfo (LineWidth is a float)
            rect.GraphInfo = new GraphInfo
            {
                FillColor = Color.LightGray,
                Color = Color.Black,
                LineWidth = 2f
            };
            graph.Shapes.Add(rect);

            // Apply rotation to the entire graph element.
            // RotationAngle expects degrees (double). Here we rotate 45°.
            graph.GraphInfo.RotationAngle = 45.0;

            // Add the graph to the page's content
            page.Paragraphs.Add(graph);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved to '{outputPath}'.");
    }
}
