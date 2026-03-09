using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";   // source PDF
        const string outputPath = "output.pdf"; // result PDF
        const int    pageNumber = 1;            // target page (1‑based)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (wrapped in using for proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Ensure the requested page exists
            if (pageNumber < 1 || pageNumber > doc.Pages.Count)
            {
                Console.Error.WriteLine($"Page {pageNumber} is out of range. Document has {doc.Pages.Count} pages.");
                return;
            }

            Page page = doc.Pages[pageNumber];

            // Create a Graph container (size can be larger than the arc bounds)
            // Graph constructor now expects double values.
            Graph graph = new Graph(500.0, 500.0);

            // Create an Arc shape.
            // Supported constructor: Arc(float left, float bottom, float width, float startAngle, float sweepAngle)
            // Use float literals (suffix 'F') for the parameters.
            Arc arc = new Arc(100.0F, 200.0F, 300.0F, 0.0F, 180.0F)
            {
                GraphInfo = new GraphInfo
                {
                    Color     = Aspose.Pdf.Color.Blue, // stroke color
                    LineWidth = 2                         // line thickness
                }
            };

            // Add the Arc to the Graph
            graph.Shapes.Add(arc);

            // Add the Graph to the page's content
            page.Paragraphs.Add(graph);

            // Save the modified document as PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Arc inserted and saved to '{outputPath}'.");
    }
}
