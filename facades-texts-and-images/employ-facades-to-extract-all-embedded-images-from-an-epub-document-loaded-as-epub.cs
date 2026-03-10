using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string epubPath   = "input.epub";          // Path to the source EPUB file
        const string outputDir  = "ExtractedImages";    // Folder where images will be saved

        if (!File.Exists(epubPath))
        {
            Console.Error.WriteLine($"EPUB file not found: {epubPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the EPUB as a PDF document using EpubLoadOptions
        using (Document pdfDoc = new Document(epubPath, new EpubLoadOptions()))
        {
            // Initialize the PdfExtractor facade
            using (PdfExtractor extractor = new PdfExtractor())
            {
                // Bind the loaded PDF document to the extractor
                extractor.BindPdf(pdfDoc);

                // Choose extraction mode (DefinedInResources extracts all images in resources)
                extractor.ExtractImageMode = ExtractImageMode.DefinedInResources;

                // Perform the extraction
                extractor.ExtractImage();

                int imageIndex = 1;
                // Retrieve each extracted image and save it to a file
                while (extractor.HasNextImage())
                {
                    string imagePath = Path.Combine(outputDir, $"image_{imageIndex}.png");
                    extractor.GetNextImage(imagePath);
                    Console.WriteLine($"Saved image {imageIndex} to {imagePath}");
                    imageIndex++;
                }
            }
        }

        Console.WriteLine("Image extraction completed.");
    }
}