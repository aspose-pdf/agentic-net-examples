using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Directory containing the source PDF files
        const string inputDirectory = "InputPdfs";
        // Root directory where extracted images will be saved
        const string outputRootDirectory = "ExtractedImages";

        if (!Directory.Exists(inputDirectory))
        {
            Console.Error.WriteLine($"Input directory not found: {inputDirectory}");
            return;
        }

        // Ensure the root output directory exists
        Directory.CreateDirectory(outputRootDirectory);

        // Process each PDF file in the input directory
        foreach (string pdfPath in Directory.GetFiles(inputDirectory, "*.pdf"))
        {
            // Create a subfolder named after the PDF (without extension)
            string pdfFileName = Path.GetFileNameWithoutExtension(pdfPath);
            string outputFolder = Path.Combine(outputRootDirectory, pdfFileName);
            Directory.CreateDirectory(outputFolder);

            // Use PdfExtractor to extract images from the current PDF
            using (PdfExtractor extractor = new PdfExtractor())
            {
                // Bind the PDF file to the extractor
                extractor.BindPdf(pdfPath);
                // Prepare the extractor for image extraction
                extractor.ExtractImage();

                int imageIndex = 1;
                // Iterate through all extracted images
                while (extractor.HasNextImage())
                {
                    // Save each image as a JPEG file in the subfolder
                    string outputImagePath = Path.Combine(outputFolder, $"image-{imageIndex}.jpg");
                    extractor.GetNextImage(outputImagePath);
                    imageIndex++;
                }
            }

            Console.WriteLine($"Extracted images from '{pdfPath}' to '{outputFolder}'.");
        }

        Console.WriteLine("Batch image extraction completed.");
    }
}