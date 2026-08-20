using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "rectangle_with_shadow.pdf";

        // Create a new PDF document inside a using block for proper disposal
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Create a Graph container that will hold the shapes
            // Width and height define the drawing area (in points)
            // Use double parameters as the float overload is obsolete
            Graph graph = new Graph(500.0, 400.0);

            // Define shadow rectangle (slightly offset, semi‑transparent)
            // Constructor: left, bottom, width, height
            Aspose.Pdf.Drawing.Rectangle shadowRect = new Aspose.Pdf.Drawing.Rectangle(105, 195, 200, 100);
            shadowRect.GraphInfo = new GraphInfo
            {
                // 50 % transparent gray using ARGB (alpha, red, green, blue)
                FillColor = Aspose.Pdf.Color.FromArgb(128, 128, 128, 128)
            };

            // Define the main rectangle (opaque)
            Aspose.Pdf.Drawing.Rectangle mainRect = new Aspose.Pdf.Drawing.Rectangle(100, 200, 200, 100);
            mainRect.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.Blue // fully opaque
            };

            // Add the shadow first so it appears behind the main rectangle
            graph.Shapes.Add(shadowRect);
            graph.Shapes.Add(mainRect);

            // Add the Graph to the page
            page.Paragraphs.Add(graph);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
