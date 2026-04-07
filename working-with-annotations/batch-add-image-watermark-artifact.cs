using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF files to process
        string[] pdfFiles = { "doc1.pdf", "doc2.pdf", "doc3.pdf" };
        // Path to the watermark image (PNG, JPEG, etc.)
        string imagePath = "watermark.png";
        // Directory where watermarked PDFs will be saved
        string outputDir = "output";

        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image not found: {imagePath}");
            return;
        }

        // Load the image once into memory – the same byte array will be reused
        byte[] imageBytes = File.ReadAllBytes(imagePath);

        Directory.CreateDirectory(outputDir);

        foreach (string inputPath in pdfFiles)
        {
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"PDF not found: {inputPath}");
                continue;
            }

            string outputPath = Path.Combine(
                outputDir,
                Path.GetFileNameWithoutExtension(inputPath) + "_watermarked.pdf");

            // Open each PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Add the watermark artifact to every page
                foreach (Page page in doc.Pages)
                {
                    WatermarkArtifact watermark = new WatermarkArtifact();

                    // Reuse the same image stream for all artifacts
                    using (MemoryStream ms = new MemoryStream(imageBytes))
                    {
                        watermark.SetImage(ms);
                    }

                    // Center the watermark on the page
                    watermark.ArtifactHorizontalAlignment = HorizontalAlignment.Center;
                    watermark.ArtifactVerticalAlignment   = VerticalAlignment.Center;

                    // Place the watermark above the page content and make it semi‑transparent
                    watermark.IsBackground = false;
                    watermark.Opacity      = 0.3f;

                    // Attach the artifact to the current page
                    page.Artifacts.Add(watermark);
                }

                // Save the modified PDF
                doc.Save(outputPath);
                Console.WriteLine($"Saved watermarked PDF: {outputPath}");
            }
        }
    }
}