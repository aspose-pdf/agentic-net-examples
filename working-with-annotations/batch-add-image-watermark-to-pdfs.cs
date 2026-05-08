using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // List of input PDF files to process
        var inputFiles = new List<string>
        {
            "doc1.pdf",
            "doc2.pdf",
            "doc3.pdf"
        };

        // Path to the image that will be used as a watermark
        const string imagePath = "watermark.png";

        // Directory where watermarked PDFs will be saved
        const string outputDir = "Watermarked";
        Directory.CreateDirectory(outputDir);

        foreach (var inputPath in inputFiles)
        {
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"File not found: {inputPath}");
                continue;
            }

            // Build output file name
            string fileName = System.IO.Path.GetFileNameWithoutExtension(inputPath);
            string outputPath = System.IO.Path.Combine(outputDir, $"{fileName}_wm.pdf");

            // Load the PDF document (lifecycle rule: use using)
            using (Document doc = new Document(inputPath))
            {
                // Add the same image as a watermark to every page
                foreach (Page page in doc.Pages)
                {
                    // Create a watermark artifact
                    WatermarkArtifact watermark = new WatermarkArtifact
                    {
                        IsBackground = true,   // place behind page content
                        Opacity = 0.3          // semi‑transparent
                    };

                    // Use the same image file for all artifacts to keep file size low
                    watermark.SetImage(imagePath);

                    // Attach the artifact to the current page
                    page.Artifacts.Add(watermark);
                }

                // Save the modified document (lifecycle rule: save inside using)
                doc.Save(outputPath);
            }

            Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
        }
    }
}