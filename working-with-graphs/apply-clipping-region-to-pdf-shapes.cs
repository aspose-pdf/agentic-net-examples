using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Operators;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "clipped_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Work with the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Define the clipping rectangle (coordinates in points)
            // Lower‑left (LLX, LLY) = (100, 500), Upper‑right (URX, URY) = (300, 700)
            Aspose.Pdf.Rectangle clipRect = new Aspose.Pdf.Rectangle(100, 500, 300, 700);

            // Build the clipping path:
            // Move to lower‑left corner
            page.Contents.Add(new MoveTo(clipRect.LLX, clipRect.LLY));
            // Draw lines around the rectangle
            page.Contents.Add(new LineTo(clipRect.URX, clipRect.LLY));
            page.Contents.Add(new LineTo(clipRect.URX, clipRect.URY));
            page.Contents.Add(new LineTo(clipRect.LLX, clipRect.URY));
            // Close the path
            page.Contents.Add(new ClosePath());
            // Apply the clipping path (non‑zero winding rule)
            page.Contents.Add(new Clip());

            // -----------------------------------------------------------------
            // Draw shapes that will be confined to the clipping rectangle
            // -----------------------------------------------------------------
            // Create a Graph container (acts like a paragraph)
            Graph graph = new Graph(500.0, 800.0); // width, height in points

            // Example 1: a light‑gray rectangle inside the clipping area
            Aspose.Pdf.Drawing.Rectangle shapeRect = new Aspose.Pdf.Drawing.Rectangle(120, 520, 150, 100);
            shapeRect.GraphInfo = new GraphInfo
            {
                FillColor = Color.LightGray,
                Color = Color.Black,
                LineWidth = 1f
            };
            graph.Shapes.Add(shapeRect);

            // Example 2: a yellow ellipse inside the clipping area
            Aspose.Pdf.Drawing.Ellipse ellipse = new Aspose.Pdf.Drawing.Ellipse(200, 600, 100, 80);
            ellipse.GraphInfo = new GraphInfo
            {
                FillColor = Color.Yellow,
                Color = Color.Red,
                LineWidth = 1f
            };
            graph.Shapes.Add(ellipse);

            // Add the graph (and its shapes) to the page
            page.Paragraphs.Add(graph);
            // -----------------------------------------------------------------

            // End the clipping region – subsequent drawing will no longer be clipped
            page.Contents.Add(new EOClip());

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with clipping applied: '{outputPath}'.");
    }
}