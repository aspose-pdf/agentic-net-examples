using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath  = "ellipse_texture.pdf";
        const string texturePath = "texture.jpg";

        // Verify that the texture image exists
        if (!File.Exists(texturePath))
        {
            Console.Error.WriteLine($"Texture image not found: {texturePath}");
            return;
        }

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // Set the page background to the texture image – this is the closest
            // approximation to a "texture brush" using the core Aspose.Pdf API.
            page.BackgroundImage = new Image { File = texturePath };

            // Create a Graph container – all drawing shapes must be placed inside a Graph
            Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

            // Define the ellipse bounds (left, bottom, width, height)
            // Original rectangle: left=100, bottom=500, right=400, top=800
            // => width = 400 - 100 = 300, height = 800 - 500 = 300
            Ellipse ellipse = new Ellipse(100, 500, 300, 300);

            // Style the ellipse: thin black border, transparent fill (so the background shows through)
            ellipse.GraphInfo = new GraphInfo
            {
                Color = Color.Black,
                LineWidth = 1,
                FillColor = Color.Transparent
            };

            // Add the ellipse to the graph, then the graph to the page
            graph.Shapes.Add(ellipse);
            page.Paragraphs.Add(graph);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with textured ellipse saved to '{outputPath}'.");
    }
}
