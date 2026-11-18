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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Create a Graph container that spans the page size
            Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

            // Define rectangle using left, bottom, width, height (absolute coordinates)
            // lower‑left (100,100), upper‑right (300,200) => width = 200, height = 100
            Aspose.Pdf.Drawing.Rectangle rectShape = new Aspose.Pdf.Drawing.Rectangle(100f, 100f, 200f, 100f);

            // Set a solid red fill color
            rectShape.GraphInfo.FillColor = Aspose.Pdf.Color.Red;
            // Optional: set border color and width
            rectShape.GraphInfo.Color = Aspose.Pdf.Color.Black;
            rectShape.GraphInfo.LineWidth = 1f;

            // Add the rectangle to the graph, then the graph to the page
            graph.Shapes.Add(rectShape);
            page.Paragraphs.Add(graph);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Rectangle added and saved to '{outputPath}'.");
    }
}
