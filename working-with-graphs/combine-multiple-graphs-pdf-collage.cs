using System;
using System.IO;
using Aspose.Pdf;                     // Core PDF API
using Aspose.Pdf.Annotations;        // For stamp annotations (ImageStamp derives from StampAnnotation)

class Program
{
    static void Main()
    {
        // Input graph images to be combined
        string[] graphImages = { "graph1.png", "graph2.png", "graph3.png" };

        // Verify that all image files exist
        foreach (string imgPath in graphImages)
        {
            if (!File.Exists(imgPath))
            {
                Console.Error.WriteLine($"Image not found: {imgPath}");
                return;
            }
        }

        // Output PDF path
        const string outputPdf = "combined_graphs.pdf";

        // Create a new PDF document with a single blank page
        using (Document doc = new Document())
        {
            // Add an empty page (default size A4)
            Page page = doc.Pages.Add();

            // Position each graph image at a different location on the page
            // Coordinates are measured from the bottom‑left corner of the page
            // (X = horizontal distance, Y = vertical distance)
            // Adjust the values as needed for your collage layout
            double[,] positions = new double[,]
            {
                { 50,  500 },   // graph1 at (50, 500)
                { 300, 500 },   // graph2 at (300, 500)
                { 175, 250 }    // graph3 at (175, 250)
            };

            for (int i = 0; i < graphImages.Length; i++)
            {
                // Create an ImageStamp for the current graph image
                ImageStamp imgStamp = new ImageStamp(graphImages[i]);

                // Use absolute positioning: no automatic alignment
                imgStamp.HorizontalAlignment = HorizontalAlignment.None;
                imgStamp.VerticalAlignment   = VerticalAlignment.None;

                // Set the exact X/Y offsets (in points) from the page origin
                imgStamp.XIndent = positions[i, 0];
                imgStamp.YIndent = positions[i, 1];

                // Optional: make the stamp transparent background and set opacity
                imgStamp.Background = false;
                imgStamp.Opacity    = 1.0; // fully opaque; adjust between 0.0 and 1.0 if needed

                // Add the stamp to the page
                page.AddStamp(imgStamp);
            }

            // Save the resulting PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Combined graph PDF saved to '{outputPdf}'.");
    }
}