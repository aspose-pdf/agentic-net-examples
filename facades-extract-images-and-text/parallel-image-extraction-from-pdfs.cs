using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aspose.Pdf.Facades;

class Program
{
    // Entry point – async to allow awaiting Task.WhenAll
    static async Task Main(string[] args)
    {
        // Base directory of the running application (works cross‑platform)
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;

        // Resolve input and output folders relative to the base directory
        string inputDirectory = Path.Combine(baseDir, "InputPdfs");
        string outputRoot = Path.Combine(baseDir, "ExtractedImages");

        // Ensure the input folder exists – if not, fall back to the current working directory
        if (!Directory.Exists(inputDirectory))
        {
            Console.WriteLine($"Input folder '{inputDirectory}' not found. Falling back to current directory.");
            inputDirectory = Directory.GetCurrentDirectory();
        }

        // Ensure the output root exists
        Directory.CreateDirectory(outputRoot);

        // Gather all PDF files from the input directory
        string[] pdfFiles = Directory.GetFiles(inputDirectory, "*.pdf", SearchOption.TopDirectoryOnly);
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine($"No PDF files found in '{inputDirectory}'. Exiting.");
            return;
        }

        // List to hold extraction tasks
        List<Task> extractionTasks = new List<Task>();

        // Create a task for each PDF file
        foreach (string pdfPath in pdfFiles)
        {
            // Capture the current path for the lambda (avoid modified closure issues)
            string pathCopy = pdfPath;
            extractionTasks.Add(Task.Run(() => ExtractImagesFromPdf(pathCopy, outputRoot)));
        }

        // Run all tasks in parallel and wait for completion
        try
        {
            await Task.WhenAll(extractionTasks);
            Console.WriteLine("Image extraction from all PDFs completed.");
        }
        catch (AggregateException aggEx)
        {
            foreach (var ex in aggEx.InnerExceptions)
            {
                Console.Error.WriteLine($"[Error] {ex.Message}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[Unexpected Error] {ex.Message}");
        }
    }

    // Extracts all images from a single PDF and saves them to a dedicated subfolder
    static void ExtractImagesFromPdf(string pdfPath, string outputRoot)
    {
        // Validate input PDF existence
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Create a subfolder named after the PDF (without extension) to store its images
        string pdfFileName = Path.GetFileNameWithoutExtension(pdfPath);
        string pdfOutputDir = Path.Combine(outputRoot, pdfFileName);
        Directory.CreateDirectory(pdfOutputDir);

        try
        {
            // Use PdfExtractor (Facade) to extract images
            using (PdfExtractor extractor = new PdfExtractor())
            {
                // Bind the PDF file to the extractor
                extractor.BindPdf(pdfPath);

                // Prepare the extractor for image extraction
                extractor.ExtractImage();

                int imageIndex = 1;
                // Loop through all available images
                while (extractor.HasNextImage())
                {
                    // Build output file name (default image format is used by GetNextImage)
                    string imagePath = Path.Combine(pdfOutputDir, $"image-{imageIndex}.png");
                    // Save the current image to the file system
                    extractor.GetNextImage(imagePath);
                    imageIndex++;
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to extract images from '{pdfPath}': {ex.Message}");
        }
    }
}
