using System;
using System.Drawing.Imaging;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices; // Added for Resolution

class PdfToJpegConverter
{
    static void Main()
    {
        // Input PDF file
        const string inputPdf = "input.pdf";

        // Output folder for JPEG images
        const string outputFolder = "jpeg_images";

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Verify the input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Use PdfConverter (Facade) to convert each page to JPEG
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the PDF document to the converter
            converter.BindPdf(inputPdf);

            // Set the resolution to 150 DPI (Resolution object required)
            converter.Resolution = new Resolution(150);

            // Prepare the converter for conversion
            converter.DoConvert();

            int pageNumber = 1;

            // Iterate through all pages and save each as a JPEG image
            while (converter.HasNextImage())
            {
                // Build the output file name (e.g., jpeg_images/page_1.jpg)
                string outputPath = Path.Combine(outputFolder, $"page_{pageNumber}.jpg");

                // Save the current page as JPEG with quality 80 (adjust as needed)
                // This overload uses the default JPEG format and allows setting quality.
                converter.GetNextImage(outputPath, ImageFormat.Jpeg, 80);

                pageNumber++;
            }

            // Release resources held by the converter
            converter.Close();
        }

        Console.WriteLine("PDF pages have been converted to JPEG images successfully.");
    }
}
