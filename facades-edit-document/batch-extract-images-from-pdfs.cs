using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging;

class Program
{
    static void Main()
    {
        const string inputDirectory = "pdfs"; // folder containing source PDF files
        const string outputRoot = "extracted_images"; // root folder for all output subfolders

        if (!Directory.Exists(inputDirectory))
        {
            Console.Error.WriteLine($"Input directory not found: {inputDirectory}");
            return;
        }

        Directory.CreateDirectory(outputRoot);

        string[] pdfFiles = Directory.GetFiles(inputDirectory, "*.pdf");
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine("No PDF files found to process.");
            return;
        }

        foreach (string pdfPath in pdfFiles)
        {
            string pdfFileName = Path.GetFileNameWithoutExtension(pdfPath);
            string outputFolder = Path.Combine(outputRoot, pdfFileName);
            Directory.CreateDirectory(outputFolder);

            using (PdfExtractor extractor = new PdfExtractor())
            {
                extractor.BindPdf(pdfPath);
                extractor.ExtractImage();

                int imageIndex = 1;
                while (extractor.HasNextImage())
                {
                    string imageFile = Path.Combine(outputFolder, $"image-{imageIndex}.png");
                    // Save each extracted image as PNG. You can change the format if needed.
                    extractor.GetNextImage(imageFile, ImageFormat.Png);
                    imageIndex++;
                }
            }

            Console.WriteLine($"Extracted images from '{pdfFileName}.pdf' to folder '{outputFolder}'.");
        }
    }
}