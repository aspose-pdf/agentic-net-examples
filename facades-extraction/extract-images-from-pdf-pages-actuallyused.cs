using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputFolder = "ExtractedImages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPdf);
            // The default extraction mode returns only images that are actually used.
            // If the library version exposes the enum, you can set it explicitly:
            // extractor.ExtractImageMode = ExtractImageMode.ActuallyUsed;

            for (int page = 10; page <= 12; page++)
            {
                // Process one page at a time so we know the source page number.
                extractor.StartPage = page;
                extractor.EndPage   = page;
                extractor.ExtractImage();

                int imageIndex = 1;
                while (extractor.HasNextImage())
                {
                    string outPath = Path.Combine(outputFolder,
                        $"page_{page}_image_{imageIndex}.jpg");
                    extractor.GetNextImage(outPath, ImageFormat.Jpeg);
                    imageIndex++;
                }
            }
        }

        Console.WriteLine("Image extraction completed.");
    }
}
