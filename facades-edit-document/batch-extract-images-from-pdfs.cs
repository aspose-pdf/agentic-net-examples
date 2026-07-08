using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Folder containing the PDF files to process
        const string inputFolder = "pdfs";
        // Root folder where extracted images will be saved
        const string outputRoot = "extracted_images";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        // Ensure the root output directory exists
        Directory.CreateDirectory(outputRoot);

        // Process each PDF file in the input folder
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            // Create a subfolder named after the PDF (without extension)
            string pdfFileName = Path.GetFileNameWithoutExtension(pdfPath);
            string pdfOutputDir = Path.Combine(outputRoot, pdfFileName);
            Directory.CreateDirectory(pdfOutputDir);

            // Use PdfExtractor to extract images from the current PDF
            using (PdfExtractor extractor = new PdfExtractor())
            {
                extractor.BindPdf(pdfPath);      // Load the PDF
                extractor.ExtractImage();        // Prepare image extraction

                int imageIndex = 1;
                while (extractor.HasNextImage())
                {
                    // Save each extracted image as a JPEG file
                    string imagePath = Path.Combine(pdfOutputDir, $"image-{imageIndex}.jpg");
                    extractor.GetNextImage(imagePath); // Returns bool; ignored here
                    imageIndex++;
                }
            }

            Console.WriteLine($"Images extracted from '{pdfPath}' to '{pdfOutputDir}'.");
        }
    }
}