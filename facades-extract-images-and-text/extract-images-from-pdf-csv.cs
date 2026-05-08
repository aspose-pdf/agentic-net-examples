using System;
using System.IO;
using System.Text;
using Aspose.Pdf.Facades;
using System.Drawing;
using System.Drawing.Imaging;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";                 // source PDF
        const string imagesFolder = "ExtractedImages";      // folder for images
        const string csvPath = "images.csv";                // output CSV file

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        Directory.CreateDirectory(imagesFolder);

        StringBuilder csvBuilder = new StringBuilder();
        csvBuilder.AppendLine("Filename,PageNumber,Width,Height"); // CSV header

        // Use PdfExtractor (facade) inside a using block for deterministic disposal
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file
            extractor.BindPdf(pdfPath);

            // Total number of pages in the document
            int pageCount = extractor.Document.Pages.Count;
            int imageCounter = 1;

            // Process each page individually to capture page numbers
            for (int page = 1; page <= pageCount; page++)
            {
                extractor.StartPage = page;
                extractor.EndPage   = page;

                // Extract images from the current page
                extractor.ExtractImage();

                // Retrieve each extracted image
                while (extractor.HasNextImage())
                {
                    string imageFileName = $"image_page{page}_{imageCounter}.png";
                    string imagePath = Path.Combine(imagesFolder, imageFileName);

                    // Save the image as PNG
                    extractor.GetNextImage(imagePath, ImageFormat.Png);

                    // Determine image dimensions
                    int width, height;
                    using (var img = Image.FromFile(imagePath))
                    {
                        width  = img.Width;
                        height = img.Height;
                    }

                    // Append CSV line
                    csvBuilder.AppendLine($"{imageFileName},{page},{width},{height}");

                    imageCounter++;
                }
            }
        }

        // Write CSV content to file
        File.WriteAllText(csvPath, csvBuilder.ToString(), Encoding.UTF8);
        Console.WriteLine($"Extraction complete. Images saved to '{imagesFolder}'. CSV saved to '{csvPath}'.");
    }
}