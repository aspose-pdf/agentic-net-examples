using System;
using System.IO;
using Aspose.Pdf;

class BatchWatermark
{
    static void Main()
    {
        // Directory containing input PDFs
        string inputDir = @"C:\InputPdfs";
        // Directory to save watermarked PDFs
        string outputDir = @"C:\OutputPdfs";

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Get all PDF files in the input directory
        string[] pdfFiles = Directory.GetFiles(inputDir, "*.pdf");

        foreach (string inputPath in pdfFiles)
        {
            // Determine output file path
            string fileName = Path.GetFileNameWithoutExtension(inputPath);
            string outputPath = Path.Combine(outputDir, fileName + "_watermarked.pdf");

            // Open the PDF document
            using (Document doc = new Document(inputPath))
            {
                // Calculate opacity based on page count (example: 5% per page, capped at 0.9)
                double opacity = Math.Min(0.9, doc.Pages.Count * 0.05);

                // Create a WatermarkArtifact instance
                WatermarkArtifact watermark = new WatermarkArtifact();
                watermark.Text = "CONFIDENTIAL";
                watermark.IsBackground = true;          // place behind page content
                watermark.Opacity = opacity;            // set calculated opacity
                watermark.ArtifactHorizontalAlignment = HorizontalAlignment.Center;
                watermark.ArtifactVerticalAlignment = VerticalAlignment.Center;

                // Add the watermark to every page
                foreach (Page page in doc.Pages)
                {
                    page.Artifacts.Add(watermark);
                }

                // Save the modified document
                doc.Save(outputPath);
            }

            Console.WriteLine($"Processed: {inputPath} -> {outputPath}");
        }
    }
}