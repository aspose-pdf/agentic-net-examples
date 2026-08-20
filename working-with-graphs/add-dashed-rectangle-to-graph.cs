using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "output.pdf";

        // Document lifecycle must be managed with a using block
        using (Document doc = new Document())
        {
            // Add a new page to the document
            Page page = doc.Pages.Add();

            // Create a Graph container (width, height) – this holds vector shapes
            // Use the double‑based constructor (the float overload is obsolete)
            Graph graph = new Graph(400.0, 200.0)
            {
                // Position the graph on the page (optional)
                Left = 100,
                Top  = 500
            };

            // Create a rectangle shape for the graph.
            // Parameters: left, bottom, width, height (all in points)
            Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(0f, 0f, 200f, 100f);

            // Configure the rectangle's border:
            // - 2‑point thickness
            // - dashed line style (dash length 5, gap length 5)
            // - black border color
            rect.GraphInfo = new GraphInfo
            {
                Color     = Aspose.Pdf.Color.Black,
                LineWidth = 2f,
                DashArray = new int[] { 5, 5 }
            };

            // Add the rectangle to the graph's shape collection
            graph.Shapes.Add(rect);

            // Add the graph to the page's paragraph collection
            page.Paragraphs.Add(graph);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
