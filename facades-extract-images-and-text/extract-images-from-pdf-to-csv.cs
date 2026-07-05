using System;
using System.IO;
using System.Text;
using System.Drawing;                     // Used only for reading image dimensions
using System.Drawing.Imaging;            // ImageFormat enum
using Aspose.Pdf.Facades;                // PdfExtractor resides here

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputCsvPath = "images.csv";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Prepare CSV writer
        using (var csvWriter = new StreamWriter(outputCsvPath, false, Encoding.UTF8))
        {
            // Write CSV header
            csvWriter.WriteLine("Filename,PageNumber,Width,Height");

            // Use PdfExtractor to pull images page‑by‑page
            using (var extractor = new Aspose.Pdf.Facades.PdfExtractor())
            {
                extractor.BindPdf(inputPdfPath);

                // Total pages in the document
                int pageCount = extractor.Document.Pages.Count;

                for (int pageNum = 1; pageNum <= pageCount; pageNum++)
                {
                    // Restrict extraction to a single page
                    extractor.StartPage = pageNum;
                    extractor.EndPage   = pageNum;

                    // Extract images from the current page
                    extractor.ExtractImage();

                    int imageIndex = 1;
                    while (extractor.HasNextImage())
                    {
                        // Build a unique filename for each extracted image
                        string imageFileName = $"page{pageNum}_image{imageIndex}.png";

                        // Save the image as PNG (you can choose another format if desired)
                        extractor.GetNextImage(imageFileName, ImageFormat.Png);

                        // Obtain image dimensions
                        int width, height;
                        using (Image img = Image.FromFile(imageFileName))
                        {
                            width  = img.Width;
                            height = img.Height;
                        }

                        // Write CSV line
                        csvWriter.WriteLine($"{imageFileName},{pageNum},{width},{height}");

                        imageIndex++;
                    }
                }
            }
        }

        Console.WriteLine($"Image extraction complete. CSV saved to '{outputCsvPath}'.");
    }
}