using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Create a Graph container – it acts as a canvas for vector shapes
            Graph graph = new Graph(500, 400); // width, height of the canvas

            // Create an ellipse: left, bottom, width, height
            Ellipse ellipse = new Ellipse(100, 200, 150, 100);

            // Set visual properties via GraphInfo (FillColor, Stroke Color, LineWidth)
            ellipse.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.LightGray,
                Color    = Aspose.Pdf.Color.DarkBlue,
                LineWidth = 2
            };

            // Add the ellipse to the graph
            graph.Shapes.Add(ellipse);

            // Add the graph (with the ellipse) to the page's paragraph collection
            page.Paragraphs.Add(graph);

            // Save the modified document (PDF format)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Ellipse added and saved to '{outputPath}'.");
    }
}