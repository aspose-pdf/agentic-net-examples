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
            // Access the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Create a Graph container that will hold vector shapes.
            // Width and height are set to the page size for full‑page drawing.
            double pageWidth = page.PageInfo.Width;
            double pageHeight = page.PageInfo.Height;
            Graph graph = new Graph(pageWidth, pageHeight);

            // -------------------------------------------------
            // Add a rectangle shape
            // Constructor: (left, bottom, width, height) – all float values
            Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(100f, 500f, 200f, 100f);
            rect.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.LightGray, // Background fill
                Color     = Aspose.Pdf.Color.Black,    // Border color
                LineWidth = 2f                         // Border thickness
            };
            graph.Shapes.Add(rect);

            // -------------------------------------------------
            // Add an ellipse shape
            // Constructor: (left, bottom, width, height) – all float values
            Aspose.Pdf.Drawing.Ellipse ellipse = new Aspose.Pdf.Drawing.Ellipse(350f, 500f, 150f, 100f);
            ellipse.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.Yellow,
                Color     = Aspose.Pdf.Color.Red,
                LineWidth = 1.5f
            };
            graph.Shapes.Add(ellipse);

            // -------------------------------------------------
            // Add a line shape
            // Constructor expects a float array: { x1, y1, x2, y2 }
            float[] linePoints = { 100f, 400f, 300f, 400f };
            Aspose.Pdf.Drawing.Line line = new Aspose.Pdf.Drawing.Line(linePoints);
            line.GraphInfo = new GraphInfo
            {
                Color     = Aspose.Pdf.Color.Blue,
                LineWidth = 2f
            };
            graph.Shapes.Add(line);

            // Attach the Graph (containing all shapes) to the page.
            page.Paragraphs.Add(graph);

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
