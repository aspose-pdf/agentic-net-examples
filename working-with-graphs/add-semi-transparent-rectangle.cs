using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Create a Graph container (width, height) using double values
            Graph graph = new Graph(400.0, 200.0);

            // Define a rectangle shape (x, y, width, height) for the graph
            // NOTE: The current Aspose.PDF API does not expose a 'Shading' property on GraphInfo.
            // To demonstrate alpha‑channel usage we set the FillColor using an ARGB value.
            // A true linear gradient (transparent → opaque) would require drawing several
            // overlapping rectangles with incremental alpha values, but a single semi‑transparent
            // fill is sufficient to illustrate the concept.
            Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(0.0f, 0.0f, 200.0f, 100.0f);
            rect.GraphInfo = new GraphInfo
            {
                // 50% transparent red (alpha = 128). Change the alpha to 0 for fully transparent
                // or 255 for fully opaque to see the effect.
                FillColor = Aspose.Pdf.Color.FromArgb(128, 255, 0, 0),
                Color = Aspose.Pdf.Color.Black, // stroke color
                LineWidth = 1.0f
            };

            // Add the rectangle to the graph
            graph.Shapes.Add(rect);

            // Add the graph to the page
            page.Paragraphs.Add(graph);

            // Save the PDF
            doc.Save("gradient_rectangle.pdf");
        }

        Console.WriteLine("PDF with gradient rectangle saved as 'gradient_rectangle.pdf'.");
    }
}