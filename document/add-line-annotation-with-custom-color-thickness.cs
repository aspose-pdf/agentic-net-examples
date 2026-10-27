using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Get the first page (Aspose.Pdf uses 1‑based indexing)
                Page page = doc.Pages[1];

                // Create a Graph container that spans the whole page
                Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

                // Define the line geometry (start X,Y and end X,Y)
                // The constructor expects an array: [x1, y1, x2, y2]
                Line line = new Line(new float[] { 100f, 500f, 200f, 500f });

                // Customize appearance: color and thickness
                line.GraphInfo.Color = Color.Blue;
                line.GraphInfo.LineWidth = 2f; // thickness in points

                // Add the line to the graph and the graph to the page
                graph.Shapes.Add(line);
                page.Paragraphs.Add(graph);

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Line annotation added and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
