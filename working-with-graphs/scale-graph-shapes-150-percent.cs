using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

// Alias to avoid conflict with Aspose.Pdf.Rectangle (which is a different type)
using DrawRect = Aspose.Pdf.Drawing.Rectangle;

class Program
{
    static void Main()
    {
        const string outputPath = "scaled_shapes.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Create a graph of the desired size (width, height) using the double constructor
            Graph graph = new Graph(500.0, 500.0)
            {
                Left = 0,
                Top = 0,
                Border = new BorderInfo(BorderSide.All, 1f, Color.Black),
                Title = new TextFragment("Scaled Shapes Demo")
            };

            // ----- Rectangle shape -----
            DrawRect rect = new DrawRect(100, 100, 200, 100);
            rect.GraphInfo.FillColor = Color.LightGray;   // fill
            rect.GraphInfo.Color = Color.Black;           // stroke colour
            rect.GraphInfo.LineWidth = 2;                 // stroke width
            // Scale this shape by 150 %
            rect.GraphInfo.ScalingRateX = 1.5f;
            rect.GraphInfo.ScalingRateY = 1.5f;
            graph.Shapes.Add(rect);

            // ----- Ellipse shape -----
            Ellipse ellipse = new Ellipse(150, 250, 200, 150);
            ellipse.GraphInfo.FillColor = Color.FromRgb(0.2, 0.5, 0.8);
            ellipse.GraphInfo.Color = Color.Black;
            ellipse.GraphInfo.LineWidth = 1.5f;
            // Scale this shape by 150 %
            ellipse.GraphInfo.ScalingRateX = 1.5f;
            ellipse.GraphInfo.ScalingRateY = 1.5f;
            graph.Shapes.Add(ellipse);

            // Place the graph onto the page
            page.Paragraphs.Add(graph);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
