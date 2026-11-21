using System;
using System.IO;
using System.Threading.Tasks;
using System.Drawing.Imaging;
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

        // Create a task for each PDF file
        Task[] tasks = new Task[pdfFiles.Length];
        for (int i = 0; i < pdfFiles.Length; i++)
        {
            string pdfPath = pdfFiles[i];
            tasks[i] = Task.Run(() => ExtractImages(pdfPath, outputRoot));
        }

        // Wait for all extraction tasks to complete
        await Task.WhenAll(tasks);
        Console.WriteLine("Image extraction completed.");
    }

    static void ExtractImages(string pdfPath, string outputRoot)
    {
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Create a subdirectory for images of this PDF
        string pdfName = Path.GetFileNameWithoutExtension(pdfPath);
        string outDir = Path.Combine(outputRoot, pdfName);
        Directory.CreateDirectory(outDir);

        // Use PdfExtractor to extract images
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Load the PDF document
            extractor.BindPdf(pdfPath);
            // No need to set ExtractImageMode – the default extracts all images.
            // Extract images from the whole document (no page range set)
            extractor.ExtractImage();

            int index = 1;
            // Iterate through all extracted images
            while (extractor.HasNextImage())
            {
                string imgPath = Path.Combine(outDir, $"Image_{index}.png");
                extractor.GetNextImage(imgPath, ImageFormat.Png);
                index++;
            }
        }
    }
}
