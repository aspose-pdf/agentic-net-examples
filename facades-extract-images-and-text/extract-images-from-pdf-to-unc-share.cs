using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Local PDF file to extract images from
        const string pdfPath = @"C:\Docs\sample.pdf";

        // Network UNC folder where extracted images will be saved
        const string uncFolder = @"\\Server\Share\Images\";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        // Ensure the UNC directory exists (creates it if missing)
        Directory.CreateDirectory(uncFolder);

        // Use PdfExtractor (Facade) inside a using block for deterministic disposal
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the source PDF file
            extractor.BindPdf(pdfPath);

            // Prepare the extractor to work with images
            extractor.ExtractImage();

            int imageIndex = 1;
            // Loop through all images in the document
            while (extractor.HasNextImage())
            {
                // Build the full UNC path for the current image
                string outputFile = Path.Combine(uncFolder, $"image-{imageIndex}.png");

                // Extract the image and save it as PNG
                extractor.GetNextImage(outputFile, ImageFormat.Png);

                imageIndex++;
            }
        }

        Console.WriteLine("Image extraction completed.");
    }
}