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

            // Create a Graph with double dimensions (the float overload is obsolete)
            Graph graph = new Graph(200.0, 100.0);

            // Create a rectangle shape for the graph (must be Aspose.Pdf.Drawing.Rectangle)
            var rect = new Aspose.Pdf.Drawing.Rectangle(0f, 0f, 200f, 100f);
            rect.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.LightGray,
                Color = Aspose.Pdf.Color.Black,
                LineWidth = 1f
            };
            graph.Shapes.Add(rect);

            // Apply non‑uniform scaling to the whole graph
            graph.GraphInfo.ScalingRateX = 2.0; // stretch width
            graph.GraphInfo.ScalingRateY = 0.5; // compress height

            // Add the graph to the page
            page.Paragraphs.Add(graph);

            // Save the PDF
            doc.Save("scaled_graph.pdf");
        }

        Console.WriteLine("PDF with non‑uniformly scaled graph saved as 'scaled_graph.pdf'.");
    }
}
