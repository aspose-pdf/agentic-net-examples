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

            // Create a Graph container (width and height define the drawing area)
            // Use double literals as the Graph constructor expects double values
            Graph graph = new Graph(300.0, 200.0);

            // Define shadow offset (float values are required for the Rectangle constructor)
            float offsetX = 5f;
            float offsetY = -5f;

            // Shadow rectangle (slightly offset, semi‑transparent appearance)
            Aspose.Pdf.Drawing.Rectangle shadowRect = new Aspose.Pdf.Drawing.Rectangle(
                offsetX,
                offsetY,
                200f,
                100f);
            shadowRect.GraphInfo = new GraphInfo
            {
                // Light gray fill to simulate a shadow; use Aspose.Pdf.Color
                FillColor = Aspose.Pdf.Color.FromRgb(0.8f, 0.8f, 0.8f),
                Color = Aspose.Pdf.Color.Gray,
                LineWidth = 1f
            };
            graph.Shapes.Add(shadowRect);

            // Main rectangle (drawn on top of the shadow)
            Aspose.Pdf.Drawing.Rectangle mainRect = new Aspose.Pdf.Drawing.Rectangle(
                0f,
                0f,
                200f,
                100f);
            mainRect.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.FromRgb(0.2f, 0.6f, 0.9f), // Example fill color
                Color = Aspose.Pdf.Color.Black,
                LineWidth = 1f
            };
            graph.Shapes.Add(mainRect);

            // Add the Graph to the page
            page.Paragraphs.Add(graph);

            // Save the PDF
            doc.Save("RectangleWithShadow.pdf");
        }

        Console.WriteLine("PDF with rectangle and shadow created successfully.");
    }
}
