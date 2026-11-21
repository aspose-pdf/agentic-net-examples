using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths to the graph images that will be placed on the page
        string[] graphImages = { "graph1.png", "graph2.png", "graph3.png", "graph4.png" };
        const string outputPdf = "collage.pdf";

        // Verify that all image files exist before proceeding
        foreach (var imgPath in graphImages)
        {
            if (!File.Exists(imgPath))
            {
                Console.Error.WriteLine($"Image not found: {imgPath}");
                return;
            }
        }

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a single blank page (default size is A4)
            Page page = doc.Pages.Add();

            // Define absolute positions (left, bottom) for each graph in points
            // A4 dimensions: 595 x 842 points
            var positions = new (double left, double bottom)[]
            {
                (50, 500),   // Position for graph1.png
                (300, 500),  // Position for graph2.png
                (50, 200),   // Position for graph3.png
                (300, 200)   // Position for graph4.png
            };

            // Place each image on the page at its designated coordinates
            for (int i = 0; i < graphImages.Length && i < positions.Length; i++)
            {
                // Create an ImageStamp for the current graph image
                ImageStamp stamp = new ImageStamp(graphImages[i]);

                // Use absolute positioning by disabling alignment
                stamp.HorizontalAlignment = HorizontalAlignment.None;
                stamp.VerticalAlignment   = VerticalAlignment.None;

                // Set the exact location on the page
                stamp.LeftMargin   = positions[i].left;
                stamp.BottomMargin = positions[i].bottom;

                // Optional: add a thin gray border around each image
                // stamp.Border = new Border(stamp) { Width = 1, Color = Aspose.Pdf.Color.Gray };

                // Add the stamp to the page
                page.AddStamp(stamp);
            }

            // Save the resulting PDF collage
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Collage PDF saved to '{outputPdf}'.");
    }
}