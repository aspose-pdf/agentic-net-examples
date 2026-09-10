using System;
using System.IO;
using Aspose.Pdf.Facades;

class BatchImageExtractor
{
    static void Main()
    {
        // Directory containing PDF files
        const string inputDirectory = @"C:\PdfCollection";
        // Root directory where subfolders with extracted images will be created
        const string outputRoot = @"C:\ExtractedImages";

        if (!Directory.Exists(inputDirectory))
        {
            Console.Error.WriteLine($"Input directory not found: {inputDirectory}");
            return;
        }

        Directory.CreateDirectory(outputRoot);

        // Process each PDF file in the input directory
        foreach (string pdfPath in Directory.GetFiles(inputDirectory, "*.pdf"))
        {
            string pdfFileName = Path.GetFileNameWithoutExtension(pdfPath);
            string outputFolder = Path.Combine(outputRoot, pdfFileName);
            Directory.CreateDirectory(outputFolder);

            try
            {
                // Use PdfExtractor to extract images from the current PDF
                using (PdfExtractor extractor = new PdfExtractor())
                {
                    extractor.BindPdf(pdfPath);          // Load PDF
                    extractor.ExtractImage();            // Prepare image extraction

                    int imageIndex = 1;
                    while (extractor.HasNextImage())
                    {
                        // Save each extracted image as a JPEG file
                        string imagePath = Path.Combine(outputFolder, $"image-{imageIndex}.jpg");
                        extractor.GetNextImage(imagePath);
                        imageIndex++;
                    }
                }

                Console.WriteLine($"Images extracted from '{pdfFileName}.pdf' to '{outputFolder}'.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }
    }
}