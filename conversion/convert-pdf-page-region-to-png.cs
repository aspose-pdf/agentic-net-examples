using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string pdfPath = "input.pdf";
        // Output PNG file path
        const string pngPath = "region_output.png";

        // Define the region to extract (coordinates are in points; origin is lower‑left)
        // Example: rectangle from (100, 200) to (300, 400)
        Aspose.Pdf.Rectangle region = new Aspose.Pdf.Rectangle(100, 200, 300, 400);

        // Page number to extract (1‑based indexing)
        const int pageNumber = 1;

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(pdfPath))
        {
            // Validate page number
            if (pageNumber < 1 || pageNumber > doc.Pages.Count)
            {
                Console.Error.WriteLine("Invalid page number.");
                return;
            }

            // Preserve original CropBox to restore later
            Page page = doc.Pages[pageNumber];
            Aspose.Pdf.Rectangle originalCropBox = page.CropBox;

            try
            {
                // Set the CropBox to the desired region
                page.CropBox = region;

                // Create a resolution (e.g., 300 DPI)
                Resolution resolution = new Resolution(300);

                // Initialize the PNG device with the resolution
                PngDevice pngDevice = new PngDevice(resolution);

                // Render the cropped page to a PNG file
                using (FileStream pngStream = new FileStream(pngPath, FileMode.Create))
                {
                    pngDevice.Process(page, pngStream);
                }

                Console.WriteLine($"Region saved as PNG: {pngPath}");
            }
            finally
            {
                // Restore the original CropBox so the document remains unchanged
                page.CropBox = originalCropBox;
            }
        }
    }
}