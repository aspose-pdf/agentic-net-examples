using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "scaled_graph.pdf";

        // Create a new PDF document inside a using block for deterministic disposal.
        using (Document doc = new Document())
        {
            // Add a page (first page is at index 1 because Aspose.Pdf uses 1‑based indexing).
            Page page = doc.Pages.Add();

            // Create a simple graph (chart) with sample data.
            // Use the double‑based constructor to avoid the obsolete warning.
            Graph graph = new Graph(400.0, 300.0); // width = 400, height = 300
            graph.Left = 100; // X position on the page
            graph.Top = 500;  // Y position on the page (distance from bottom)
            graph.Title = new TextFragment("Sample Graph");
            // Axis titles are not available in the current Graph API version, so they are omitted.

            // Add a data series as a rectangle shape (Aspose.Pdf Graph does not have a Series collection).
            // For demonstration we add a simple rectangle shape representing a bar.
            Aspose.Pdf.Drawing.Rectangle bar = new Aspose.Pdf.Drawing.Rectangle(20, 0, 40, 120);
            bar.Text = new TextFragment("Q1");
            bar.GraphInfo.FillColor = Aspose.Pdf.Color.FromRgb(0.2, 0.6, 0.8);
            bar.GraphInfo.Color = Aspose.Pdf.Color.Black; // stroke colour
            graph.Shapes.Add(bar);

            // Desired non‑uniform scaling factors.
            double scaleX = 1.5; // stretch 150 % on X axis
            double scaleY = 0.7; // compress to 70 % on Y axis

            // Apply non‑uniform scaling via GraphInfo.
            graph.GraphInfo.ScalingRateX = scaleX;
            graph.GraphInfo.ScalingRateY = scaleY;

            // Add the graph to the page's paragraphs collection.
            page.Paragraphs.Add(graph);

            // Save the document as PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with scaled graph saved to '{outputPath}'.");
    }
}
