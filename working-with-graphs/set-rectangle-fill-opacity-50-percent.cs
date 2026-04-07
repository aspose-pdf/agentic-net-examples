using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "rectangle_opacity.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a Graph container that matches the page size
            Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

            // Define a rectangle shape (x, y, width, height) using float values
            Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(100f, 500f, 200f, 100f);

            // Set the rectangle's visual properties:
            // - FillColor with 50% opacity using ARGB (alpha = 128)
            // - Border color (Color) and line width
            rect.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.FromArgb(128, 255, 0, 0), // semi‑transparent red
                Color = Aspose.Pdf.Color.Black,
                LineWidth = 1f
            };

            // Add the rectangle to the graph and the graph to the page
            graph.Shapes.Add(rect);
            page.Paragraphs.Add(graph);

            // Save the PDF document
            doc.Save(outputPath);
        }

        // Simple verification: output the ARGB components used for the fill color
        Aspose.Pdf.Color fill = Aspose.Pdf.Color.FromArgb(128, 255, 0, 0);
        Console.WriteLine($"Fill color ARGB: A=128, R=255, G=0, B=0");
        Console.WriteLine($"PDF saved to '{outputPath}'. Open the file to visually verify 50% opacity.");
    }
}