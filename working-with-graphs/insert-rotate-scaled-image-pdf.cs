using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Input image file, output PDF, desired position, scale factor and rotation angle
        const string imagePath   = "logo.png";
        const string outputPdf   = "graph_with_image.pdf";
        const double posX        = 100;   // X coordinate (points) from left
        const double posY        = 500;   // Y coordinate (points) from bottom
        const double scaleFactor = 0.5;   // 50% of original size
        const double rotateAngle = 45;    // degrees

        // Ensure the image file exists
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Create a new PDF document with a single page
        using (Aspose.Pdf.Document pdfDoc = new Aspose.Pdf.Document())
        {
            pdfDoc.Pages.Add();

            // Get reference to the first page
            Aspose.Pdf.Page page = pdfDoc.Pages[1];

            // Create an ImageStamp from the image stream
            using (FileStream imgStream = File.OpenRead(imagePath))
            {
                Aspose.Pdf.ImageStamp imgStamp = new Aspose.Pdf.ImageStamp(imgStream);

                // Scale proportionally using Zoom (applies to both axes)
                imgStamp.Zoom = (float)scaleFactor;

                // Set arbitrary rotation angle
                imgStamp.RotateAngle = (float)rotateAngle;

                // Position the stamp on the page
                imgStamp.XIndent = (float)posX;
                imgStamp.YIndent = (float)posY;

                // Add the stamp to the page
                page.AddStamp(imgStamp);
            }

            // Save the resulting PDF
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved to '{outputPdf}'.");
    }
}