using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "output.pdf";

        try
        {
            // Create a new PDF document
            using (Document doc = new Document())
            {
                // Add a page (1‑based indexing)
                Page page = doc.Pages.Add();

                // Create a Graph container (width, height)
                Graph graph = new Graph(500, 300);

                // ----- Rectangle shape (fits within the graph) -----
                Aspose.Pdf.Drawing.Rectangle rectShape = new Aspose.Pdf.Drawing.Rectangle(50, 50, 200, 150);
                rectShape.GraphInfo = new GraphInfo
                {
                    FillColor = Color.LightGray,
                    Color = Color.Black,
                    LineWidth = 2
                };

                try
                {
                    graph.Shapes.Add(rectShape);
                }
                catch (BoundsOutOfRangeException ex)
                {
                    // Log coordinates that caused the exception
                    Console.Error.WriteLine($"Rectangle out of bounds: {ex.Message}");
                }

                // ----- Line shape (intentionally exceeds graph bounds) -----
                // Points array: { x1, y1, x2, y2 }
                float[] linePoints = { 400, 250, 600, 350 }; // second point exceeds graph width (500)
                Line lineShape = new Line(linePoints);
                lineShape.GraphInfo = new GraphInfo
                {
                    Color = Color.Red,
                    LineWidth = 3
                };

                try
                {
                    graph.Shapes.Add(lineShape);
                }
                catch (BoundsOutOfRangeException ex)
                {
                    // Log coordinates that caused the exception
                    Console.Error.WriteLine($"Line out of bounds: {ex.Message}");
                }

                // Add the graph (containing the shapes) to the page
                page.Paragraphs.Add(graph);

                // Save the PDF
                doc.Save(outputPath);
                Console.WriteLine($"PDF saved to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}