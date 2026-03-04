using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file
        const string inputPdf = "sample.pdf";

        // Folder where extracted images will be saved
        const string imagesFolder = "ExtractedImages";

        // File where extracted text will be saved
        const string textOutputFile = "extracted_text.txt";

        // Verify that the input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the images output folder exists
        Directory.CreateDirectory(imagesFolder);

        // PdfExtractor implements IDisposable, so use a using block for deterministic cleanup
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF document to the extractor
            extractor.BindPdf(inputPdf);

            // -------------------------------------------------
            // Extract images
            // -------------------------------------------------
            // Use the mode that extracts images actually used on the page
            extractor.ExtractImageMode = ExtractImageMode.ActuallyUsed;

            // Perform the image extraction operation
            extractor.ExtractImage();

            int imageIndex = 1;
            // Iterate through all extracted images
            while (extractor.HasNextImage())
            {
                // Build a file name for each image (default format is JPEG)
                string imagePath = Path.Combine(imagesFolder, $"image-{imageIndex}.jpg");

                // Save the current image to the file system
                extractor.GetNextImage(imagePath);

                imageIndex++;
            }

            // -------------------------------------------------
            // Extract text
            // -------------------------------------------------
            // Set text extraction mode to pure text (0) – this is the default, but set explicitly for clarity
            extractor.ExtractTextMode = 0;

            // Perform the text extraction operation
            extractor.ExtractText();

            // Save the extracted text to a single file
            extractor.GetText(textOutputFile);
        }

        Console.WriteLine("Image and text extraction completed successfully.");
    }
}