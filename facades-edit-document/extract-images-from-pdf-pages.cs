using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "ExtractedImages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPdf);
            int pageCount = extractor.Document.Pages.Count; // total pages in the PDF

            for (int page = 1; page <= pageCount; page++)
            {
                // Process one page at a time
                extractor.StartPage = page;
                extractor.EndPage = page;
                extractor.ExtractImage();

                int index = 1;
                while (extractor.HasNextImage())
                {
                    string outputPath = Path.Combine(
                        outputDir,
                        $"Image_Page{page}_Index{index}.png");

                    // Save the extracted image as PNG
                    extractor.GetNextImage(outputPath, ImageFormat.Png);
                    index++;
                }
            }
        }

        Console.WriteLine("Image extraction completed.");
    }
}