using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: <program> <input-pdf-path> [output-pdf-path]");
            return;
        }

        string inputPath = args[0];
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Determine output path
        string outputPath;
        if (args.Length >= 2 && !string.IsNullOrWhiteSpace(args[1]))
        {
            outputPath = args[1];
        }
        else
        {
            // Default to "output.pdf" in the same directory as the input
            string dir = System.IO.Path.GetDirectoryName(inputPath) ?? Directory.GetCurrentDirectory();
            outputPath = System.IO.Path.Combine(dir, "output.pdf");
        }

        try
        {
            // Load the PDF document (lifecycle rule: use using for disposal)
            using (Document doc = new Document(inputPath))
            {
                // Ensure there is at least one page
                if (doc.Pages.Count == 0)
                {
                    // Add a blank page if the document is empty
                    doc.Pages.Add();
                }

                // Get the first page (Aspose.Pdf uses 1‑based indexing)
                Page page = doc.Pages[1];

                // Create a Graph container (width: 400 points, height: 200 points)
                Graph graph = new Graph(400.0, 200.0);

                // Add a rectangle shape (drawing rectangle, not PDF page rectangle)
                Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(0f, 0f, 200f, 100f);
                rect.GraphInfo.FillColor = Aspose.Pdf.Color.LightGray;
                rect.GraphInfo.Color = Aspose.Pdf.Color.Black;
                rect.GraphInfo.LineWidth = 2f;
                graph.Shapes.Add(rect);

                // Add an ellipse shape
                Aspose.Pdf.Drawing.Ellipse ellipse = new Aspose.Pdf.Drawing.Ellipse(220f, 0f, 200f, 100f);
                ellipse.GraphInfo.FillColor = Aspose.Pdf.Color.Yellow;
                ellipse.GraphInfo.Color = Aspose.Pdf.Color.Red;
                ellipse.GraphInfo.LineWidth = 1.5f;
                graph.Shapes.Add(ellipse);

                // Add a line shape
                float[] linePoints = { 0f, 150f, 400f, 150f };
                Aspose.Pdf.Drawing.Line line = new Aspose.Pdf.Drawing.Line(linePoints);
                line.GraphInfo.Color = Aspose.Pdf.Color.Blue;
                line.GraphInfo.LineWidth = 3f;
                graph.Shapes.Add(line);

                // Add the graph to the page's paragraph collection
                page.Paragraphs.Add(graph);

                // Save the modified document (lifecycle rule: use Save without extra options for PDF)
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
