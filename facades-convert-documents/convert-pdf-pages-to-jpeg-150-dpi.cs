using System;
using System.IO;
using System.Drawing.Imaging; // for ImageFormat
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices; // for Resolution

class PdfToJpegConverter
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "output_images";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Wrap Document in a using block for deterministic disposal
        using (Document pdfDocument = new Document(inputPdfPath))
        // Wrap PdfConverter in a using block (it implements IDisposable)
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the PDF document to the converter
            converter.BindPdf(pdfDocument);

            // Convert only pages 1 through 10 (1‑based indexing)
            converter.StartPage = 1;
            converter.EndPage   = 10;

            // Set resolution to 150 DPI (explicitly)
            converter.Resolution = new Resolution(150);

            // NOTE: In recent Aspose.Pdf versions the CropBox coordinate type is the default
            // and the former "PageCropBox" property has been removed. No additional code is
            // required to use the CropBox when converting pages to images.

            // Prepare the converter
            converter.DoConvert();

            int imageIndex = 1;
            while (converter.HasNextImage())
            {
                // Build output file name (e.g., image1.jpg, image2.jpg, ...)
                string outputFile = Path.Combine(outputFolder, $"image{imageIndex}.jpg");

                // Save the current page as JPEG
                converter.GetNextImage(outputFile, ImageFormat.Jpeg);

                imageIndex++;
            }
        }

        Console.WriteLine("Conversion completed.");
    }
}
