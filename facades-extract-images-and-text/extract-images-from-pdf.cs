using System;
using System.IO;
using System.Drawing.Imaging;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the source PDF file
        const string pdfPath = "input.pdf";

        // Verify the PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Create a temporary folder for extracted images
        string tempFolder = Path.Combine(Path.GetTempPath(),
                                         "PdfImages_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        try
        {
            // Initialize the PdfExtractor facade
            using (PdfExtractor extractor = new PdfExtractor())
            {
                // Bind the PDF document
                extractor.BindPdf(pdfPath);

                // Use default extraction mode (DefinedInResources)
                extractor.ExtractImage();

                // Extract each image and save it as PNG in the temporary folder
                int imageIndex = 1;
                while (extractor.HasNextImage())
                {
                    string imagePath = Path.Combine(tempFolder,
                                                    $"image_{imageIndex}.png");
                    // Save the next image in PNG format
                    extractor.GetNextImage(imagePath, ImageFormat.Png);
                    imageIndex++;
                }
            }

            Console.WriteLine($"Images extracted to: {tempFolder}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during extraction: {ex.Message}");
        }
    }
}