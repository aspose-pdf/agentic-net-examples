using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF that already contains the PNG image.
        const string inputPdfPath = "input.pdf";
        // GIF image to overlay (semi‑transparent).
        const string overlayGifPath = "overlay.gif";
        // Output PDF with the overlay applied.
        const string outputPdfPath = "output.pdf";

        // Verify required files exist.
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(overlayGifPath))
        {
            Console.Error.WriteLine($"Overlay GIF not found: {overlayGifPath}");
            return;
        }

        // Use PdfFileMend facade to modify the PDF.
        using (PdfFileMend mend = new PdfFileMend())
        {
            // Load the existing PDF.
            mend.BindPdf(inputPdfPath);

            // Define the rectangle where the GIF will be placed.
            // Coordinates are in default user space units (points).
            float lowerLeftX = 50f;   // X of lower‑left corner
            float lowerLeftY = 500f;  // Y of lower‑left corner
            float upperRightX = 250f; // X of upper‑right corner
            float upperRightY = 700f; // Y of upper‑right corner

            // Create compositing parameters to achieve a semi‑transparent effect.
            // BlendMode.Multiply blends the overlay with the underlying PNG.
            CompositingParameters compParams = new CompositingParameters(BlendMode.Multiply);

            // Add the GIF image on page 1 using the compositing parameters.
            // This overlays the GIF onto the existing PNG at the same coordinates.
            mend.AddImage(overlayGifPath, 1, lowerLeftX, lowerLeftY, upperRightX, upperRightY, compParams);

            // Save the modified PDF.
            mend.Save(outputPdfPath);
            mend.Close();
        }

        Console.WriteLine($"Overlay completed. Output saved to '{outputPdfPath}'.");
    }
}