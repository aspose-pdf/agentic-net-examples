using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main(string[] args)
    {
        // Expect at least the input PDF path.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: AddGraph <input-pdf> [output-pdf]");
            return;
        }

        string inputPath = args[0];
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Determine output path.
        string outputPath = args.Length >= 2
            ? args[1]
            : System.IO.Path.Combine(
                System.IO.Path.GetDirectoryName(inputPath) ?? string.Empty,
                System.IO.Path.GetFileNameWithoutExtension(inputPath) + "_graph.pdf");

        try
        {
            // Load the PDF document inside a using block for deterministic disposal.
            using (Document doc = new Document(inputPath))
            {
                // Ensure the document has at least one page.
                if (doc.Pages.Count == 0)
                {
                    // Add a blank page if none exist.
                    doc.Pages.Add();
                }

                // Get the first page (1‑based indexing).
                Page page = doc.Pages[1];

                // Create a Graph with desired width and height (points).
                // Width = 400 points, Height = 200 points.
                // Use the double‑based constructor (the float overload is obsolete).
                Graph graph = new Graph(400.0, 200.0);

                // Position the graph on the page by setting its left/top coordinates.
                // Here we place it at (100, 500) in page coordinates.
                graph.Left = 100;
                graph.Top = 500;

                // Create a rectangle shape inside the graph.
                // Constructor parameters: left, bottom, width, height.
                Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(0, 0, 200, 100);
                // Set visual properties via GraphInfo.
                rect.GraphInfo = new GraphInfo
                {
                    FillColor = Aspose.Pdf.Color.LightGray,
                    Color = Aspose.Pdf.Color.Black,
                    LineWidth = 2
                };
                // Add the rectangle to the graph's shape collection.
                graph.Shapes.Add(rect);

                // Optionally add more shapes (e.g., a line) to demonstrate the graph.
                float[] linePoints = { 0, 0, 300, 150 };
                Line line = new Line(linePoints);
                line.GraphInfo = new GraphInfo
                {
                    Color = Aspose.Pdf.Color.Red,
                    LineWidth = 1.5f
                };
                graph.Shapes.Add(line);

                // Add the graph to the page's paragraphs collection.
                page.Paragraphs.Add(graph);

                // Save the modified document.
                doc.Save(outputPath);
            }

            Console.WriteLine($"Graph added and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
