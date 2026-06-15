using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: <program> <input-pdf> [output-pdf]");
            return;
        }

        // Input PDF path (first argument)
        string inputPath = args[0];
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Determine output path: second argument or auto‑generated name
        string outputPath;
        if (args.Length > 1)
        {
            outputPath = args[1];
        }
        else
        {
            string dir   = System.IO.Path.GetDirectoryName(inputPath);
            string name  = System.IO.Path.GetFileNameWithoutExtension(inputPath);
            outputPath   = System.IO.Path.Combine(dir, $"{name}_graph.pdf");
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Ensure the document has at least one page
                if (doc.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The PDF contains no pages.");
                    return;
                }

                // Work with the first page (1‑based indexing)
                Page page = doc.Pages[1];

                // Create a Graph that covers the whole page size
                Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

                // Define a rectangle shape (left, bottom, width, height)
                Aspose.Pdf.Drawing.Rectangle shapeRect = new Aspose.Pdf.Drawing.Rectangle(100, 500, 200, 100);
                shapeRect.GraphInfo = new GraphInfo
                {
                    FillColor = Color.LightGray,
                    Color     = Color.Black,
                    LineWidth = 2
                };

                // Add the rectangle to the graph's shape collection
                graph.Shapes.Add(shapeRect);

                // Optionally add more shapes (e.g., an ellipse)
                Aspose.Pdf.Drawing.Ellipse ellipse = new Aspose.Pdf.Drawing.Ellipse(350, 500, 150, 100);
                ellipse.GraphInfo = new GraphInfo
                {
                    FillColor = Color.Yellow,
                    Color     = Color.Red,
                    LineWidth = 1.5f
                };
                graph.Shapes.Add(ellipse);

                // Add the completed graph to the page's paragraph collection
                page.Paragraphs.Add(graph);

                // Save the modified document
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