using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: <program> <input-pdf-path> [output-pdf-path]");
            return;
        }

        string inputPath = args[0];
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPath}");
            return;
        }

        // Determine output path (optional second argument, otherwise add suffix)
        string outputPath = args.Length > 1
            ? args[1]
            : System.IO.Path.Combine(
                System.IO.Path.GetDirectoryName(inputPath) ?? string.Empty,
                System.IO.Path.GetFileNameWithoutExtension(inputPath) + "_with_graph.pdf");

        try
        {
            // Load the existing PDF document (lifecycle rule: use Document constructor)
            using (Document doc = new Document(inputPath))
            {
                // Create a Graph object (width = 400 points, height = 200 points)
                Graph graph = new Graph(400.0, 200.0); // double literals as required

                // ----- Add a rectangle shape -----
                // Aspose.Pdf.Drawing.Rectangle constructor: (left, bottom, width, height)
                Aspose.Pdf.Drawing.Rectangle rectShape = new Aspose.Pdf.Drawing.Rectangle(0f, 0f, 200f, 100f);
                rectShape.GraphInfo = new GraphInfo
                {
                    FillColor = Aspose.Pdf.Color.LightGray,
                    Color = Aspose.Pdf.Color.Black,
                    LineWidth = 2f
                };
                graph.Shapes.Add(rectShape);

                // ----- Add a line shape -----
                // Line constructor expects a float array: { x1, y1, x2, y2 }
                float[] linePoints = { 0f, 0f, 300f, 100f };
                Line lineShape = new Line(linePoints);
                lineShape.GraphInfo = new GraphInfo
                {
                    Color = Aspose.Pdf.Color.Red,
                    LineWidth = 2f
                };
                graph.Shapes.Add(lineShape);

                // Add the graph to the first page's paragraph collection
                // Page indexing in Aspose.Pdf is 1‑based (lifecycle rule)
                Page firstPage = doc.Pages[1];
                firstPage.Paragraphs.Add(graph);

                // Save the modified document (lifecycle rule: use Document.Save)
                doc.Save(outputPath);
            }

            Console.WriteLine($"Graph added successfully. Output saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }
}
