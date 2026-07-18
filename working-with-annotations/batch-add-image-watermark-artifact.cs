using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF files to process
        string[] pdfFiles = { "doc1.pdf", "doc2.pdf", "doc3.pdf" };
        // Path to the watermark image (same image used for all PDFs)
        string watermarkImagePath = "watermark.png";
        // Directory where watermarked PDFs will be saved
        string outputDir = "Output";

        Directory.CreateDirectory(outputDir);

        foreach (string inputPath in pdfFiles)
        {
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"File not found: {inputPath}");
                continue;
            }

            // Build output file name (original name with _wm suffix)
            string outputPath = Path.Combine(outputDir,
                Path.GetFileNameWithoutExtension(inputPath) + "_wm.pdf");

            // Load the PDF document (using rule: Document creation via constructor)
            using (Document doc = new Document(inputPath))
            {
                // Add the same watermark artifact to every page
                foreach (Page page in doc.Pages)
                {
                    WatermarkArtifact watermark = new WatermarkArtifact();
                    // Use the same image reference for all artifacts
                    watermark.SetImage(watermarkImagePath);
                    watermark.IsBackground = true;   // place behind page content
                    watermark.Opacity = 0.5;         // semi‑transparent

                    page.Artifacts.Add(watermark);
                }

                // Save the modified PDF (using rule: Document.Save with path)
                doc.Save(outputPath);
            }

            Console.WriteLine($"Watermarked PDF saved: {outputPath}");
        }
    }
}