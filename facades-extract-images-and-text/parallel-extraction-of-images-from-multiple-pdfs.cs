using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Aspose.Pdf.Facades;

class Program
{
    // Entry point – runs the parallel extraction
    static async Task Main(string[] args)
    {
        // Example PDF files – replace with your own paths or pass via args
        string[] pdfFiles = { "doc1.pdf", "doc2.pdf", "doc3.pdf" };
        string outputRoot = "ExtractedImages";

        await ExtractImagesFromMultiplePdfsAsync(pdfFiles, outputRoot);
    }

    // Extracts images from each PDF in parallel using Task.WhenAll
    static async Task ExtractImagesFromMultiplePdfsAsync(string[] pdfPaths, string outputRoot)
    {
        // Ensure the root output directory exists
        Directory.CreateDirectory(outputRoot);

        // Create a task for each PDF file
        Task[] extractionTasks = pdfPaths.Select(pdfPath => Task.Run(() => ExtractImagesFromPdf(pdfPath, outputRoot))).ToArray();

        // Await completion of all extraction tasks
        await Task.WhenAll(extractionTasks);
    }

    // Helper that extracts images from a single PDF file
    private static void ExtractImagesFromPdf(string pdfPath, string outputRoot)
    {
        if (string.IsNullOrWhiteSpace(pdfPath) || !File.Exists(pdfPath))
            return; // skip invalid entries

        // Create a subfolder for each PDF to avoid filename collisions
        string pdfName = Path.GetFileNameWithoutExtension(pdfPath);
        string pdfOutputDir = Path.Combine(outputRoot, pdfName);
        Directory.CreateDirectory(pdfOutputDir);

        // Use PdfExtractor (Facade) to extract images
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(pdfPath);      // Bind the source PDF
            extractor.ExtractImage();        // Prepare image extraction

            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                // Save each extracted image; the extension determines the format
                string imageFile = Path.Combine(pdfOutputDir, $"image-{imageIndex}.png");
                extractor.GetNextImage(imageFile);
                imageIndex++;
            }
        }
    }
}
