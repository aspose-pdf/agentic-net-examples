using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        // Input PDF, image to be used as stamp, and output PDF paths
        const string inputPdfPath  = "input.pdf";
        const string stampImagePath = "logo.png";
        const string outputPdfPath = "output.pdf";

        // Desired coordinates for the stamp (in points, 1 point = 1/72 inch)
        // XIndent: distance from the left edge of the page
        // YIndent: distance from the bottom edge of the page
        const double customX = 150; // e.g., 150 points from the left
        const double customY = 300; // e.g., 300 points from the bottom

        // Verify that the source files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(stampImagePath))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImagePath}");
            return;
        }

        // Load the PDF, add the image stamp with precise positioning, and save
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Create an ImageStamp from the image file
            ImageStamp imgStamp = new ImageStamp(stampImagePath);

            // Set exact coordinates
            imgStamp.XIndent = customX; // horizontal position from left
            imgStamp.YIndent = customY; // vertical position from bottom

            // Optional: set size (width/height) or scaling via Zoom if needed
            // imgStamp.Width  = 100; // desired width in points
            // imgStamp.Height = 50;  // desired height in points

            // Add the stamp to the first page (pages are 1‑based)
            pdfDoc.Pages[1].AddStamp(imgStamp);

            // Save the modified PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Image stamp placed at ({customX}, {customY}) and saved to '{outputPdfPath}'.");
    }
}