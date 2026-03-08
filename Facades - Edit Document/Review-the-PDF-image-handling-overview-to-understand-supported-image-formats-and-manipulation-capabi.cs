using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Input PDF file
        const string inputPdf = "sample.pdf";

        // Directory where extracted images will be saved
        const string outputFolder = "ExtractedImages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // ------------------------------------------------------------
        // PdfConverter supports conversion of PDF pages to BMP, JPEG,
        // PNG and TIFF images. Below we extract each page as a PNG.
        // ------------------------------------------------------------

        // Create the converter (lifecycle rule: use provided constructor)
        Aspose.Pdf.Facades.PdfConverter converter = new Aspose.Pdf.Facades.PdfConverter();

        // Bind the PDF document to the converter
        converter.BindPdf(inputPdf);

        // Prepare the converter for extraction
        converter.DoConvert();

        int pageIndex = 1;
        while (converter.HasNextImage())
        {
            // Build a unique file name for each page
            string outputPath = Path.Combine(outputFolder, $"page_{pageIndex}.png");

            // Save the current page as PNG (ImageFormat.Png is supported)
            converter.GetNextImage(outputPath, ImageFormat.Png);

            pageIndex++;
        }

        // Release resources (lifecycle rule: close the facade)
        converter.Close();

        Console.WriteLine($"Successfully extracted {pageIndex - 1} page images to '{outputFolder}'.");
    }
}