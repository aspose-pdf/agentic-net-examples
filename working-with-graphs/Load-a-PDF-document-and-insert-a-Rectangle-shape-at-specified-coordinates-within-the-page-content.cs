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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Define the rectangle shape (left, bottom, width, height)
            // Adjust these values to the desired coordinates
            Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(100, 500, 200, 100);

            // Set visual appearance via GraphInfo (fill, stroke, line width)
            rect.GraphInfo = new Aspose.Pdf.GraphInfo
            {
                FillColor = Aspose.Pdf.Color.LightGray,
                Color = Aspose.Pdf.Color.Black,
                LineWidth = 2
            };

            // Create a Graph container; size can match the page dimensions
            Aspose.Pdf.Drawing.Graph graph = new Aspose.Pdf.Drawing.Graph(page.Rect.Width, page.Rect.Height);

            // Add the rectangle shape to the graph
            graph.Shapes.Add(rect);

            // Insert the graph into the page's content
            page.Paragraphs.Add(graph);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Rectangle added and saved to '{outputPath}'.");
    }
}