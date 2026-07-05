using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging;

class Program
{
    // Entry point – runs the parallel extraction
    static async Task Main(string[] args)
    {
        // List of PDF files to process (adjust paths as needed)
        string[] pdfFiles = { "doc1.pdf", "doc2.pdf", "doc3.pdf" };
        // Root folder where all extracted images will be placed
        string outputRoot = "ExtractedImages";

        // Ensure the root output directory exists
        Directory.CreateDirectory(outputRoot);

        // Create a task for each PDF file
        var extractionTasks = new List<Task>();
        foreach (string pdfPath in pdfFiles)
        {
            extractionTasks.Add(Task.Run(() => ExtractImagesFromPdf(pdfPath, outputRoot)));
        }

        // Wait for all extraction tasks to complete
        await Task.WhenAll(extractionTasks);

        Console.WriteLine("Image extraction completed for all PDFs.");
    }

    // Extracts all images from a single PDF and saves them to a dedicated folder
    static void ExtractImagesFromPdf(string pdfPath, string outputRoot)
    {
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Create a subdirectory for this PDF to avoid filename collisions
        string pdfName = Path.GetFileNameWithoutExtension(pdfPath);
        string pdfOutputDir = Path.Combine(outputRoot, pdfName);
        Directory.CreateDirectory(pdfOutputDir);

        // Use PdfExtractor (a Facade) to pull images from the PDF
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the source PDF file
            extractor.BindPdf(pdfPath);
            // Prepare the extractor to retrieve images
            extractor.ExtractImage();

            int imageIndex = 1;
            // Loop while there are more images available
            while (extractor.HasNextImage())
            {
                // Build a unique filename for each extracted image
                string imageFile = Path.Combine(pdfOutputDir, $"image-{imageIndex}.png");
                // Save the image as PNG (you can choose other formats via ImageFormat)
                extractor.GetNextImage(imageFile, ImageFormat.Png);
                imageIndex++;
            }
        }
    }
}