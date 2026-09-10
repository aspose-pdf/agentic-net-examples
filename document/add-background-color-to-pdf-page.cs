using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Choose the page to modify (first page in this example)
            Page page = doc.Pages[1];

            // Create a Graph container (size can be larger than the page)
            Graph graph = new Graph(page.Rect.Width, page.Rect.Height);

            // Define a rectangle that covers the whole page
            // Cast double values to float because Aspose.Pdf.Drawing.Rectangle expects float parameters.
            Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(
                (float)page.Rect.LLX,   // left
                (float)page.Rect.LLY,   // bottom
                (float)page.Rect.Width, // width
                (float)page.Rect.Height // height
            );

            // Set visual properties via GraphInfo.
            // Use FromArgb(alpha, r, g, b) where alpha 0-255 defines opacity.
            // Example: 128 (≈50% opacity) semi‑transparent blue.
            rect.GraphInfo = new GraphInfo
            {
                FillColor = Color.FromArgb(128, 0, 0, 255), // semi‑transparent blue
                Color     = Color.Empty, // no stroke
                LineWidth = 0f
            };

            // Add the rectangle shape to the graph
            graph.Shapes.Add(rect);

            // Add the graph to the page's paragraphs collection
            page.Paragraphs.Add(graph);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Background color added and saved to '{outputPath}'.");
    }
}
