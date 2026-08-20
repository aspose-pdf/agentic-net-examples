using System;
using System.IO;
using Aspose.Pdf.Facades; // PdfExtractor resides here

class Program
{
    static void Main()
    {
        // Path to the source PDF file
        const string inputPdfPath = "input.pdf";

        // Create a unique temporary folder for extracted images
        string tempFolder = Path.Combine(Path.GetTempPath(),
                                         "PdfImages_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        try
        {
            // Initialize the extractor and bind the PDF document
            using (PdfExtractor extractor = new PdfExtractor())
            {
                extractor.BindPdf(inputPdfPath);

                // Use the default extraction mode (DefinedInResources)
                extractor.ExtractImage();

                int imageIndex = 1;
                // Iterate through all extracted images
                while (extractor.HasNextImage())
                {
                    // Build the output file name (default format is JPEG)
                    string outputImagePath = Path.Combine(tempFolder,
                                                          $"image-{imageIndex}.jpg");

                    // Save the current image to the temporary folder
                    extractor.GetNextImage(outputImagePath);
                    imageIndex++;
                }
            }

            Console.WriteLine($"Images extracted to temporary folder: {tempFolder}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during image extraction: {ex.Message}");
        }
    }
}