using System;
using System.IO;
using System.Drawing; // kept for DPI information, but Image will be fully qualified
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input and output file paths
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string imagePath     = "external_image.png";

        // Page number where the image will be placed (1‑based indexing)
        const int targetPageNumber = 1;

        // Desired maximum dimensions for the image (points; 1 point = 1/72 inch)
        const double maxWidth  = 200.0; // e.g., 200 points
        const double maxHeight = 200.0; // e.g., 200 points

        // Rotation angle in degrees (clockwise)
        const float rotationAngle = 45.0f;

        // Validate input files
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
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Ensure the requested page exists
            if (targetPageNumber < 1 || targetPageNumber > pdfDoc.Pages.Count)
            {
                Console.Error.WriteLine($"Page {targetPageNumber} is out of range. Document has {pdfDoc.Pages.Count} pages.");
                return;
            }

            // Load the external image to obtain its original dimensions (points)
            double originalWidthPoints;
            double originalHeightPoints;
            // Fully qualify System.Drawing.Image to avoid ambiguity with Aspose.Pdf.Image
            using (System.Drawing.Image sysImg = System.Drawing.Image.FromFile(imagePath))
            {
                // Convert pixel dimensions to points using the image's DPI (72 points per inch)
                originalWidthPoints  = sysImg.Width  * 72.0 / sysImg.HorizontalResolution;
                originalHeightPoints = sysImg.Height * 72.0 / sysImg.VerticalResolution;
            }

            // Compute a uniform scaling factor to keep the aspect ratio
            double widthScale  = maxWidth  / originalWidthPoints;
            double heightScale = maxHeight / originalHeightPoints;
            double scaleFactor = Math.Min(widthScale, heightScale);

            // Calculate the scaled dimensions (points)
            double scaledWidth  = originalWidthPoints  * scaleFactor;
            double scaledHeight = originalHeightPoints * scaleFactor;

            // Create an ImageStamp (a visual element that can be placed on a page)
            ImageStamp imgStamp = new ImageStamp(imagePath)
            {
                // Apply the proportional size
                Width  = scaledWidth,
                Height = scaledHeight,

                // Rotate the image around its centre (degrees clockwise)
                RotateAngle = rotationAngle,

                // Position the stamp – centred on the page
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Center
            };

            // Add the stamp to the specified page
            pdfDoc.Pages[targetPageNumber].AddStamp(imgStamp);

            // Save the modified PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Image inserted, scaled, and rotated. Output saved to '{outputPdfPath}'.");
    }
}
