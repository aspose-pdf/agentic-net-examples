using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;
using Aspose.Pdf.Optimization;
using Aspose.Pdf.Text; // Added for TextFragment

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputFolder = "Images";

        // Create a sample PDF if it does not exist (seed file for the demo)
        if (!File.Exists(inputPdf))
        {
            using (Document seed = new Document())
            {
                // Add a simple page with some content
                Page page = seed.Pages.Add();
                page.Paragraphs.Add(new TextFragment("Sample page for conversion"));
                seed.Save(inputPdf);
            }
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Apply JPEG compression with quality 80 to images inside the PDF
            OptimizationOptions opt = new OptimizationOptions();
            opt.ImageCompressionOptions.CompressImages = true;
            opt.ImageCompressionOptions.ImageQuality = 80;
            pdfDoc.OptimizeResources(opt);

            // Define resolution for the output JPEG images
            Resolution resolution = new Resolution(300);

            // Create a JpegDevice with the desired resolution and quality
            JpegDevice jpegDevice = new JpegDevice(resolution, 80);

            // Convert each page to a JPEG image
            for (int pageNumber = 1; pageNumber <= pdfDoc.Pages.Count; pageNumber++)
            {
                string outputPath = Path.Combine(outputFolder, $"page_{pageNumber}.jpeg");
                using (FileStream outStream = new FileStream(outputPath, FileMode.Create))
                {
                    jpegDevice.Process(pdfDoc.Pages[pageNumber], outStream);
                }
            }
        }

        Console.WriteLine("PDF pages have been converted to JPEG images with quality 80.");
    }
}
