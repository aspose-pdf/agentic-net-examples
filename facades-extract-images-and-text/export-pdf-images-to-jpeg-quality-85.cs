using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf;
using System.Drawing.Imaging;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "ExtractedImages";

        // Verify source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Source file not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // PdfConverter implements IDisposable – wrap in using for deterministic cleanup
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the PDF file to the converter
            converter.BindPdf(inputPdf);
            // Prepare internal structures for image extraction
            converter.DoConvert();

            int imageNumber = 1;
            // Iterate over all images in the PDF
            while (converter.HasNextImage())
            {
                // Build output file name (e.g., image1.jpg, image2.jpg, ...)
                string outputPath = Path.Combine(outputDir, $"image{imageNumber}.jpg");

                // Export the current image as JPEG with quality set to 85 (0‑100 range)
                converter.GetNextImage(outputPath, ImageFormat.Jpeg, 85);

                imageNumber++;
            }
        }

        Console.WriteLine("All images have been exported to JPEG format.");
    }
}