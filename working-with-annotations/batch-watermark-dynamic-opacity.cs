using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class BatchWatermark
{
    // Adjust opacity based on page count.
    // Example: more pages → lower opacity, but keep within 0.1‑0.9 range.
    static double ComputeOpacity(int pageCount)
    {
        // Simple linear scaling: 0.9 for 1 page, down to 0.1 for 1000+ pages.
        double opacity = 0.9 - (pageCount - 1) * 0.0008;
        if (opacity < 0.1) opacity = 0.1;
        if (opacity > 0.9) opacity = 0.9;
        return opacity;
    }

    static void Main()
    {
        // Input folder containing PDFs.
        const string inputFolder = @"C:\InputPdfs";
        // Output folder for watermarked PDFs.
        const string outputFolder = @"C:\OutputPdfs";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Gather all PDF files in the input folder.
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf", SearchOption.TopDirectoryOnly);
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine("No PDF files found to process.");
            return;
        }

        foreach (string inputPath in pdfFiles)
        {
            string fileName = Path.GetFileName(inputPath);
            string outputPath = Path.Combine(outputFolder, fileName);

            try
            {
                // Load the PDF document (using statement ensures proper disposal).
                using (Document doc = new Document(inputPath))
                {
                    int pageCount = doc.Pages.Count;
                    double opacity = ComputeOpacity(pageCount);

                    // Create a watermark artifact once and reuse it for each page.
                    WatermarkArtifact watermark = new WatermarkArtifact
                    {
                        IsBackground = true,
                        Opacity = opacity,
                        Text = "CONFIDENTIAL",
                        // Center the watermark using alignment properties.
                        ArtifactHorizontalAlignment = HorizontalAlignment.Center,
                        ArtifactVerticalAlignment = VerticalAlignment.Center,
                        // Define visual style of the text.
                        TextState = new TextState
                        {
                            Font = FontRepository.FindFont("Helvetica"),
                            FontSize = 48,
                            ForegroundColor = Aspose.Pdf.Color.Gray
                        }
                    };

                    // Add the watermark to every page.
                    foreach (Page page in doc.Pages)
                    {
                        page.Artifacts.Add(watermark);
                    }

                    // Save the modified document.
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Watermarked: {fileName} → {outputPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{fileName}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch watermarking completed.");
    }
}