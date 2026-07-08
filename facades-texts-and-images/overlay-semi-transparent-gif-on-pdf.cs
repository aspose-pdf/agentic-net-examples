using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";   // PDF that already contains a PNG image
        const string overlayGifPath = "overlay.gif"; // Semi‑transparent GIF to overlay
        const string outputPdfPath  = "output.pdf";

        if (!File.Exists(inputPdfPath) || !File.Exists(overlayGifPath))
        {
            Console.Error.WriteLine("Required input files are missing.");
            return;
        }

        // Load the existing PDF document
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Initialize PdfFileMend with the loaded document
            using (PdfFileMend mender = new PdfFileMend(pdfDoc))
            {
                // Create compositing parameters – Normal blend mode respects the GIF's alpha channel
                CompositingParameters compParams = new CompositingParameters(BlendMode.Normal);

                // Define the rectangle where the GIF will be placed (adjust as needed)
                float lowerLeftX  = 100f; // X coordinate of lower‑left corner
                float lowerLeftY  = 200f; // Y coordinate of lower‑left corner
                float upperRightX = 300f; // X coordinate of upper‑right corner
                float upperRightY = 400f; // Y coordinate of upper‑right corner

                // Add the GIF image to page 1 using the compositing parameters
                using (FileStream gifStream = File.OpenRead(overlayGifPath))
                {
                    mender.AddImage(gifStream, 1, lowerLeftX, lowerLeftY, upperRightX, upperRightY, compParams);
                }

                // Save the modified PDF
                mender.Save(outputPdfPath);
            }
        }

        Console.WriteLine($"Overlay applied successfully. Output saved to '{outputPdfPath}'.");
    }
}