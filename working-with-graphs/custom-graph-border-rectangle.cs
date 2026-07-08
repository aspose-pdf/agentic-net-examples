using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Annotations; // optional, kept for completeness

class Program
{
    static void Main()
    {
        const string outputPath = "custom_border.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page
            Page page = doc.Pages.Add();

            // Create a Graph container (width: 400pt, height: 200pt) – use double literals as required by the API
            Graph graph = new Graph(400.0, 200.0);

            // Configure GraphInfo for line width and dash pattern
            GraphInfo gInfo = new GraphInfo
            {
                LineWidth = 2f,                     // Set line width
                DashArray = new int[] { 6, 3 }      // Dash length 6pt, gap 3pt
            };

            // Apply the GraphInfo to the Graph's border using BorderInfo (all four sides)
            graph.Border = new BorderInfo(BorderSide.All, gInfo);

            // OPTIONAL: set a background fill for the graph (e.g., light gray)
            graph.GraphInfo.FillColor = Color.LightGray;

            // Create a rectangle shape inside the graph
            // Parameters: left, bottom, width, height (float values)
            var rect = new Aspose.Pdf.Drawing.Rectangle(50f, 50f, 300f, 100f);
            rect.GraphInfo = new GraphInfo
            {
                FillColor = Color.Yellow,   // Fill color
                Color = Color.DarkBlue,     // Stroke color
                LineWidth = 1.5f            // Stroke width
            };

            // Add the rectangle to the graph
            graph.Shapes.Add(rect);

            // Add the graph to the page's paragraphs
            page.Paragraphs.Add(graph);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
