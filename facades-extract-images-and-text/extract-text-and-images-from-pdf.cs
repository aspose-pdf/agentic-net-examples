using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string textOutputPath = "extracted_text.txt";
        const string imagesOutputDir = "ExtractedImages";

        // Verify the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Ensure the images output directory exists
        Directory.CreateDirectory(imagesOutputDir);

        // PdfExtractor implements IDisposable, so use a using block for deterministic cleanup
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file to the extractor
            extractor.BindPdf(pdfPath);

            // Optional: set extraction modes (defaults are suitable for most scenarios)
            // 0 = pure text mode, 1 = raw ordering mode
            extractor.ExtractTextMode = 0;
            // extractor.ExtractImageMode = ExtractImageMode.DefinedInResources; // default

            // ----------- Text Extraction -----------
            extractor.ExtractText();                     // Perform text extraction
            extractor.GetText(textOutputPath);           // Save all extracted text to a file

            // ----------- Image Extraction -----------
            extractor.ExtractImage();                    // Perform image extraction

            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                // Save each image as a separate file (PNG format by default)
                string imagePath = Path.Combine(imagesOutputDir, $"image-{imageIndex}.png");
                extractor.GetNextImage(imagePath);
                imageIndex++;
            }
        }

        Console.WriteLine("Text and image extraction completed successfully.");
    }
}