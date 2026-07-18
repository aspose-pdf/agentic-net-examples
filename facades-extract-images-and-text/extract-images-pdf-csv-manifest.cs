using System;
using System.IO;
using System.Drawing;               // Used only for reading image dimensions
using System.Drawing.Imaging;
using Aspose.Pdf.Facades;          // Facade classes for PDF processing

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";                 // Source PDF
        const string imagesDir  = "ExtractedImages";           // Folder for images
        const string csvPath    = "image_manifest.csv";        // CSV manifest

        // Validate input PDF
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(imagesDir);

        try
        {
            // -----------------------------------------------------------------
            // Convert each PDF page to an image using PdfConverter (Facade API)
            // -----------------------------------------------------------------
            using (PdfConverter converter = new PdfConverter())
            {
                // Bind the PDF file
                converter.BindPdf(inputPdf);

                // Prepare for conversion
                converter.DoConvert();

                int pageNumber = 1;                     // 1‑based page index
                using (StreamWriter csvWriter = new StreamWriter(csvPath, false))
                {
                    // Write CSV header
                    csvWriter.WriteLine("FileName,PageNumber,Width,Height");

                    // Iterate over all extracted images (one per page)
                    while (converter.HasNextImage())
                    {
                        // Build image file name
                        string imageFileName = $"page_{pageNumber}.png";
                        string imagePath = Path.Combine(imagesDir, imageFileName);

                        // Save the image (default format is JPEG; we request PNG)
                        converter.GetNextImage(imagePath, ImageFormat.Png);

                        // Load the saved image to obtain its dimensions
                        using (Image img = Image.FromFile(imagePath))
                        {
                            int width  = img.Width;
                            int height = img.Height;

                            // Write a line to the CSV manifest
                            csvWriter.WriteLine($"{imageFileName},{pageNumber},{width},{height}");
                        }

                        pageNumber++;
                    }
                }
            }

            Console.WriteLine($"Images extracted to '{imagesDir}'.");
            Console.WriteLine($"CSV manifest created at '{csvPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}