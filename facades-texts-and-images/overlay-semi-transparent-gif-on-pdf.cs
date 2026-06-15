using System;
using System.IO;
using Aspose.Pdf;               // BlendMode, CompositingParameters
using Aspose.Pdf.Facades;      // PdfFileMend

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // PDF containing the PNG
        const string outputPdf = "output.pdf";  // Resulting PDF with overlay
        const string gifPath   = "overlay.gif"; // Semi‑transparent GIF to overlay

        // Verify required files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(gifPath))
        {
            Console.Error.WriteLine($"Overlay GIF not found: {gifPath}");
            return;
        }

        // PdfFileMend constructor binds the source PDF and sets the destination file
        using (PdfFileMend mend = new PdfFileMend(inputPdf, outputPdf))
        {
            // Create compositing parameters – Normal blend preserves the GIF's alpha channel
            CompositingParameters compParams = new CompositingParameters(BlendMode.Normal);

            // Define the rectangle where the GIF will be placed (coordinates in points)
            // Adjust these values to match the location of the existing PNG on the page
            float lowerLeftX  = 100f; // X‑coordinate of lower‑left corner
            float lowerLeftY  = 200f; // Y‑coordinate of lower‑left corner
            float upperRightX = 300f; // X‑coordinate of upper‑right corner
            float upperRightY = 400f; // Y‑coordinate of upper‑right corner

            // Add the GIF onto page 1 using the compositing parameters
            // This overlays the GIF on top of any existing content (e.g., the PNG)
            mend.AddImage(gifPath, 1, lowerLeftX, lowerLeftY, upperRightX, upperRightY, compParams);

            // Finalize the operation
            mend.Close();
        }

        Console.WriteLine($"Overlay applied successfully. Output saved to '{outputPdf}'.");
    }
}