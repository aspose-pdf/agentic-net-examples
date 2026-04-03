using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main(string[] args)
    {
        // Expect at least two arguments: input PDF path and output PDF path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: <program> <input-pdf-path> <output-pdf-path>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the existing PDF document
            using (Document doc = new Document(inputPath))
            {
                // Ensure there is at least one page to work with
                if (doc.Pages.Count == 0)
                {
                    // If the document is empty, add a new page
                    doc.Pages.Add();
                }

                // Use the first page (1‑based indexing)
                Page page = doc.Pages[1];

                // Create a Graph object (width, height) – using double literals as required
                Graph graph = new Graph(500.0, 800.0);

                // Example: add a diagonal line to the graph
                float[] linePoints = { 100f, 100f, 400f, 400f };
                Line line = new Line(linePoints);
                line.GraphInfo = new GraphInfo
                {
                    Color = Color.Blue,
                    LineWidth = 2
                };
                graph.Shapes.Add(line);

                // Add the graph to the page's paragraph collection
                page.Paragraphs.Add(graph);

                // Save the modified document as PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Graph added and PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}