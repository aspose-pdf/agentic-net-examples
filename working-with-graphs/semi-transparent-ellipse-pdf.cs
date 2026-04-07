using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "ellipse_semi_transparent.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a Graph container (required for vector shapes)
            // Use the constructor that accepts double values as required by the API.
            Graph graph = new Graph(500.0, 400.0);

            // Create an ellipse shape (left, bottom, width, height)
            // The Ellipse constructor expects integer parameters, so cast to int explicitly.
            Ellipse ellipse = new Ellipse((int)100, (int)150, (int)300, (int)200);

            // Define a semi‑transparent fill color.
            // Aspose.Pdf.Color supports an alpha component via FromArgb with integer ARGB values (0‑255).
            Color semiTransparentRed = Color.FromArgb(128, 255, 0, 0); // 50 % opacity red

            // Apply the fill color via GraphInfo.
            ellipse.GraphInfo = new GraphInfo
            {
                FillColor = semiTransparentRed,
                // Optionally set a stroke color and width (LineWidth expects a float).
                Color = Color.Black,
                LineWidth = 1f
            };

            // Add the ellipse to the graph and the graph to the page
            graph.Shapes.Add(ellipse);
            page.Paragraphs.Add(graph);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with semi‑transparent ellipse saved to '{outputPath}'.");
    }
}
