using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Output PDF path
        const string outputPath = "gradient_background.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a BackgroundArtifact
            BackgroundArtifact bgArtifact = new BackgroundArtifact
            {
                // Place the artifact behind page contents
                IsBackground = true,
                // Set opacity (0..1). 1 = fully opaque
                Opacity = 1.0,
                // Solid background color as a placeholder (gradient not directly supported via this property)
                BackgroundColor = Color.LightGray
            };

            // Add the artifact to the page's artifact collection
            page.Artifacts.Add(bgArtifact);

            // OPTIONAL: If you need a gradient fill, you can create a Form (XForm) with a rectangle
            // that uses a shading pattern and assign it to the artifact via SetPdfPage.
            // The following demonstrates creating a simple rectangle shape; replace with gradient logic as needed.
            /*
            Graph graph = new Graph(500, 800);
            Rectangle rect = new Rectangle(0, 0, 500, 800);
            // Example: set a solid fill; replace with gradient fill using appropriate API if available
            rect.GraphInfo = new GraphInfo
            {
                FillColor = Color.LightBlue,
                Color = Color.Blue,
                LineWidth = 0
            };
            graph.Shapes.Add(rect);
            // Convert the graph to a form and assign it to the artifact
            // Note: This requires additional steps to create an XForm; omitted for brevity.
            */

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with background artifact saved to '{outputPath}'.");
    }
}