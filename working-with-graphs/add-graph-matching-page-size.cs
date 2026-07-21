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

        // Load the PDF document (using statement ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Retrieve the page dimensions from PageInfo
            double pageWidth = page.PageInfo.Width;
            double pageHeight = page.PageInfo.Height;

            // Instantiate a Graph that matches the page size
            Graph graph = new Graph(pageWidth, pageHeight);

            // OPTIONAL: add a shape to the graph (e.g., a rectangle covering the page)
            // Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(0, 0, pageWidth, pageHeight);
            // rect.GraphInfo = new GraphInfo { FillColor = Aspose.Pdf.Color.LightGray };
            // graph.Shapes.Add(rect);

            // Add the Graph to the page's paragraph collection
            page.Paragraphs.Add(graph);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Graph added and saved to '{outputPath}'.");
    }
}