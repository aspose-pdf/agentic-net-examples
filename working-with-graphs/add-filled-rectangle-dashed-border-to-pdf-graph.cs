using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Input and output PDF paths
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Open the existing PDF (or create a new one if needed)
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (pages are 1‑based)
            Page page = doc.Pages[1];

            // Create a Graph container with desired size (use double constructor)
            // Width = 200 points, Height = 100 points
            Graph graph = new Graph(200.0, 100.0);

            // Position the graph on the page.
            // Graph uses Left and Top properties (Bottom was removed in newer versions).
            // To place the lower‑left corner at (100, 500) we set Top = bottom + height.
            graph.Left = 100;               // X‑coordinate of the left side
            graph.Top  = 500 + 100;         // Y‑coordinate of the top side (bottom + height)

            // Create a rectangle shape inside the graph
            // Constructor: Rectangle(left, bottom, width, height)
            var rect = new Aspose.Pdf.Drawing.Rectangle(0f, 0f, 200f, 100f);

            // Configure visual appearance via GraphInfo
            rect.GraphInfo = new GraphInfo
            {
                // Fill the rectangle with a light gray color
                FillColor = Aspose.Pdf.Color.LightGray,

                // Border color (stroke) – black in this example
                Color = Aspose.Pdf.Color.Black,

                // Border (stroke) width in points
                LineWidth = 2f,

                // Define dash pattern: 5 points dash, 3 points gap
                DashArray = new int[] { 5, 3 }
            };

            // Add the rectangle shape to the graph
            graph.Shapes.Add(rect);

            // Add the graph to the page's paragraph collection
            page.Paragraphs.Add(graph);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
