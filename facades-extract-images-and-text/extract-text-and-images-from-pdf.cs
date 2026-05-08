using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging;

class Program
{
    static void Main()
    {
        const string inputPdfPath      = "input.pdf";
        const string outputTextPath    = "extracted_text.txt";
        const string outputImagesFolder = "ExtractedImages";

        // Verify input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the folder for images exists
        Directory.CreateDirectory(outputImagesFolder);

        // PdfExtractor implements IDisposable, so use a using block
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF document to the extractor
            extractor.BindPdf(inputPdfPath);

            // Enable text extraction (0 = pure text mode, 1 = raw ordering mode)
            extractor.ExtractTextMode = 0;

            // Enable image extraction.
            // Default mode (ExtractImageMode.DefinedInResources) extracts all images.
            // Uncomment the next line to extract only actually used images.
            // extractor.ExtractImageMode = ExtractImageMode.ActuallyUsed;

            // Perform the extraction operations
            extractor.ExtractText();
            extractor.ExtractImage();

            // Save the extracted text to a single file
            extractor.GetText(outputTextPath);

            // Save each extracted image as a PNG file
            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                string imagePath = Path.Combine(outputImagesFolder, $"image_{imageIndex}.png");
                extractor.GetNextImage(imagePath, ImageFormat.Png);
                imageIndex++;
            }

            // Explicitly close the extractor (optional, as using will dispose)
            extractor.Close();
        }

        Console.WriteLine("Text and images have been extracted successfully.");
    }
}