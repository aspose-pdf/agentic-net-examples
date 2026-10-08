using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

class Program
{
    static async Task Main()
    {
        // List of PDF files to process
        string[] pdfFiles = { "doc1.pdf", "doc2.pdf", "doc3.pdf" };

        // Root folder where extracted images will be saved
        string outputRoot = "ExtractedImages";
        Directory.CreateDirectory(outputRoot);

        var tasks = new List<Task>();

        // Create a task for each PDF file
        foreach (var pdfPath in pdfFiles)
        {
            tasks.Add(Task.Run(() => ExtractImagesFromPdf(pdfPath, outputRoot)));
        }

        // Wait for all extraction tasks to complete
        await Task.WhenAll(tasks);
        Console.WriteLine("Image extraction completed for all PDFs.");
    }

    static void ExtractImagesFromPdf(string pdfPath, string outputRoot)
    {
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Create a subdirectory for images of this PDF
        string pdfName = Path.GetFileNameWithoutExtension(pdfPath);
        string pdfOutputDir = Path.Combine(outputRoot, pdfName);
        Directory.CreateDirectory(pdfOutputDir);

        // Use Aspose.Pdf.Facades.PdfExtractor to extract images
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF document
            extractor.BindPdf(pdfPath);

            // Extract all images from the document
            extractor.ExtractImage();

            int imageIndex = 1;
            // Iterate through extracted images
            while (extractor.HasNextImage())
            {
                string imageFile = Path.Combine(pdfOutputDir, $"image_{imageIndex}.png");
                extractor.GetNextImage(imageFile);
                Console.WriteLine($"Extracted: {imageFile}");
                imageIndex++;
            }
        }
    }
}