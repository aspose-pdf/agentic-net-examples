using System;
using System.IO;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputCsv = "image_manifest.csv";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Prepare CSV file with header
        using (StreamWriter csvWriter = new StreamWriter(outputCsv, false, Encoding.UTF8))
        {
            csvWriter.WriteLine("FileName,PageNumber,Width,Height");

            // Open the PDF to obtain the page count
            using (Document document = new Document(inputPdf))
            {
                int pageCount = document.Pages.Count; // 1‑based indexing

                // Iterate through each page and extract images from that page
                for (int pageNumber = 1; pageNumber <= pageCount; pageNumber++)
                {
                    // PdfExtractor works on a specific page range.
                    using (PdfExtractor extractor = new PdfExtractor())
                    {
                        extractor.BindPdf(inputPdf);
                        extractor.StartPage = pageNumber;
                        extractor.EndPage   = pageNumber;
                        extractor.ExtractImage();

                        int imageIndex = 1;
                        while (extractor.HasNextImage())
                        {
                            // Build a unique file name for each extracted image
                            string imageFileName = $"page{pageNumber}_img{imageIndex}.png";

                            // Save the image as PNG
                            extractor.GetNextImage(imageFileName, ImageFormat.Png);

                            // Retrieve image dimensions using System.Drawing.Image (fully qualified to avoid ambiguity)
                            using (var img = System.Drawing.Image.FromFile(imageFileName))
                            {
                                int width  = img.Width;
                                int height = img.Height;

                                // Write a line to the CSV manifest
                                csvWriter.WriteLine($"{imageFileName},{pageNumber},{width},{height}");
                            }

                            imageIndex++;
                        }
                    }
                }
            }

            Console.WriteLine($"Image extraction complete. Manifest saved to '{outputCsv}'.");
        }
    }
}
