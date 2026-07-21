using System;
using System.IO;
using Aspose.Pdf.Facades;

namespace PdfProcessingService
{
    // Simple console service that extracts text and images from PDFs located in a mounted volume.
    // Input and output directories are provided via environment variables or default to "/data/input" and "/data/output".
    class Program
    {
        static void Main(string[] args)
        {
            // Resolve input and output paths (mounted volumes)
            string inputDir = Environment.GetEnvironmentVariable("PDF_INPUT_DIR") ?? "/data/input";
            string outputDir = Environment.GetEnvironmentVariable("PDF_OUTPUT_DIR") ?? "/data/output";

            if (!Directory.Exists(inputDir))
            {
                Console.Error.WriteLine($"Input directory does not exist: {inputDir}");
                return;
            }

            // Ensure output directory exists
            Directory.CreateDirectory(outputDir);

            // Process each PDF file in the input directory
            foreach (string pdfPath in Directory.GetFiles(inputDir, "*.pdf"))
            {
                try
                {
                    ProcessPdf(pdfPath, outputDir);
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
                }
            }

            Console.WriteLine("PDF processing completed.");
        }

        // Extracts text and images from a single PDF file.
        private static void ProcessPdf(string pdfPath, string outputRoot)
        {
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(pdfPath);
            string pdfOutputDir = Path.Combine(outputRoot, fileNameWithoutExt);
            Directory.CreateDirectory(pdfOutputDir);

            // Use PdfExtractor (Facade) inside a using block for deterministic disposal.
            using (PdfExtractor extractor = new PdfExtractor())
            {
                // Bind the PDF file to the extractor.
                extractor.BindPdf(pdfPath);

                // -------------------------
                // Extract full text to a .txt file.
                // -------------------------
                extractor.ExtractText();
                string textOutputPath = Path.Combine(pdfOutputDir, $"{fileNameWithoutExt}.txt");
                extractor.GetText(textOutputPath);

                // -------------------------
                // Extract images to separate files.
                // -------------------------
                extractor.ExtractImage();
                int imageIndex = 1;
                while (extractor.HasNextImage())
                {
                    // Images are saved as PNG by default; you can specify a different format via overloads if needed.
                    string imagePath = Path.Combine(pdfOutputDir, $"image-{imageIndex}.png");
                    extractor.GetNextImage(imagePath);
                    imageIndex++;
                }
            }

            Console.WriteLine($"Processed '{Path.GetFileName(pdfPath)}' -> output folder: {pdfOutputDir}");
        }
    }
}