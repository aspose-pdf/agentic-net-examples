using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string imagesFolder = "Images";

        // Verify the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(imagesFolder);

        // PdfExtractor implements IDisposable – wrap it in a using block
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Load the PDF document
            extractor.BindPdf(pdfPath);

            // The ExtractImageMode property is not available in the current Aspose.Pdf version.
            // Image extraction is performed by calling ExtractImage(), which extracts all images by default.
            extractor.ExtractImage();

            // Iterate through the extracted images and save each as PNG
            int index = 1;
            while (extractor.HasNextImage())
            {
                string outPath = Path.Combine(imagesFolder, $"Image{index}.png");
                extractor.GetNextImage(outPath, ImageFormat.Png);
                Console.WriteLine($"Saved image {index} to '{outPath}'.");
                index++;
            }
        }
    }
}
