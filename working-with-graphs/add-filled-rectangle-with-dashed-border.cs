using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "graph_with_rectangle.pdf";

        using (Document doc = new Document())
        {
            // Add a new page to the document
            Page page = doc.Pages.Add();

            // Create a Graph container with explicit width and height (the coordinate space)
            // Width = UpperRightX - LowerLeftX, Height = UpperRightY - LowerLeftY
            Graph graph = new Graph(300, 300); // 300 points wide, 300 points high

            // Define a rectangle shape inside the graph coordinate space
            // Parameters: lower‑left X, lower‑left Y, upper‑right X, upper‑right Y
            Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(50, 150, 250, 50);

            // Styling via GraphInfo
            rect.GraphInfo.FillColor = Color.LightBlue;   // Fill colour
            rect.GraphInfo.Color = Color.Black;          // Border colour
            rect.GraphInfo.LineWidth = 2;                // Border width
            rect.GraphInfo.DashArray = new int[] { 4, 2 }; // 4‑point dash, 2‑point gap
            rect.GraphInfo.DashPhase = 0;

            // Add the rectangle to the graph
            graph.Shapes.Add(rect);

            // Add the graph to the page
            page.Paragraphs.Add(graph);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
