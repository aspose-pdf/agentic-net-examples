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

            // Desired non‑uniform scaling factors
            // For example: stretch width by 1.5 and compress height by 0.75
            double scaleX = 1.5;
            double scaleY = 0.75;

            // Create a Graph container (width = 400 pt, height = 200 pt)
            // Use the double‑based constructor as the float overload is obsolete
            Graph graph = new Graph(400.0, 200.0);

            // Configure non‑uniform scaling via GraphInfo
            graph.GraphInfo = new GraphInfo
            {
                ScalingRateX = scaleX, // X‑axis scaling factor
                ScalingRateY = scaleY, // Y‑axis scaling factor
                FillColor = Color.LightGray,
                Color = Color.Black,
                LineWidth = 1f
            };

            // Add a simple shape (a rectangle) to illustrate the scaling effect
            // Use Aspose.Pdf.Drawing.Rectangle (not Aspose.Pdf.Rectangle)
            var rect = new Aspose.Pdf.Drawing.Rectangle(0f, 0f, 200f, 100f);
            rect.GraphInfo = new GraphInfo
            {
                FillColor = Color.Yellow,
                Color = Color.DarkBlue,
                LineWidth = 2f
            };
            graph.Shapes.Add(rect);

            // Position the graph on the page (lower‑left corner at (50, 500))
            // Graph does not expose a Bottom property; use Top instead.
            graph.Left = 50f;
            graph.Top = 500f;

            // Add the graph to the page's paragraph collection
            page.Paragraphs.Add(graph);

            // Save the PDF
            string outputPath = "scaled_graph.pdf";
            doc.Save(outputPath);
            Console.WriteLine($"PDF saved to '{outputPath}'.");
        }
    }
}
