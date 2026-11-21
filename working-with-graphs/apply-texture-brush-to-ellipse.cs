using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "ellipse_texture.pdf";
        const string imagePath = "pattern.png";

        // Verify that the texture image exists
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image not found: {imagePath}");
            return;
        }

        // Create a new PDF document (wrapped in using for deterministic disposal)
        using (Document doc = new Document())
        {
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // Set the page background to the texture image – this acts as a fill pattern
            page.BackgroundImage = new Image { File = imagePath };

            // Create a Graph container that will hold drawing shapes
            Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

            // Define the ellipse bounds (left, bottom, width, height)
            // Original rectangle (100, 500, 400, 800) => left=100, bottom=500, width=300, height=300
            Ellipse ellipse = new Ellipse(100f, 500f, 300f, 300f);

            // Style the ellipse (stroke only – the background image provides the "fill")
            ellipse.GraphInfo.Color = Color.Black; // outline color
            ellipse.GraphInfo.LineWidth = 1f;

            // Add the ellipse to the graph, then the graph to the page
            graph.Shapes.Add(ellipse);
            page.Paragraphs.Add(graph);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
