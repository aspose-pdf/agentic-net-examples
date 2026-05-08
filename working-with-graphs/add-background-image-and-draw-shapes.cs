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
        const string bgImagePath = "background.jpg";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }
        if (!File.Exists(bgImagePath))
        {
            Console.Error.WriteLine($"Background image not found: {bgImagePath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Work with the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Apply a background image to the page (drawn behind all other content)
            Aspose.Pdf.Image bg = new Aspose.Pdf.Image();
            bg.File = bgImagePath;
            page.BackgroundImage = bg;

            // Create a graph that spans the whole page
            double pageWidth = page.PageInfo.Width;
            double pageHeight = page.PageInfo.Height;
            Graph graph = new Graph(pageWidth, pageHeight)
            {
                // Ensure the graph is rendered above the background image
                ZIndex = 1
            };

            // Add a rectangle shape on top of the background
            Aspose.Pdf.Drawing.Rectangle rectShape = new Aspose.Pdf.Drawing.Rectangle(100, 500, 200, 100);
            rectShape.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.LightGray,
                Color = Aspose.Pdf.Color.Black,
                LineWidth = 2
            };
            graph.Shapes.Add(rectShape);

            // Add an ellipse shape on top of the background
            Aspose.Pdf.Drawing.Ellipse ellipse = new Aspose.Pdf.Drawing.Ellipse(300, 500, 150, 100);
            ellipse.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.Yellow,
                Color = Aspose.Pdf.Color.Red,
                LineWidth = 1.5f
            };
            graph.Shapes.Add(ellipse);

            // Add a line shape on top of the background
            float[] linePoints = { 50, 700, 400, 750 };
            Aspose.Pdf.Drawing.Line line = new Aspose.Pdf.Drawing.Line(linePoints);
            line.GraphInfo = new GraphInfo
            {
                Color = Aspose.Pdf.Color.Blue,
                LineWidth = 2
            };
            graph.Shapes.Add(line);

            // Insert the graph into the page's content (shapes will appear over the background image)
            page.Paragraphs.Add(graph);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with background image and shapes saved to '{outputPath}'.");
    }
}