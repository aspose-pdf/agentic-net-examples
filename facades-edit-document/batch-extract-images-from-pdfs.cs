using System;
using System.IO;
using Aspose.Pdf.Facades;

class BatchImageExtractor
{
    static void Main()
    {
        // Input folder containing PDF files
        const string inputFolder = @"C:\InputPdfs";
        // Root output folder where subfolders will be created
        const string outputRoot = @"C:\ExtractedImages";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        // Ensure the root output directory exists
        Directory.CreateDirectory(outputRoot);

        // Get all PDF files in the input folder (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf");
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine("No PDF files found.");
            return;
        }

        foreach (string pdfPath in pdfFiles)
        {
            // Create a subfolder named after the PDF file (without extension)
            string subFolderName = Path.GetFileNameWithoutExtension(pdfPath);
            string subFolderPath = Path.Combine(outputRoot, subFolderName);
            Directory.CreateDirectory(subFolderPath);

            try
            {
                // Use PdfExtractor to extract images from the current PDF
                using (PdfExtractor extractor = new PdfExtractor())
                {
                    extractor.BindPdf(pdfPath);
                    extractor.ExtractImage();

                    int imageIndex = 1;
                    while (extractor.HasNextImage())
                    {
                        // Save each image as a separate file (default format is PDF)
                        // Change the extension if a different image format is desired,
                        // e.g., ".png" with GetNextImage(string, ImageFormat.Png)
                        string imageFile = Path.Combine(subFolderPath, $"image-{imageIndex}.pdf");
                        extractor.GetNextImage(imageFile);
                        imageIndex++;
                    }
                }

                Console.WriteLine($"Extracted images from '{pdfPath}' to '{subFolderPath}'.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }
    }
}