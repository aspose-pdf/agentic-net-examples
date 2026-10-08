using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";

        // Verify the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Create a unique temporary folder for the extracted images
        string tempFolder = Path.Combine(Path.GetTempPath(),
                                         "PdfImages_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        try
        {
            // PdfExtractor implements IDisposable – use a using block for deterministic cleanup
            using (PdfExtractor extractor = new PdfExtractor())
            {
                // Load the PDF document
                extractor.BindPdf(inputPdf);

                // Extract images using the default extraction mode (all pages)
                extractor.ExtractImage();

                // Save each extracted image to the temporary folder
                int imageIndex = 1;
                while (extractor.HasNextImage())
                {
                    // Build a file name – using PNG extension; Aspose will convert the image accordingly
                    string imagePath = Path.Combine(tempFolder, $"Image_{imageIndex}.png");
                    extractor.GetNextImage(imagePath);
                    imageIndex++;
                }
            }

            Console.WriteLine($"Images have been extracted to: {tempFolder}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during extraction: {ex.Message}");
        }
    }
}
