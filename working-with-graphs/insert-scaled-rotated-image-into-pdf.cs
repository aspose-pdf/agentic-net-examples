using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing; // ImageStamp resides in this namespace

class Program
{
    static void Main()
    {
        // Input PDF, external image, and output PDF paths
        const string inputPdfPath = "input.pdf";
        const string imagePath = "graph_image.png";
        const string outputPdfPath = "output.pdf";

        // Desired scaling factor (e.g., 0.5 = 50% of original size) and rotation angle in degrees
        const double scaleFactor = 0.5;
        const double rotationAngle = 45.0; // rotate 45 degrees

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPdfPath))
            {
                // Choose the page where the image will be placed (first page in this example)
                Page page = doc.Pages[1];

                // Create an ImageStamp for the external image
                ImageStamp imgStamp = new ImageStamp(imagePath)
                {
                    // Place the stamp over the page content (not as background)
                    Background = false,

                    // Align the stamp to the centre of the page
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,

                    // Rotate the image by the specified angle
                    RotateAngle = rotationAngle,

                    // Scale the image proportionally – use the Zoom property
                    Zoom = scaleFactor
                };

                // Add the configured stamp to the selected page
                page.AddStamp(imgStamp);

                // Save the modified PDF
                doc.Save(outputPdfPath);
            }

            Console.WriteLine($"Image inserted, scaled, and rotated successfully. Output saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
