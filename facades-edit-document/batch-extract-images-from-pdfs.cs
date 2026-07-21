using System;
using System.IO;
using System.Drawing.Imaging;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Input directory containing PDF files
        const string inputDirectory = "pdfs";
        // Root output directory for extracted images
        const string outputRoot = "extracted_images";

        // Ensure the output root exists
        Directory.CreateDirectory(outputRoot);

        // Verify that the input directory exists before enumerating files
        if (!Directory.Exists(inputDirectory))
        {
            Console.WriteLine($"Input directory '{inputDirectory}' not found. No PDFs to process.");
            return;
        }

        // Iterate over all PDF files in the input directory
        foreach (string pdfPath in Directory.GetFiles(inputDirectory, "*.pdf"))
        {
            // Create a subfolder named after the PDF file (without extension)
            string pdfName = Path.GetFileNameWithoutExtension(pdfPath);
            string outputFolder = Path.Combine(outputRoot, pdfName);
            Directory.CreateDirectory(outputFolder);

            // Use PdfExtractor to extract images from the current PDF
            using (PdfExtractor extractor = new PdfExtractor())
            {
                extractor.BindPdf(pdfPath);
                extractor.ExtractImage();

                int imageIndex = 1;
#pragma warning disable CA1416 // 'ImageFormat.Jpeg' is platform‑specific; suppress for cross‑platform builds where the call is safe.
                while (extractor.HasNextImage())
                {
                    // Save each extracted image as JPEG in the subfolder
                    string imagePath = Path.Combine(outputFolder, $"image-{imageIndex}.jpg");
                    extractor.GetNextImage(imagePath, ImageFormat.Jpeg);
                    imageIndex++;
                }
#pragma warning restore CA1416
            }
        }

        Console.WriteLine("Image extraction completed.");
    }
}
