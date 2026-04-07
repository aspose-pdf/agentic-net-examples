using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class BatchWatermark
{
    static void Main()
    {
        // Input folder containing PDF files
        const string inputFolder = @"C:\InputPdfs";
        // Output folder for watermarked PDFs
        const string outputFolder = @"C:\OutputPdfs";

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Verify that the input directory exists; if not, inform the user and exit gracefully.
        if (!Directory.Exists(inputFolder))
        {
            Console.WriteLine($"Input folder '{inputFolder}' does not exist. No files to process.");
            return;
        }

        // Enumerate all PDF files in the input folder
        foreach (string inputPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            // Determine output file path (same file name in output folder)
            string outputPath = Path.Combine(outputFolder, Path.GetFileName(inputPath));

            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Determine opacity based on page count.
                // Example: 5% opacity per page, capped at 0.9 (90% opacity).
                int pageCount = doc.Pages.Count;
                double opacity = Math.Min(0.9, pageCount * 0.05);

                // Prepare a TextState for the watermark text
                TextState watermarkTextState = new TextState
                {
                    Font = FontRepository.FindFont("Helvetica"),
                    FontSize = 48,
                    ForegroundColor = Aspose.Pdf.Color.Gray
                };

                // Add a WatermarkArtifact to each page
                foreach (Page page in doc.Pages)
                {
                    WatermarkArtifact watermark = new WatermarkArtifact
                    {
                        IsBackground = true,          // place behind page content
                        Opacity = opacity,            // computed per‑document opacity
                        Text = "CONFIDENTIAL",        // watermark text
                        TextState = watermarkTextState
                    };

                    page.Artifacts.Add(watermark);
                }

                // Save the modified document to the output path
                doc.Save(outputPath);
            }

            Console.WriteLine($"Processed '{Path.GetFileName(inputPath)}' -> '{outputPath}'");
        }

        Console.WriteLine("Batch watermarking completed.");
    }
}
