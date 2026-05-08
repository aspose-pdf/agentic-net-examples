using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace

class BatchWatermark
{
    static void Main()
    {
        // Input folder containing PDFs to process
        const string inputFolder = @"C:\InputPdfs";
        // Output folder where watermarked PDFs will be saved
        const string outputFolder = @"C:\OutputPdfs";

        // Resolve absolute paths (helps when the code runs on non‑Windows platforms)
        string inputPath = Path.GetFullPath(inputFolder);
        string outputPath = Path.GetFullPath(outputFolder);

        // Ensure output directory exists
        Directory.CreateDirectory(outputPath);

        // Verify that the input directory exists before enumerating files
        if (!Directory.Exists(inputPath))
        {
            Console.WriteLine($"Input folder not found: {inputPath}");
            return;
        }

        // Enumerate all PDF files in the input folder
        foreach (string pdfPath in Directory.GetFiles(inputPath, "*.pdf"))
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(pdfPath))
            {
                // Determine the number of pages in the current document
                int pageCount = doc.Pages.Count;

                // Compute opacity based on page count.
                // Example: opacity ranges from 0.2 (few pages) to 0.8 (many pages).
                double opacity = Math.Min(0.8, Math.Max(0.2, pageCount / 100.0 + 0.2));

                // Add a WatermarkArtifact to every page
                foreach (Page page in doc.Pages)
                {
                    // Create a new WatermarkArtifact instance
                    WatermarkArtifact watermark = new WatermarkArtifact
                    {
                        Text = "CONFIDENTIAL",
                        Opacity = opacity,
                        IsBackground = true,
                        ArtifactHorizontalAlignment = HorizontalAlignment.Center,
                        ArtifactVerticalAlignment = VerticalAlignment.Center
                    };

                    // Add the artifact to the current page
                    page.Artifacts.Add(watermark);
                }

                // Build the output file path (preserve original file name)
                string outFile = Path.Combine(outputPath, Path.GetFileName(pdfPath));

                // Save the modified document.
                doc.Save(outFile);
            }

            Console.WriteLine($"Processed and saved: {Path.GetFileName(pdfPath)}");
        }

        Console.WriteLine("Batch watermarking completed.");
    }
}
