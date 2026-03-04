using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for input PDF, output PDF with stamp, stamp image, and extracted images folder
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "stamped_output.pdf";
        const string stampImagePath = "stamp.png";
        const string extractedImagesDir = "ExtractedImages";

        // Verify input PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Ensure the directory for extracted images exists
        Directory.CreateDirectory(extractedImagesDir);

        // -------------------------------------------------
        // 1. Extract all images from the PDF using PdfExtractor
        // -------------------------------------------------
        PdfExtractor extractor = new PdfExtractor();               // create extractor
        extractor.BindPdf(inputPdfPath);                           // bind source PDF
        extractor.ExtractImage();                                  // start image extraction

        int imageCounter = 1;
        while (extractor.HasNextImage())
        {
            // Save each extracted image as PNG (extension determines format)
            string imageFile = Path.Combine(extractedImagesDir, $"image_{imageCounter}.png");
            extractor.GetNextImage(imageFile);
            Console.WriteLine($"Extracted image {imageCounter} -> {imageFile}");
            imageCounter++;
        }

        // -------------------------------------------------
        // 2. Add an image stamp to every page using PdfFileStamp
        // -------------------------------------------------
        PdfFileStamp fileStamp = new PdfFileStamp();               // create stamp facade
        fileStamp.InputFile = inputPdfPath;                        // source PDF
        fileStamp.OutputFile = outputPdfPath;                      // destination PDF

        // Configure the stamp (image, position, size, opacity)
        Stamp stamp = new Stamp();                                 // create stamp object
        stamp.BindImage(stampImagePath);                           // use the provided image as stamp
        stamp.SetOrigin(100, 500);                                 // lower‑left corner of stamp (x, y)
        stamp.SetImageSize(150, 150);                              // width and height in points
        stamp.Opacity = 0.5f;                                      // 50% transparent
        stamp.IsBackground = false;                               // place on top of page content

        // Apply the stamp to the whole document
        fileStamp.AddStamp(stamp);
        fileStamp.Close();                                         // finalize and save

        Console.WriteLine($"Stamped PDF saved to: {outputPdfPath}");
    }
}