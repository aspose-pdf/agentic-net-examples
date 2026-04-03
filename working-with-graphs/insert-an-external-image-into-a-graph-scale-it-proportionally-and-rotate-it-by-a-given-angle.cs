using System;
using System.IO;
using Aspose.Pdf;                     // Core PDF classes
using Aspose.Pdf.Facades;            // For stamp handling (ImageStamp is in Aspose.Pdf namespace, but we keep Facades for consistency)

class Program
{
    static void Main()
    {
        // Input PDF, image to insert, and output PDF paths
        const string inputPdfPath   = "input.pdf";
        const string imagePath      = "logo.png";
        const string outputPdfPath  = "output.pdf";

        // Desired scaling factor (1.0 = original size). Adjust as needed.
        const float scaleFactor = 0.5f;   // 50 % of original size
        // Desired rotation angle in degrees (arbitrary angle allowed)
        const float rotationAngle = 45f;  // rotate 45 degrees clockwise

        // Position where the image will be placed (lower‑left corner of the stamp)
        const float xPosition = 100f;    // points from the left edge
        const float yPosition = 200f;    // points from the bottom edge

        // Ensure the input files exist
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

        // Load the PDF document inside a using block for deterministic disposal
        using (Aspose.Pdf.Document pdfDoc = new Aspose.Pdf.Document(inputPdfPath))
        {
            // Choose the page where the image will be inserted (1‑based index)
            Aspose.Pdf.Page targetPage = pdfDoc.Pages[1];

            // Create an ImageStamp from the external image file
            Aspose.Pdf.ImageStamp imgStamp = new Aspose.Pdf.ImageStamp(imagePath);

            // Set the position of the stamp on the page
            imgStamp.XIndent = xPosition;   // horizontal coordinate (from left)
            imgStamp.YIndent = yPosition;   // vertical coordinate (from bottom)

            // Apply proportional scaling using the Zoom property
            // Zoom scales both width and height equally, preserving aspect ratio
            imgStamp.Zoom = scaleFactor;

            // Apply arbitrary rotation
            imgStamp.RotateAngle = rotationAngle;

            // Place the stamp in front of the page content (foreground)
            imgStamp.Background = false;

            // Add the configured stamp to the target page
            targetPage.AddStamp(imgStamp);

            // Save the modified PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Image inserted, scaled, and rotated. Output saved to '{outputPdfPath}'.");
    }
}
