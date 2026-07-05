using System;
using System.IO;
using System.Drawing.Imaging; // ImageFormat enum
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices; // Resolution

class PdfToBmpConverter
{
    static void Main()
    {
        // Input PDF file path
        const string pdfPath = "input.pdf";

        // Output folder for BMP images
        const string outputFolder = "BmpImages";

        // Ensure the input file exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found – {pdfPath}");
            return;
        }

        // Create output directory if it does not exist
        Directory.CreateDirectory(outputFolder);

        // Initialize the PdfConverter facade
        PdfConverter converter = new PdfConverter();

        // Bind the PDF document to the converter
        converter.BindPdf(pdfPath);

        // Configure resolution (e.g., 300 DPI)
        converter.Resolution = new Resolution(300);

        // NOTE: In recent Aspose.Pdf versions the PageCoordinateType enum has been removed.
        // The default coordinate type is CropBox, which matches the original intent.
        // If a specific coordinate type is required, it can be set using the integer value
        // of the underlying enum (0 = MediaBox, 1 = CropBox, etc.).
        // Example (uncomment if needed and adjust the value):
        // converter.CoordinateType = (PageCoordinateType)1; // CropBox

        // Prepare the converter for image extraction
        converter.DoConvert();

        int pageIndex = 1;
        // Loop through all pages and save each as a BMP image
        while (converter.HasNextImage())
        {
            string outputPath = Path.Combine(outputFolder, $"page_{pageIndex}.bmp");
            // Use System.Drawing.Imaging.ImageFormat for BMP
            converter.GetNextImage(outputPath, ImageFormat.Bmp);
            pageIndex++;
        }

        // Release resources
        converter.Close();

        Console.WriteLine($"Conversion completed. BMP images saved to '{outputFolder}'.");
    }
}
