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
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a Graph container (width: 200pt, height: 100pt)
            // Use the double‑based constructor as required by newer versions
            Graph graph = new Graph(200.0, 100.0);

            // Define a rectangle shape inside the graph using the drawing Rectangle type
            // Rectangle constructor expects integer values (or cast to int)
            var rect = new Aspose.Pdf.Drawing.Rectangle(0, 0, 200, 100);
            rect.GraphInfo = new GraphInfo
            {
                // 50% transparent fill color (alpha = 128)
                // Color.FromArgb expects integer RGB components (0‑255)
                FillColor = Color.FromArgb(128, 51, 153, 204), // 0.2*255≈51, 0.6*255≈153, 0.8*255≈204
                // Border color of the rectangle
                Color = Color.Black,
                // Border line width (float literal)
                LineWidth = 1f
            };

            // Add the rectangle to the graph's shape collection
            graph.Shapes.Add(rect);

            // Add the graph (with the semi‑transparent rectangle) to the page
            page.Paragraphs.Add(graph);

            // Save the PDF document
            doc.Save("output.pdf");
        }

        Console.WriteLine("PDF with semi‑transparent shape created successfully.");
    }
}