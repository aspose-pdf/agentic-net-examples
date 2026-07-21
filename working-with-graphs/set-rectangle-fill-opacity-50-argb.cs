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
            // Add a page
            Page page = doc.Pages.Add();

            // Create a Graph container (size can be larger than the rectangle)
            // The Graph constructor expects double values.
            Graph graph = new Graph(400.0, 200.0);

            // Define a rectangle shape (left, bottom, width, height).
            // Use Aspose.Pdf.Drawing.Rectangle – it implements Shape and has a GraphInfo property.
            var rect = new Aspose.Pdf.Drawing.Rectangle(50f, 50f, 150f, 100f);
            // Width = 200 - 50 = 150, Height = 150 - 50 = 100

            // Set visual properties via GraphInfo.
            // Use an ARGB color with 50% opacity (alpha = 128 out of 255).
            rect.GraphInfo = new GraphInfo
            {
                FillColor = Color.FromArgb(128, 255, 0, 0), // semi‑transparent red
                Color = Color.Black,                       // border color
                LineWidth = 1f
            };

            // Add the rectangle to the graph and the graph to the page
            graph.Shapes.Add(rect);
            page.Paragraphs.Add(graph);

            // Verify the fill color (the Color object's ToString includes ARGB info)
            Console.WriteLine("Rectangle fill color (ARGB): " + rect.GraphInfo.FillColor);

            // Save the PDF
            doc.Save("output.pdf");
        }

        Console.WriteLine("PDF with semi‑transparent rectangle saved as 'output.pdf'.");
    }
}
