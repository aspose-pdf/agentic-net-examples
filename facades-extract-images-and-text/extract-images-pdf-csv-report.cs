using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using Aspose.Pdf.Facades;

class ImageExtractor
{
    static void Main()
    {
        const string pdfPath = "input.pdf";                 // source PDF
        const string imagesFolder = "ExtractedImages";      // folder for extracted images
        const string csvPath = "image_report.csv";          // CSV output file

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        Directory.CreateDirectory(imagesFolder);

        var csvLines = new List<string>();
        csvLines.Add("Filename,PageNumber,Width,Height");

        int imageCounter = 1;

        // Use PdfExtractor from Aspose.Pdf.Facades as required
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(pdfPath);

            // Get total number of pages from the bound document
            int pageCount = extractor.Document.Pages.Count;

            // Iterate through each page to know the page number of each image
            for (int page = 1; page <= pageCount; page++)
            {
                extractor.StartPage = page;
                extractor.EndPage = page;
                extractor.ExtractImage();

                while (extractor.HasNextImage())
                {
                    // Save each image as PNG (easily readable for dimensions)
                    string imageFileName = $"image_{page}_{imageCounter}.png";
                    string imageFullPath = Path.Combine(imagesFolder, imageFileName);

                    // Extract the image to the file
                    extractor.GetNextImage(imageFullPath, ImageFormat.Png);

                    // Retrieve image dimensions
                    using (Image img = Image.FromFile(imageFullPath))
                    {
                        int width = img.Width;
                        int height = img.Height;
                        csvLines.Add($"{imageFileName},{page},{width},{height}");
                    }

                    imageCounter++;
                }
            }
        }

        // Write CSV report
        File.WriteAllLines(csvPath, csvLines);
        Console.WriteLine($"Extraction complete. Images saved to '{imagesFolder}'. CSV report saved to '{csvPath}'.");
    }
}