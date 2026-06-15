using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // List of PDF files to process
        string[] pdfFiles = { "doc1.pdf", "doc2.pdf", "doc3.pdf" };
        // Path to the watermark image (PNG, JPEG, etc.)
        string watermarkPath = "watermark.png";
        // Directory where watermarked PDFs will be saved
        string outputDir = "Watermarked";

        if (!File.Exists(watermarkPath))
        {
            Console.Error.WriteLine($"Watermark image not found: {watermarkPath}");
            return;
        }

        // Load the image once into a byte array – this reference will be reused for all artifacts
        byte[] imageData = File.ReadAllBytes(watermarkPath);

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        foreach (string inputPath in pdfFiles)
        {
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"PDF not found: {inputPath}");
                continue;
            }

            string outputPath = Path.Combine(outputDir, Path.GetFileName(inputPath));

            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Add a watermark artifact to every page
                foreach (Page page in doc.Pages)
                {
                    // Create a new artifact for the current page
                    WatermarkArtifact watermark = new WatermarkArtifact();

                    // Reuse the same image data for each artifact
                    using (MemoryStream imgStream = new MemoryStream(imageData))
                    {
                        watermark.SetImage(imgStream);
                    }

                    // Configure visual appearance (optional)
                    watermark.IsBackground = true;   // place behind page content
                    watermark.Opacity = 0.3;         // semi‑transparent

                    // Example of explicit positioning (commented out)
                    // watermark.Position = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

                    // Attach the artifact to the page
                    page.Artifacts.Add(watermark);
                }

                // Save the modified PDF to the output folder
                doc.Save(outputPath);
            }

            Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
        }
    }
}