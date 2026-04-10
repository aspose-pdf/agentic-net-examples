using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging;

class ExportPdfImagesToJpeg
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdfPath = "input.pdf";

        // Output folder for JPEG images
        const string outputFolder = "ExportedImages";

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Verify the input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block (ensures proper disposal)
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Create a PdfConverter instance (facade for image extraction)
            using (PdfConverter converter = new PdfConverter())
            {
                // Bind the loaded PDF document to the converter
                converter.BindPdf(pdfDocument);
                // Prepare the converter for processing
                converter.DoConvert();

                int imageIndex = 1;
                // Iterate over all images (one per page) in the PDF
                while (converter.HasNextImage())
                {
                    // Build the output file name (e.g., image1.jpg, image2.jpg, ...)
                    string outputFile = Path.Combine(outputFolder, $"image{imageIndex}.jpg");

                    // Export the current image as JPEG with quality 85
                    // GetNextImage(string outputFile, ImageFormat format, int quality)
                    converter.GetNextImage(outputFile, ImageFormat.Jpeg, 85);

                    Console.WriteLine($"Saved JPEG image: {outputFile}");
                    imageIndex++;
                }
            }
        }

        Console.WriteLine("Image export completed.");
    }
}