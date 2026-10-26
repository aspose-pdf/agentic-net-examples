using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdfPath   = "input.pdf";          // source PDF containing the graph
        const string outputPdfPath  = "scaled_graph.pdf";   // result PDF
        const string graphImagePath = "graph.png";          // external image of the graph (if replacing)

        // Desired non‑uniform scaling factors
        const double scaleX = 1.5; // stretch horizontally by 150%
        const double scaleY = 0.75; // compress vertically to 75%

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Load the existing PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Assume the graph is an image on the first page.
            // We'll replace it with a scaled version using ImageStamp.
            Page page = doc.Pages[1];

            // Load the graph image to obtain its original dimensions.
            // System.Drawing.Image provides Width and Height in pixels.
            double originalWidth;
            double originalHeight;
            using (System.Drawing.Image sysImg = System.Drawing.Image.FromFile(graphImagePath))
            {
                originalWidth  = sysImg.Width;   // pixels
                originalHeight = sysImg.Height;  // pixels
            }

            // Create an ImageStamp for the graph and apply non‑uniform scaling.
            ImageStamp graphStamp = new ImageStamp(graphImagePath)
            {
                // Apply non‑uniform scaling by setting Width and Height separately.
                Width  = originalWidth  * scaleX,
                Height = originalHeight * scaleY,

                // Position the stamp – here we center it on the page.
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Center,

                // Ensure the stamp is placed above existing content.
                Background = false
            };

            // Add the scaled stamp to the page.
            page.AddStamp(graphStamp);

            // Save the modified PDF.
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Scaled graph PDF saved to '{outputPdfPath}'.");
    }
}
