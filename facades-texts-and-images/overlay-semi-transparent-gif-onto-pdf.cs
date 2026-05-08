using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices; // for BlendMode enum

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdfPath   = "input.pdf";   // PDF that already contains a PNG image
        const string outputPdfPath  = "output.pdf";  // Resulting PDF with GIF overlay
        const string overlayGifPath = "overlay.gif"; // Semi‑transparent GIF to overlay

        // Verify files exist
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

        // Load the existing PDF inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Create a PdfFileMend facade bound to the loaded document
            PdfFileMend mender = new PdfFileMend(pdfDoc);

            // Define compositing parameters – using Multiply blend mode for semi‑transparent effect
            CompositingParameters compParams = new CompositingParameters(BlendMode.Multiply);

            // Define the rectangle where the GIF will be placed (coordinates are in points)
            // Example: lower‑left (50, 400), upper‑right (250, 600)
            float lowerLeftX  = 50f;
            float lowerLeftY  = 400f;
            float upperRightX = 250f;
            float upperRightY = 600f;

            // Add the GIF image to page 1 with the compositing parameters
            // This overlays the GIF on top of any existing content (e.g., the PNG)
            mender.AddImage(overlayGifPath, 1, lowerLeftX, lowerLeftY, upperRightX, upperRightY, compParams);

            // Save the modified PDF – no extra save options needed
            pdfDoc.Save(outputPdfPath);

            // Close the facade (releases internal resources)
            mender.Close();
        }

        Console.WriteLine($"Overlay completed. Output saved to '{outputPdfPath}'.");
    }
}