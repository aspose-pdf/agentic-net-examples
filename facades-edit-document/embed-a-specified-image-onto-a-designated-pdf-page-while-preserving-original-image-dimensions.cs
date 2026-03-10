using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing; // Used only for obtaining image dimensions (width & height)

class EmbedImageExample
{
    static void Main()
    {
        // Input PDF, image to embed, and output PDF paths
        const string inputPdfPath  = "input.pdf";
        const string imagePath     = "logo.png";
        const string outputPdfPath = "output.pdf";

        // Page number (1‑based) where the image will be placed
        const int targetPage = 1;

        // Desired lower‑left corner of the image on the page (in points)
        const float lowerLeftX = 100f;
        const float lowerLeftY = 150f;

        // Validate files
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

        // Obtain original image dimensions (preserve them)
        float imageWidth;
        float imageHeight;
        using (System.Drawing.Image sysImg = System.Drawing.Image.FromFile(imagePath))
        {
            // Image dimensions are in pixels; PDF uses points (1 point = 1/72 inch).
            // Assuming 72 DPI for simplicity – this keeps the visual size identical.
            imageWidth  = sysImg.Width;
            imageHeight = sysImg.Height;
        }

        // Compute upper‑right corner based on original dimensions
        float upperRightX = lowerLeftX + imageWidth;
        float upperRightY = lowerLeftY + imageHeight;

        // Embed the image using PdfFileMend (facade API)
        using (PdfFileMend mender = new PdfFileMend())
        {
            // Bind the source PDF
            mender.BindPdf(inputPdfPath);

            // Open the image stream
            using (FileStream imgStream = File.OpenRead(imagePath))
            {
                // Add the image to the specified page preserving its size
                bool success = mender.AddImage(
                    imgStream,
                    targetPage,
                    lowerLeftX,
                    lowerLeftY,
                    upperRightX,
                    upperRightY);

                if (!success)
                {
                    Console.Error.WriteLine("Failed to add the image to the PDF.");
                    return;
                }
            }

            // Save the modified PDF
            mender.Save(outputPdfPath);
        }

        Console.WriteLine($"Image embedded successfully. Output saved to '{outputPdfPath}'.");
    }
}