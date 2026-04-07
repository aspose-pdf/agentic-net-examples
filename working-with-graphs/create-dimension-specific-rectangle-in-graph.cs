using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "rectangle_graph.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a single page to the document
            Page page = doc.Pages.Add();

            // Use the Graph constructor that accepts double values
            Graph graph = new Graph(400.0, 200.0); // width = 400pt, height = 200pt

            // Define a rectangle shape with specific dimensions inside the graph
            // Parameters: left, bottom, width, height (all as float)
            var rect = new Aspose.Pdf.Drawing.Rectangle(50f, 50f, 300f, 100f);

            // Set visual styling via GraphInfo (FillColor, Stroke Color, LineWidth)
            rect.GraphInfo = new GraphInfo
            {
                FillColor = Color.LightGray,   // background fill
                Color = Color.Black,           // border color
                LineWidth = 2f                 // border thickness (float)
            };

            // Add the rectangle shape to the graph's shape collection
            graph.Shapes.Add(rect);

            // Add the graph (containing the rectangle) to the page's content
            page.Paragraphs.Add(graph);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with dimension‑specific rectangle saved to '{outputPath}'.");
    }
}
