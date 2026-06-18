using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Folder containing the PDF files to process
        const string inputFolder = @"C:\PdfCollection";
        // Root folder where extracted images will be saved
        const string outputRoot = @"C:\ExtractedImages";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        // Ensure the root output folder exists
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
            string pdfName = Path.GetFileNameWithoutExtension(pdfPath);
            string pdfOutputFolder = Path.Combine(outputRoot, pdfName);
            Directory.CreateDirectory(pdfOutputFolder);

            // Use PdfExtractor to pull images from the current PDF
            using (PdfExtractor extractor = new PdfExtractor())
            {
                extractor.BindPdf(pdfPath);
                extractor.ExtractImage();

                int imageIndex = 1;
                while (extractor.HasNextImage())
                {
                    // Build the output file name (e.g., image-1.png, image-2.jpg, etc.)
                    // The original image format is preserved when we omit the ImageFormat argument.
                    string imageFile = Path.Combine(pdfOutputFolder, $"image-{imageIndex}");
                    extractor.GetNextImage(imageFile);
                    imageIndex++;
                }
            }

            Console.WriteLine($"Extracted images from '{pdfName}.pdf' to '{pdfOutputFolder}'.");
        }

        Console.WriteLine("Batch image extraction completed.");
    }
}
