using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdf = "input.pdf";

        // Folder where extracted images will be saved
        const string outputFolder = "ExtractedImages";

        // Validate input file existence
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        try
        {
            // Initialize the PDF extractor (Facades API) inside a using block for proper disposal
            using (PdfExtractor extractor = new PdfExtractor())
            {
                // Bind the source PDF document
                extractor.BindPdf(inputPdf);

                // NOTE: The ExtractImageMode property is not available in the current Aspose.Pdf.Facades version.
                // Image extraction is performed by calling ExtractImage(), which extracts all images by default.
                // extractor.ExtractImageMode = ExtractImageMode.DefinedInResources; // removed for compatibility

                // Extract all images from the document
                extractor.ExtractImage();

                int imageIndex = 1;
                // Iterate through each extracted image using HasNextImage / GetNextImage
                while (extractor.HasNextImage())
                {
                    string imagePath = Path.Combine(outputFolder, $"image_{imageIndex}.png");

                    // Save the image to the file system
                    using (FileStream imageStream = new FileStream(imagePath, FileMode.Create, FileAccess.Write))
                    {
                        extractor.GetNextImage(imageStream);
                    }

                    Console.WriteLine($"Saved image {imageIndex} to '{imagePath}'.");
                    imageIndex++;
                }

                if (imageIndex == 1)
                {
                    Console.WriteLine("No images were found in the PDF.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during image extraction: {ex.Message}");
        }
    }
}
