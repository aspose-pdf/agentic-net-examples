using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

namespace PdfWatermarkBatch
{
    // Simple configuration holder – replace values as needed
    public class WatermarkConfig
    {
        // Path to the image that will be used as watermark
        public string WatermarkImagePath { get; set; } = "watermark.png";

        // Opacity of the watermark (0.0 – fully transparent, 1.0 – fully opaque)
        public float Opacity { get; set; } = 0.3f;

        // Should the watermark appear behind the page content?
        public bool IsBackground { get; set; } = true;

        // Position of the watermark on each page (lower‑left corner)
        public float PositionX { get; set; } = 100f;
        public float PositionY { get; set; } = 200f;

        // Folder containing source PDFs
        public string InputFolder { get; set; } = "InputPdfs";

        // Folder where watermarked PDFs will be written
        public string OutputFolder { get; set; } = "OutputPdfs";
    }

    class Program
    {
        static void Main()
        {
            WatermarkConfig config = new WatermarkConfig();

            // Ensure output directory exists
            Directory.CreateDirectory(config.OutputFolder);

            // Validate watermark image
            if (!File.Exists(config.WatermarkImagePath))
            {
                Console.Error.WriteLine($"Watermark image not found: {config.WatermarkImagePath}");
                return;
            }

            // Process each PDF file in the input folder
            foreach (string pdfPath in Directory.GetFiles(config.InputFolder, "*.pdf"))
            {
                string fileName = Path.GetFileName(pdfPath);
                string outputPath = Path.Combine(config.OutputFolder, fileName);

                try
                {
                    // Load the source PDF (lifecycle: create -> load -> use)
                    using (Document doc = new Document(pdfPath))
                    {
                        // Initialize the PdfFileStamp facade on the loaded document
                        PdfFileStamp fileStamp = new PdfFileStamp(doc);

                        // Create a Aspose.Pdf.Facades.Stamp object that represents the watermark
                        Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();

                        // Bind the watermark image
                        stamp.BindImage(config.WatermarkImagePath);

                        // Set position, opacity and background flag
                        stamp.SetOrigin(config.PositionX, config.PositionY);
                        stamp.Opacity = config.Opacity;
                        stamp.IsBackground = config.IsBackground;

                        // Add the stamp to the document
                        fileStamp.AddStamp(stamp);

                        // Save the watermarked PDF (lifecycle: save)
                        fileStamp.Save(outputPath);

                        // Close the facade (releases internal resources)
                        fileStamp.Close();
                    }

                    Console.WriteLine($"Watermarked PDF saved: {outputPath}");
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
                }
            }
        }
    }
}