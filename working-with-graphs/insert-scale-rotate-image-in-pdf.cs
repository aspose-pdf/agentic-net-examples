using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string imagePath     = "image.png";

        // Desired transformation
        const float rotationAngleDegrees = 45f;   // rotate 45 degrees
        const float scaleFactor          = 0.5f; // 50 % of original size

        // Position of the image on the page (lower‑left corner)
        const float posX = 100f;
        const float posY = 200f;

        // Ensure the source files exist
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

        // Open the PDF document
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Work with the first page (1‑based indexing)
            Page page = pdfDoc.Pages[1];

            // Create an ImageStamp – core API (no Facades)
            ImageStamp imgStamp = new ImageStamp(imagePath);

            // Set the position where the stamp will be placed
            imgStamp.XIndent = posX; // distance from the left edge
            imgStamp.YIndent = posY; // distance from the bottom edge

            // Uniform scaling – preserves aspect ratio
            imgStamp.Zoom = scaleFactor; // applies to both X and Y

            // Rotate the image by an arbitrary angle (degrees)
            imgStamp.RotateAngle = rotationAngleDegrees;

            // Add the stamp (image) to the page
            page.AddStamp(imgStamp);

            // Save the modified PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Image inserted, scaled, and rotated. Saved to '{outputPdfPath}'.");
    }
}