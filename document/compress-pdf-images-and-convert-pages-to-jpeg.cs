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
        const string inputPdfPath = "input.pdf";          // source PDF
        const string outputFolder = "Images";            // folder for JPEG files

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // ---------------------------------------------------------------------
        // Ensure a source PDF exists – create a simple one if it is missing.
        // ---------------------------------------------------------------------
        if (!File.Exists(inputPdfPath))
        {
            using (Document sampleDoc = new Document())
            {
                Page page = sampleDoc.Pages.Add();
                page.Paragraphs.Add(new TextFragment("Sample PDF generated because 'input.pdf' was not found."));
                sampleDoc.Save(inputPdfPath);
                Console.WriteLine($"Created placeholder PDF at '{inputPdfPath}'.");
            }
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // ------------------------------------------------------------
            // Apply JPEG compression to images inside the PDF (quality 80)
            // ------------------------------------------------------------
            OptimizationOptions opt = new OptimizationOptions();
            // ImageCompressionOptions is read‑only; configure the existing instance
            opt.ImageCompressionOptions.CompressImages = true;   // enable compression
            opt.ImageCompressionOptions.ImageQuality   = 80;    // set compression quality (0‑100)
            // Optional: specify JPEG encoding explicitly
            // opt.ImageCompressionOptions.Encoding = ImageEncoding.Jpeg;

            // Perform the optimization (compression) on the document
            pdfDoc.OptimizeResources(opt);

            // ------------------------------------------------------------
            // Convert each page to a JPEG image
            // ------------------------------------------------------------
            // Define the resolution for the output images (e.g., 300 DPI)
            Resolution resolution = new Resolution(300);

            // JpegDevice constructor that accepts resolution and quality
            // Quality here controls the JPEG output quality (0‑100)
            JpegDevice jpegDevice = new JpegDevice(resolution, 80);

            for (int pageNumber = 1; pageNumber <= pdfDoc.Pages.Count; pageNumber++)
            {
                string outputPath = Path.Combine(outputFolder, $"page_{pageNumber}.jpeg");

                // Convert the current page and write the JPEG to a file stream
                using (FileStream jpegStream = new FileStream(outputPath, FileMode.Create))
                {
                    jpegDevice.Process(pdfDoc.Pages[pageNumber], jpegStream);
                }

                Console.WriteLine($"Saved page {pageNumber} as JPEG to '{outputPath}'.");
            }
        }
    }
}
