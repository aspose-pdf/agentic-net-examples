using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf.Facades; // PdfExtractor resides in this namespace

class ParallelImageExtractor
{
    // Extracts images from a single PDF file into its own output folder.
    private static void ExtractImages(string pdfPath, string outputRoot)
    {
        // Ensure the source PDF exists.
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        // Create a subdirectory named after the PDF (without extension) to store images.
        string pdfName = Path.GetFileNameWithoutExtension(pdfPath);
        string outputDir = Path.Combine(outputRoot, pdfName);
        Directory.CreateDirectory(outputDir);

        // Use PdfExtractor (Facade) to extract images.
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file.
            extractor.BindPdf(pdfPath);

            // Start the image extraction process.
            extractor.ExtractImage();

            int imageIndex = 1;
            // Loop while there are more images.
            while (extractor.HasNextImage())
            {
                // Build the output file name (default image format is JPEG).
                string imageFile = Path.Combine(outputDir, $"image-{imageIndex}.jpg");

                // Save the next image to the file.
                // GetNextImage returns true on success; we ignore the return value here.
                extractor.GetNextImage(imageFile);

                imageIndex++;
            }
        }

        Console.WriteLine($"Extracted images from '{pdfPath}' to '{outputDir}'.");
    }

    // Asynchronously extracts images from multiple PDFs in parallel.
    public static async Task ExtractImagesFromMultiplePdfsAsync(string[] pdfPaths, string outputRoot)
    {
        // Create a task for each PDF file.
        Task[] extractionTasks = new Task[pdfPaths.Length];
        for (int i = 0; i < pdfPaths.Length; i++)
        {
            string pdfPath = pdfPaths[i];
            extractionTasks[i] = Task.Run(() => ExtractImages(pdfPath, outputRoot));
        }

        // Await all tasks to complete.
        await Task.WhenAll(extractionTasks);
    }

    // Example entry point.
    static async Task Main(string[] args)
    {
        // Example input: list of PDF files to process.
        string[] pdfFiles = new string[]
        {
            "sample1.pdf",
            "sample2.pdf",
            "sample3.pdf"
        };

        // Directory where all extracted images will be placed.
        string outputDirectory = "ExtractedImages";

        // Ensure the root output directory exists.
        Directory.CreateDirectory(outputDirectory);

        // Run the parallel extraction.
        await ExtractImagesFromMultiplePdfsAsync(pdfFiles, outputDirectory);
    }
}