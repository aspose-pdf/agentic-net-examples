using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging;

class PdfToBmpConverter
{
    static void Main()
    {
        // Input PDF file path
        const string pdfPath = "input.pdf";

        // Directory where BMP images will be saved
        const string outputDir = "BmpImages";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Initialize the PdfConverter facade
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the PDF document to the converter
            converter.BindPdf(pdfPath);

            // NOTE: In recent versions of Aspose.Pdf the Resolution class has been removed.
            // The converter uses a default DPI (96) which is sufficient for most BMP conversions.
            // If a higher DPI is required, use the ImageDevice API instead.

            // Prepare the conversion process
            converter.DoConvert();

            int pageNumber = 1;
            // Iterate through all pages and save each as a 24‑bit BMP image
            while (converter.HasNextImage())
            {
                string bmpPath = Path.Combine(outputDir, $"page_{pageNumber}.bmp");
                // GetNextImage with ImageFormat.Bmp produces a 24‑bit BMP
                converter.GetNextImage(bmpPath, ImageFormat.Bmp);
                pageNumber++;
            }
        }

        Console.WriteLine($"Conversion completed. BMP files are saved in '{outputDir}'.");
    }
}
