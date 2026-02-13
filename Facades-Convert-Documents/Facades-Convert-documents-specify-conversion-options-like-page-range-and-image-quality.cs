using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices; // Resolution class

class Program
{
    static void Main(string[] args)
    {
        // Paths for source PDF and destination image file
        string inputPdfPath = "input.pdf";
        string outputImagePath = "output.tiff";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{inputPdfPath}'.");
            return;
        }

        try
        {
            // Load the PDF document
            Document pdfDoc = new Document(inputPdfPath);

            // Convert selected pages to images using PdfConverter
            using (PdfConverter converter = new PdfConverter())
            {
                // Bind the loaded document
                converter.BindPdf(pdfDoc);

                // ----- Conversion options -----
                // Convert pages 2 through 4 (inclusive)
                converter.StartPage = 2;
                converter.EndPage   = 4;

                // Set image resolution (DPI) – higher value yields better quality
                converter.Resolution = new Resolution(300);

                // NOTE: PdfConverter does NOT expose an ImageQuality property.
                // Quality for TIFF output is controlled by the resolution and the
                // internal compression defaults. If you need explicit JPEG quality,
                // use Aspose.Pdf.Devices.JpegDevice instead.
                // ------------------------------------------------------------

                // Execute the conversion
                converter.DoConvert();

                // Save the resulting images as a multi‑page TIFF file
                converter.SaveAsTIFF(outputImagePath);
            }

            Console.WriteLine($"Conversion finished. Images saved to '{outputImagePath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
