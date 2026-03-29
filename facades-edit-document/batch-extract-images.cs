using System;
using System.IO;
using System.Drawing.Imaging;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        string inputFolder = "pdfs";
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf");
        foreach (string pdfPath in pdfFiles)
        {
            string pdfFileName = Path.GetFileNameWithoutExtension(pdfPath);
            string outputFolder = Path.Combine("output", pdfFileName);
            Directory.CreateDirectory(outputFolder);

            using (PdfExtractor extractor = new PdfExtractor())
            {
                extractor.BindPdf(pdfPath);
                extractor.ExtractImage();

                int imageIndex = 1;
                while (extractor.HasNextImage())
                {
                    string imagePath = Path.Combine(outputFolder, $"image-{imageIndex}.png");
                    extractor.GetNextImage(imagePath, ImageFormat.Png);
                    imageIndex++;
                }
            }

            Console.WriteLine($"Extracted images from '{pdfFileName}' to '{outputFolder}'.");
        }
    }
}