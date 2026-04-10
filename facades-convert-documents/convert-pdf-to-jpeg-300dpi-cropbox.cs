using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        // Input PDF file
        const string inputPdfPath = "input.pdf";

        // Output folder for JPEG images (will be created if it does not exist)
        const string outputFolder = "JpegImages";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{inputPdfPath}'.");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF document (load rule)
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Initialize PdfConverter with the loaded document (create rule)
            PdfConverter converter = new PdfConverter(pdfDocument);

            // Set resolution to 300 DPI
            converter.Resolution = new Resolution(300);

            // Use CropBox for precise cropping (default is CropBox, set explicitly)
            converter.CoordinateType = PageCoordinateType.CropBox;

            // Prepare the converter
            converter.DoConvert();

            int pageNumber = 1;
            // Extract each page as a JPEG image (save rule – GetNextImage writes the file)
            while (converter.HasNextImage())
            {
                string outputFile = Path.Combine(outputFolder, $"page_{pageNumber}.jpeg");
                converter.GetNextImage(outputFile); // default format is JPEG
                pageNumber++;
            }

            // Release resources held by the converter
            converter.Close();
        }

        Console.WriteLine("PDF conversion to JPEG images completed successfully.");
    }
}