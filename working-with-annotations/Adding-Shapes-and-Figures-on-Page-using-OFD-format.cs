using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using DrawingRectangle = Aspose.Pdf.Drawing.Rectangle; // Alias to disambiguate

class Program
{
    static void Main()
    {
        const string inputOfd = "input.ofd";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputOfd))
        {
            Console.Error.WriteLine($"File not found: {inputOfd}");
            return;
        }

        // Load OFD (input‑only format) using the appropriate load options
        using (Document doc = new Document(inputOfd, new OfdLoadOptions()))
        {
            // Ensure there is at least one page to work with
            if (doc.Pages.Count == 0)
                doc.Pages.Add();

            // 1‑based page indexing
            Page page = doc.Pages[1];

            // Create a Graph container sized to the page
            double pageWidth = page.Rect.URX - page.Rect.LLX;
            double pageHeight = page.Rect.URY - page.Rect.LLY;
            Graph graph = new Graph(pageWidth, pageHeight);

            // ----- Aspose.Pdf.Drawing.Rectangle (shape) -----
            DrawingRectangle rectShape = new DrawingRectangle(100, 500, 200, 100);
            rectShape.GraphInfo = new GraphInfo
            {
                FillColor = Color.LightGray,
                Color = Color.Black,
                LineWidth = 2
            };
            graph.Shapes.Add(rectShape);

            // ----- Ellipse -----
            Ellipse ellipseShape = new Ellipse(300, 500, 150, 100);
            ellipseShape.GraphInfo = new GraphInfo
            {
                FillColor = Color.Yellow,
                Color = Color.Red,
                LineWidth = 1.5f
            };
            graph.Shapes.Add(ellipseShape);

            // ----- Line -----
            float[] linePoints = { 100F, 400F, 300F, 400F };
            Line lineShape = new Line(linePoints);
            lineShape.GraphInfo = new GraphInfo
            {
                Color = Color.Blue,
                LineWidth = 2
            };
            graph.Shapes.Add(lineShape);

            // Add the graph (containing all shapes) to the page
            page.Paragraphs.Add(graph);

            // OFD cannot be saved; save the modified document as PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Shapes added and saved to '{outputPdf}'.");
    }
}