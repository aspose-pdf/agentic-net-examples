using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Input and output PDF file paths
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Create a Graph container sized to the page dimensions
            Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

            // Define a rectangle shape with rounded corners
            // Constructor parameters: left, bottom, width, height (all float values)
            var roundedRect = new Aspose.Pdf.Drawing.Rectangle(100f, 500f, 200f, 100f);
            roundedRect.RoundedCornerRadius = 15f; // radius of the corners

            // Set visual appearance via GraphInfo (fill color, border color, line width)
            roundedRect.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.LightGray, // solid fill
                Color = Aspose.Pdf.Color.Black,        // border color
                LineWidth = 2f                         // border thickness
            };

            // Add the rectangle shape to the graph
            graph.Shapes.Add(roundedRect);

            // Add the graph to the page's paragraph collection
            page.Paragraphs.Add(graph);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Rounded rectangle added and saved to '{outputPath}'.");
    }
}