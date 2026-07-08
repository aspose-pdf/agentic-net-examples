using System;
using System.IO;
using Aspose.Pdf;

class BatchWatermark
{
    static void Main()
    {
        // Folder containing source PDFs
        const string inputFolder = @"C:\PdfBatch\Input";
        // Folder where watermarked PDFs will be saved
        const string outputFolder = @"C:\PdfBatch\Output";
        // Path to the watermark image (e.g., PNG, JPEG)
        const string watermarkImagePath = @"C:\Images\watermark.png";

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Get all PDF files in the input folder
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf", SearchOption.TopDirectoryOnly);
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine("No PDF files found in the input folder.");
            return;
        }

        foreach (string inputPath in pdfFiles)
        {
            // Determine output file path (same file name, different folder)
            string outputPath = Path.Combine(outputFolder, Path.GetFileName(inputPath));

            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Iterate through all pages (1‑based indexing)
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    Page page = doc.Pages[i];

                    // Create a new WatermarkArtifact for the current page
                    WatermarkArtifact watermark = new WatermarkArtifact();

                    // Configure the watermark (optional settings)
                    watermark.IsBackground = true;          // place behind page content
                    watermark.Opacity = 0.5;                // 50% transparent

                    // Set the same image for every watermark.
                    // Using SetImage(string) with the same file path ensures the image
                    // is stored only once per document, reducing file size.
                    watermark.SetImage(watermarkImagePath);

                    // Add the watermark to the page's artifact collection
                    page.Artifacts.Add(watermark);
                }

                // Save the modified document to the output location
                doc.Save(outputPath);
            }

            Console.WriteLine($"Watermarked PDF saved: {outputPath}");
        }

        Console.WriteLine("Batch processing completed.");
    }
}