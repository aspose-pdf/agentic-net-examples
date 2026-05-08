using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main(string[] args)
    {
        // Expect the input PDF path as the first argument.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: AddGraph <input-pdf-path>");
            return;
        }

        string inputPath = args[0];
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Build an output path by appending "_graph" before the extension.
        string dir = System.IO.Path.GetDirectoryName(inputPath) ?? string.Empty;
        string nameWithoutExt = System.IO.Path.GetFileNameWithoutExtension(inputPath);
        string outputPath = System.IO.Path.Combine(dir, $"{nameWithoutExt}_graph.pdf");

        try
        {
            // Load the existing PDF document.
            using (Document doc = new Document(inputPath))
            {
                // Ensure there is at least one page.
                if (doc.Pages.Count == 0)
                {
                    // Add a blank page if the document is empty.
                    doc.Pages.Add();
                }

                // Get the first page (1‑based indexing).
                Page page = doc.Pages[1];

                // Create a Graph container (width: 400 points, height: 200 points).
                Graph graph = new Graph(400.0, 200.0); // double literals as required

                // Define a rectangle shape inside the graph.
                // Constructor parameters: left, bottom, width, height (float).
                var rect = new Aspose.Pdf.Drawing.Rectangle(0f, 0f, 100f, 50f);
                rect.GraphInfo = new GraphInfo
                {
                    FillColor = Aspose.Pdf.Color.LightGray,
                    Color = Aspose.Pdf.Color.Black,
                    LineWidth = 2f
                };

                // Add the rectangle to the graph's shape collection.
                graph.Shapes.Add(rect);

                // Optionally, add more shapes (e.g., a line) to demonstrate the graph.
                float[] linePoints = { 120f, 0f, 300f, 150f };
                Line line = new Line(linePoints);
                line.GraphInfo = new GraphInfo
                {
                    Color = Aspose.Pdf.Color.Red,
                    LineWidth = 1.5f
                };
                graph.Shapes.Add(line);

                // Position the graph on the page by setting its left/top coordinates.
                // The Graph itself is a paragraph, so we can set its margin to position it.
                graph.Margin = new MarginInfo { Left = 50, Top = 500 };

                // Add the graph to the page's paragraphs collection.
                page.Paragraphs.Add(graph);

                // Save the modified document.
                doc.Save(outputPath);
            }

            Console.WriteLine($"Graph added and saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
