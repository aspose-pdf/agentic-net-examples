using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf.Facades;

class ParallelImageExtractor
{
    // Extracts images from multiple PDF files concurrently.
    // Each PDF's images are saved into a subfolder named after the PDF (without extension).
    public static async Task ExtractImagesFromPdfsAsync(string[] pdfFiles, string outputRoot)
    {
        // Ensure the root output directory exists.
        Directory.CreateDirectory(outputRoot);

        // Create a task for each PDF file.
        Task[] extractionTasks = new Task[pdfFiles.Length];
        for (int idx = 0; idx < pdfFiles.Length; idx++)
        {
            string pdfPath = pdfFiles[idx];
            extractionTasks[idx] = Task.Run(() =>
            {
                // Validate input file.
                if (!File.Exists(pdfPath))
                {
                    Console.Error.WriteLine($"File not found: {pdfPath}");
                    return;
                }

                // Prepare output subdirectory for this PDF.
                string pdfNameWithoutExt = Path.GetFileNameWithoutExtension(pdfPath);
                string pdfOutputDir = Path.Combine(outputRoot, pdfNameWithoutExt);
                Directory.CreateDirectory(pdfOutputDir);

                // Use PdfExtractor (Facade) to extract images.
                using (PdfExtractor extractor = new PdfExtractor())
                {
                    extractor.BindPdf(pdfPath);          // Bind the source PDF.
                    extractor.ExtractImage();            // Prepare image extraction.

                    int imageIndex = 1;
                    while (extractor.HasNextImage())
                    {
                        // Save each image as a separate file (default format is PNG/JPEG based on source).
                        string imageFile = Path.Combine(pdfOutputDir, $"image-{imageIndex}.png");
                        extractor.GetNextImage(imageFile);
                        imageIndex++;
                    }
                }

                Console.WriteLine($"Completed extraction for: {pdfPath}");
            });
        }

        // Await all extraction tasks to finish.
        await Task.WhenAll(extractionTasks);
    }

    // Example usage.
    static async Task Main()
    {
        // List of PDF files to process.
        string[] pdfFiles = new[]
        {
            "sample1.pdf",
            "sample2.pdf",
            "sample3.pdf"
        };

        // Directory where extracted images will be stored.
        string outputRoot = "ExtractedImages";

        await ExtractImagesFromPdfsAsync(pdfFiles, outputRoot);
        Console.WriteLine("All image extractions completed.");
    }
}