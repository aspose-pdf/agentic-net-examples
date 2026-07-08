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

        // Load the existing PDF
        using (Document doc = new Document(inputPath))
        {
            // Select the page to modify (first page)
            Page page = doc.Pages[1];

            // Get page dimensions
            double pageWidth = page.Rect.Width;
            double pageHeight = page.Rect.Height;

            // Create a Graph container sized to the page
            Graph graph = new Graph(pageWidth, pageHeight);

            // Create a rectangle that covers the whole page (use drawing rectangle)
            var rect = new Aspose.Pdf.Drawing.Rectangle(
                0f,
                0f,
                (float)pageWidth,
                (float)pageHeight);

            rect.GraphInfo = new GraphInfo
            {
                // Semi‑transparent background (alpha 100 out of 255)
                FillColor = Aspose.Pdf.Color.FromArgb(100, 0, 0, 255),
                // No visible border
                Color = Aspose.Pdf.Color.Transparent,
                LineWidth = 0f
            };

            // Add the rectangle to the graph
            graph.Shapes.Add(rect);

            // Add the graph to the page's content
            page.Paragraphs.Add(graph);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Background color added and saved to '{outputPath}'.");
    }
}