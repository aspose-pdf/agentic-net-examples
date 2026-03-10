using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices; // for Resolution

class PdfImageHandlingDemo
{
    static void Main()
    {
        const string inputPdf = "sample.pdf";

        // Ensure the input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // -----------------------------------------------------------------
        // 1. Convert each PDF page to images (BMP, JPEG, PNG, TIFF)
        // -----------------------------------------------------------------
        using (var doc = new Document(inputPdf)) // Document lifecycle managed with using
        {
            // PdfConverter works on a Document instance
            using (var converter = new PdfConverter(doc))
            {
                // Set resolution using Aspose.Pdf.Devices.Resolution
                converter.Resolution = new Resolution(150);

                // Convert all pages
                converter.StartPage = 1;
                converter.EndPage = doc.Pages.Count;
                converter.DoConvert();

                // Output directory for converted images
                string outputDir = "ConvertedImages";
                Directory.CreateDirectory(outputDir);

                int pageIndex = 1;
                while (converter.HasNextImage())
                {
                    // Save as PNG (format inferred from file extension)
                    string pngPath = Path.Combine(outputDir, $"page_{pageIndex}.png");
                    converter.GetNextImage(pngPath);

                    // Save as JPEG (default quality, format inferred)
                    string jpegPath = Path.Combine(outputDir, $"page_{pageIndex}.jpg");
                    converter.GetNextImage(jpegPath);

                    // Save as BMP
                    string bmpPath = Path.Combine(outputDir, $"page_{pageIndex}.bmp");
                    converter.GetNextImage(bmpPath);

                    // Save as TIFF (single‑page TIFF)
                    string tiffPath = Path.Combine(outputDir, $"page_{pageIndex}.tiff");
                    converter.GetNextImage(tiffPath);

                    pageIndex++;
                }
            }
        }

        // -----------------------------------------------------------------
        // 2. Extract embedded images from the PDF (supported formats: BMP, JPEG, PNG, GIF, etc.)
        // -----------------------------------------------------------------
        using (var extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPdf);
            extractor.StartPage = 1;
            extractor.EndPage = extractor.Document.Pages.Count;
            extractor.ExtractImage(); // Prepare extraction

            // Directory for extracted images
            string extractDir = "ExtractedImages";
            Directory.CreateDirectory(extractDir);

            int imgIndex = 1;
            while (extractor.HasNextImage())
            {
                // Save each extracted image as PNG (format inferred from extension)
                string imgPath = Path.Combine(extractDir, $"image_{imgIndex}.png");
                extractor.GetNextImage(imgPath);
                imgIndex++;
            }
        }

        // -----------------------------------------------------------------
        // 3. Add a new image to a specific page of the PDF using PdfFileMend
        // -----------------------------------------------------------------
        const string outputPdfWithImage = "sample_with_image.pdf";
        const string imageToAdd = "logo.png";

        if (!File.Exists(imageToAdd))
        {
            Console.Error.WriteLine($"Image to add not found: {imageToAdd}");
            return;
        }

        using (var mend = new PdfFileMend())
        {
            // Bind the source PDF
            mend.BindPdf(inputPdf);

            // Add the image to page 1 at coordinates (50, 700) - (150, 800)
            using (FileStream imgStream = File.OpenRead(imageToAdd))
            {
                // lowerLeftX, lowerLeftY, upperRightX, upperRightY define the rectangle
                mend.AddImage(imgStream, 1, 50f, 700f, 150f, 800f);
            }

            // Save the modified PDF
            mend.Save(outputPdfWithImage);
        }

        Console.WriteLine("PDF image handling demo completed.");
    }
}
