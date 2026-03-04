using System;
using System.IO;
using System.Drawing.Imaging;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file
        const string inputPdf = "input.pdf";

        // Directory where extracted images will be saved
        const string imagesOutputDir = "ExtractedImages";

        // File where extracted text will be saved
        const string textOutputFile = "extracted_text.txt";

        // Validate input file
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the images output directory exists
        Directory.CreateDirectory(imagesOutputDir);

        // Use PdfExtractor (a Facade) to extract images and text
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF document from file path
            extractor.BindPdf(inputPdf);

            // -------------------------
            // Extract all images
            // -------------------------
            extractor.ExtractImage(); // Prepare image extraction

            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                // Create a stream for each image file
                string imagePath = Path.Combine(imagesOutputDir, $"image_{imageIndex}.png");
                using (FileStream imgStream = new FileStream(imagePath, FileMode.Create, FileAccess.Write))
                {
                    // Save the image in PNG format (you can choose other formats supported by ImageFormat)
                    extractor.GetNextImage(imgStream, ImageFormat.Png);
                }

                Console.WriteLine($"Extracted image {imageIndex} to {imagePath}");
                imageIndex++;
            }

            // -------------------------
            // Extract all text
            // -------------------------
            extractor.ExtractText(); // Prepare text extraction

            // Save the extracted text to a single file
            extractor.GetText(textOutputFile);
            Console.WriteLine($"Extracted text saved to {textOutputFile}");
        }
    }
}