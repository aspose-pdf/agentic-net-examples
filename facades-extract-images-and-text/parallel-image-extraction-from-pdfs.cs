using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf.Facades; // PdfExtractor resides here

class ParallelImageExtractor
{
    // Entry point
    static async Task Main()
    {
        // Input PDF files – adjust paths as needed
        string[] pdfFiles = {
            "sample1.pdf",
            "sample2.pdf",
            "sample3.pdf"
        };

        // Base output directory for extracted images
        string baseOutputDir = "ExtractedImages";

        // Ensure the base directory exists
        Directory.CreateDirectory(baseOutputDir);

        // Create a task for each PDF file
        Task[] extractionTasks = new Task[pdfFiles.Length];
        for (int i = 0; i < pdfFiles.Length; i++)
        {
            string pdfPath = pdfFiles[i];
            extractionTasks[i] = Task.Run(() => ExtractImagesFromPdf(pdfPath, baseOutputDir));
        }

        // Wait for all extractions to complete
        await Task.WhenAll(extractionTasks);

        Console.WriteLine("All images have been extracted.");
    }

    // Extracts all images from a single PDF and saves them to a dedicated subfolder
    private static void ExtractImagesFromPdf(string pdfPath, string baseOutputDir)
    {
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Create a subfolder named after the PDF (without extension) to store its images
        string pdfNameWithoutExt = Path.GetFileNameWithoutExtension(pdfPath);
        string outputDir = Path.Combine(baseOutputDir, pdfNameWithoutExt);
        Directory.CreateDirectory(outputDir);

        // Use PdfExtractor within a using block to ensure proper disposal
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file to the extractor
            extractor.BindPdf(pdfPath);

            // Prepare the extractor to retrieve images
            extractor.ExtractImage();

            int imageIndex = 1;
            // Loop while there are more images
            while (extractor.HasNextImage())
            {
                // Build the output file name (default image format is JPEG)
                string outputFile = Path.Combine(outputDir, $"image-{imageIndex}.jpg");

                // Save the next image; GetNextImage returns a bool indicating success
                bool success = extractor.GetNextImage(outputFile);
                if (!success)
                {
                    Console.Error.WriteLine($"Failed to extract image {imageIndex} from {pdfPath}");
                }

                imageIndex++;
            }
        }
    }
}