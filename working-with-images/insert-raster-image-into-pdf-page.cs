using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath    = "input.pdf";
        const string imagePath  = "image.png";
        const string outputPath = "output.pdf";

        // Absolute coordinates (lower‑left corner) in points (1 point = 1/72 inch)
        const double llx   = 100; // X position from left
        const double lly   = 200; // Y position from bottom
        const double width = 300; // Desired image width
        const double height = 200; // Desired image height

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image not found: {imagePath}");
            return;
        }

        try
        {
            // Load the existing PDF inside a using block for deterministic disposal
            using (Document doc = new Document(pdfPath))
            {
                // Aspose.Pdf uses 1‑based page indexing
                Page page = doc.Pages[1];

                // Create an ImageStamp for the raster image
                ImageStamp imgStamp = new ImageStamp(imagePath)
                {
                    // Set absolute position on the page
                    XIndent = llx,
                    YIndent = lly,
                    // Set the size of the image
                    Width   = width,
                    Height  = height,
                    // Place the image over the page content (false = foreground)
                    Background = false
                };

                // Add the stamp to the selected page
                page.AddStamp(imgStamp);

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Image inserted and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}