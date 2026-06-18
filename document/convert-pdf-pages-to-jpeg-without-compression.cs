using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;
using Aspose.Pdf.Optimization;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputFolder = "Images";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Disable image compression to keep original image data
            var opt = new OptimizationOptions();
            opt.ImageCompressionOptions.CompressImages = false;
            pdfDoc.OptimizeResources(opt);

            // Define the resolution for the output images (e.g., 300 DPI)
            Resolution resolution = new Resolution(300);

            // Create a JPEG device with the desired resolution and maximum quality (100)
            JpegDevice jpegDevice = new JpegDevice(resolution, 100);

            // Iterate through all pages and save each as a JPEG image
            for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
            {
                string outPath = Path.Combine(outputFolder, $"page_{pageNum}.jpeg");
                using (FileStream outStream = new FileStream(outPath, FileMode.Create))
                {
                    jpegDevice.Process(pdfDoc.Pages[pageNum], outStream);
                }
            }
        }

        Console.WriteLine("PDF pages have been converted to images without compression.");
    }
}
