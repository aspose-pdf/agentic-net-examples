using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Folder containing source PDFs
        const string inputFolder = "input_pdfs";
        // Folder where watermarked PDFs will be saved
        const string outputFolder = "output_pdfs";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string inputPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName = Path.GetFileName(inputPath);
            string outputPath = Path.Combine(outputFolder, fileName);

            // Load the PDF document (using block ensures proper disposal)
            using (Document doc = new Document(inputPath))
            {
                // Page count uses 1‑based indexing (Pages[1] is the first page)
                int pageCount = doc.Pages.Count;

                // Example opacity calculation: 0.1 per page, capped at 0.9, minimum 0.1
                double opacity = Math.Min(0.9, Math.Max(0.1, pageCount * 0.1));

                // Create a watermark artifact
                WatermarkArtifact watermark = new WatermarkArtifact
                {
                    Text = "CONFIDENTIAL",
                    Opacity = opacity,
                    IsBackground = true, // place behind page content
                    ArtifactHorizontalAlignment = HorizontalAlignment.Center,
                    ArtifactVerticalAlignment = VerticalAlignment.Center
                };

                // Add the watermark to every page in the document
                foreach (Page page in doc.Pages)
                {
                    page.Artifacts.Add(watermark);
                }

                // Save the modified PDF (standard PDF output)
                doc.Save(outputPath);
            }

            Console.WriteLine($"Processed '{fileName}' with opacity {Math.Min(0.9, Math.Max(0.1, new Document(inputPath).Pages.Count * 0.1)):F2}");
        }
    }
}