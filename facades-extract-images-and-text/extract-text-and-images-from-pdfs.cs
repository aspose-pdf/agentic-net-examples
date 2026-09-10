using System;
using System.IO;
using Aspose.Pdf.Facades;

namespace PdfProcessor
{
    class Program
    {
        // Input, output and image directories are expected to be mounted at /data
        private const string InputDir = "/data/input";
        private const string OutputDir = "/data/output";
        private const string ImageDir = "/data/images";

        static void Main(string[] args)
        {
            // Ensure directories exist
            Directory.CreateDirectory(InputDir);
            Directory.CreateDirectory(OutputDir);
            Directory.CreateDirectory(ImageDir);

            // Process each PDF file found in the input directory
            foreach (var pdfPath in Directory.GetFiles(InputDir, "*.pdf"))
            {
                try
                {
                    ProcessPdf(pdfPath);
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
                }
            }

            Console.WriteLine("PDF processing completed.");
        }

        private static void ProcessPdf(string pdfPath)
        {
            // Derive base file name without extension
            string baseName = Path.GetFileNameWithoutExtension(pdfPath);

            // Output text file path
            string textOutputPath = Path.Combine(OutputDir, $"{baseName}.txt");

            // Counter for extracted images – declared outside the using block so it is visible later
            int extractedImageCount = 0;

            // Create a PdfExtractor facade and bind the PDF file
            using (PdfExtractor extractor = new PdfExtractor())
            {
                // Bind the source PDF
                extractor.BindPdf(pdfPath);

                // ------------------- Extract Text -------------------
                extractor.ExtractText();                     // extracts all text using Unicode encoding
                extractor.GetText(textOutputPath);           // saves extracted text to a .txt file

                // ------------------- Extract Images -------------------
                // Optional: set higher resolution for clearer images
                extractor.Resolution = 300;                  // default is 150 DPI
                extractor.ExtractImage();                    // prepares image extraction

                int imageIndex = 1;
                while (extractor.HasNextImage())
                {
                    // Save each image as PNG (default format is PNG)
                    string imagePath = Path.Combine(ImageDir, $"{baseName}_img{imageIndex}.png");
                    extractor.GetNextImage(imagePath);
                    imageIndex++;
                }
                // Store the number of images extracted for the final log message
                extractedImageCount = Math.Max(0, imageIndex - 1);

                // Close the facade (also disposes the bound Document)
                extractor.Close();
            }

            Console.WriteLine($"Processed '{pdfPath}' -> text: '{textOutputPath}', images: {extractedImageCount} extracted.");
        }
    }
}
