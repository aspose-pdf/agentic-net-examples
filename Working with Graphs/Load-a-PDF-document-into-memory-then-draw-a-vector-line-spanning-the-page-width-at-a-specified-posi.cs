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
        const double lineY      = 400; // Y coordinate from the bottom of the page

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (wrapped in using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Calculate page dimensions
            double pageWidth  = page.Rect.URX - page.Rect.LLX;
            double pageHeight = page.Rect.URY - page.Rect.LLY;

            // Create a Graph container that covers the whole page
            Graph graph = new Graph(pageWidth, pageHeight);

            // Define a line that spans the full width at the specified Y position
            float[] linePoints = {
                (float)page.Rect.LLX, (float)lineY,   // start point (left edge)
                (float)page.Rect.URX, (float)lineY    // end point (right edge)
            };
            Line line = new Line(linePoints)
            {
                GraphInfo = new GraphInfo
                {
                    Color     = Aspose.Pdf.Color.Black,
                    LineWidth = 1
                }
            };

            // Add the line shape to the graph
            graph.Shapes.Add(line);

            // Add the graph to the page's content
            page.Paragraphs.Add(graph);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Vector line drawn and saved to '{outputPath}'.");
    }
}
