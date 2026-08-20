using System;
using System.IO;
using Aspose.Pdf;

class BatchWatermark
{
    static void Main()
    {
        // Input folder containing PDFs and the watermark image file
        const string inputFolder = @"C:\InputPdfs";
        const string outputFolder = @"C:\OutputPdfs";
        const string watermarkImagePath = @"C:\Resources\watermark.png";

        // Ensure output folder exists
        Directory.CreateDirectory(outputFolder);

        // Get all PDF files in the input folder
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf");

        foreach (string pdfPath in pdfFiles)
        {
            // Load each PDF inside a using block for deterministic disposal
            using (Document doc = new Document(pdfPath))
            {
                // Add the same watermark artifact to every page
                foreach (Page page in doc.Pages)
                {
                    // Create a watermark artifact instance
                    WatermarkArtifact watermark = new WatermarkArtifact();

                    // Use the same image file for all artifacts (reference reuse)
                    watermark.SetImage(watermarkImagePath);

                    // Optional visual settings
                    watermark.IsBackground = true;   // place behind page content
                    watermark.Opacity = 0.3f;        // semi‑transparent

                    // Attach the artifact to the current page
                    page.Artifacts.Add(watermark);
                }

                // Save the modified PDF to the output folder
                string outputPath = Path.Combine(outputFolder, Path.GetFileName(pdfPath));
                doc.Save(outputPath);
            }

            Console.WriteLine($"Processed: {Path.GetFileName(pdfPath)}");
        }

        Console.WriteLine("Batch watermarking completed.");
    }
}