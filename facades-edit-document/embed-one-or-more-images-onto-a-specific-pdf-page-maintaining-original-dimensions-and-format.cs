using System;
using System.IO;
using System.Drawing;               // Used only to obtain image width/height
using Aspose.Pdf.Facades;          // Facade classes for PDF manipulation

class Program
{
    static void Main()
    {
        // Input PDF, output PDF and image to embed
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string imagePath     = "picture.jpg";

        // Target page (1‑based indexing) where the image will be placed
        const int targetPage = 1;

        // Verify that required files exist
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

        // ------------------------------------------------------------
        // Determine the original dimensions of the image.
        // Aspose.Pdf.Facades does not provide a direct way to query
        // image size, so System.Drawing.Image is used only for this
        // purpose. The pixel dimensions are used as PDF points,
        // preserving the original size.
        // ------------------------------------------------------------
        int imgWidth, imgHeight;
        using (Image img = Image.FromFile(imagePath))
        {
            imgWidth  = img.Width;   // Width in pixels
            imgHeight = img.Height;  // Height in pixels
        }

        // Define where the image will be placed on the page.
        // Lower‑left corner (llx, lly) can be adjusted as needed.
        float llx = 50;                     // lower‑left X
        float lly = 700;                    // lower‑left Y
        float urx = llx + imgWidth;         // upper‑right X (preserve width)
        float ury = lly + imgHeight;        // upper‑right Y (preserve height)

        // ------------------------------------------------------------
        // Create the PdfFileMend facade, add the image, and close.
        // The constructor with input and output file paths performs the
        // required creation step. Close() finalises the operation and
        // writes the output PDF.
        // ------------------------------------------------------------
        PdfFileMend pdfMend = new PdfFileMend(inputPdfPath, outputPdfPath);
        pdfMend.AddImage(imagePath, targetPage, llx, lly, urx, ury);
        pdfMend.Close();

        Console.WriteLine($"Image embedded and saved to '{outputPdfPath}'.");
    }
}