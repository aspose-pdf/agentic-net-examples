using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Path to the source PDF
        const string inputPdf = "input.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Create a temporary folder for extracted images
        string tempFolder = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(tempFolder);

        try
        {
            // PdfExtractor implements IDisposable – wrap it in a using block
            using (PdfExtractor extractor = new PdfExtractor())
            {
                // Load the PDF document
                extractor.BindPdf(inputPdf);

                // Use default extraction mode – no additional settings required
                extractor.ExtractImage();

                // Iterate over all extracted images
                int imageIndex = 1;
                while (extractor.HasNextImage())
                {
                    // Determine output file name (using PNG as a safe default format)
                    string imagePath = Path.Combine(tempFolder, $"Image_{imageIndex}.png");

                    // Save the image directly to the file system with the chosen format
                    extractor.GetNextImage(imagePath, ImageFormat.Png);

                    Console.WriteLine($"Extracted image saved to: {imagePath}");
                    imageIndex++;
                }
            }

            Console.WriteLine($"All images extracted to temporary folder: {tempFolder}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during extraction: {ex.Message}");
        }
    }
}
