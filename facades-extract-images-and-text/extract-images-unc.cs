using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "sample.pdf";
        const string uncFolderPath = @"\\server\share\images\";

        if (!Directory.Exists(uncFolderPath))
        {
            Console.Error.WriteLine("UNC folder does not exist: " + uncFolderPath);
            return;
        }

        PdfExtractor extractor = new PdfExtractor();
        extractor.BindPdf(inputPdfPath);
        extractor.ExtractImage();

        int imageIndex = 1;
        while (extractor.HasNextImage())
        {
            string outputImagePath = Path.Combine(uncFolderPath, "image-" + imageIndex.ToString() + ".png");
            extractor.GetNextImage(outputImagePath);
            Console.WriteLine("Extracted image to: " + outputImagePath);
            imageIndex++;
        }

        Console.WriteLine("Image extraction completed.");
    }
}
