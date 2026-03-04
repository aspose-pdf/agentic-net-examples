using System;
using System.IO;
using Aspose.Pdf.Facades;   // Facade classes (PdfExtractor, etc.)
using Aspose.Pdf;          // For ExtractImageMode enum

class Program
{
    static void Main()
    {
        const string inputPdf = "sample.pdf";
        const string imageOutputDir = "ExtractedImages";
        const string textOutputFile = "extracted_text.txt";

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // Ensure the folder for extracted images exists
        Directory.CreateDirectory(imageOutputDir);

        // PdfExtractor implements IDisposable, so wrap it in a using block
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF document to the extractor facade
            extractor.BindPdf(inputPdf);

            // Choose the image extraction mode.
            // DefinedInResources extracts all images defined in resources.
            // ActuallyUsed extracts only images that are rendered on the page.
            extractor.ExtractImageMode = ExtractImageMode.ActuallyUsed;

            // Start image extraction
            extractor.ExtractImage();

            int imgIndex = 1;
            // Retrieve each image until none are left
            while (extractor.HasNextImage())
            {
                string imgPath = Path.Combine(imageOutputDir, $"image-{imgIndex}.png");
                // GetNextImage saves the current image to the specified file.
                // The default format is PNG; you can specify a different ImageFormat if needed.
                extractor.GetNextImage(imgPath);
                Console.WriteLine($"Image {imgIndex} saved to: {imgPath}");
                imgIndex++;
            }

            // Extract text from the PDF (default pure text mode)
            extractor.ExtractText();

            // Save the extracted text to a file
            extractor.GetText(textOutputFile);
            Console.WriteLine($"Extracted text saved to: {textOutputFile}");
        }
    }
}