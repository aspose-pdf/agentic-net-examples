using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main(string[] args)
    {
        // Expect at least one argument: input PDF path.
        // Optional second argument: output PDF path.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: AddGraph <input-pdf> [output-pdf]");
            return;
        }

        string inputPath = args[0];
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found: {inputPath}");
            return;
        }

        // Determine output path.
        string outputPath = args.Length >= 2
            ? args[1]
            : System.IO.Path.Combine(
                System.IO.Path.GetDirectoryName(inputPath) ?? "",
                System.IO.Path.GetFileNameWithoutExtension(inputPath) + "_with_graph.pdf");

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

                // Use the first page for demonstration.
                Page page = doc.Pages[1];

                // Create a Graph with predefined size (width, height in points).
                // The Graph constructor expects double values.
                Graph graph = new Graph(400.0, 200.0);

                // Define a rectangle shape inside the graph.
                // For graph shapes we must use Aspose.Pdf.Drawing.Rectangle.
                var shapeRect = new Aspose.Pdf.Drawing.Rectangle(0f, 0f, 100f, 50f);
                shapeRect.GraphInfo = new GraphInfo
                {
                    FillColor = Aspose.Pdf.Color.LightGray,
                    Color = Aspose.Pdf.Color.Black,
                    LineWidth = 1f
                };

                // Add the rectangle to the graph's shape collection.
                graph.Shapes.Add(shapeRect);

                // Position the graph on the page.
                graph.Left = 100.0;
                graph.Top = 500.0;

                // Add the graph to the page's paragraphs collection.
                page.Paragraphs.Add(graph);

                // Save the modified document.
                doc.Save(outputPath);
            }

            Console.WriteLine($"Graph added successfully. Saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }
}
