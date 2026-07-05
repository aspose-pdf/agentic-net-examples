using System;
using System.IO;
using Aspose.Pdf.Facades; // Facade classes for stamping
using Aspose.Pdf;          // Core PDF types (e.g., Color)

// Configuration class for watermark settings
public class WatermarkConfig
{
    // Path to the image file used as watermark
    public string ImagePath { get; set; } = "watermark.png";

    // Opacity of the watermark (0.0 = fully transparent, 1.0 = fully opaque)
    public float Opacity { get; set; } = 0.5f;

    // Whether the watermark should appear behind page content
    public bool IsBackground { get; set; } = true;

    // Position of the watermark on each page (in points)
    public float PositionX { get; set; } = 100f; // left coordinate
    public float PositionY { get; set; } = 500f; // bottom coordinate
}

// Main program that applies the watermark to all PDFs in a folder
class Program
{
    static void Main()
    {
        // Folder containing PDFs to process
        const string inputFolder = @"C:\PdfFolder";

        // Output folder for watermarked PDFs (can be the same as inputFolder)
        const string outputFolder = @"C:\PdfFolder\Watermarked";

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load watermark configuration (could be read from a file; here we use defaults)
        WatermarkConfig config = new WatermarkConfig();

        // Validate that the watermark image exists
        if (!File.Exists(config.ImagePath))
        {
            Console.Error.WriteLine($"Watermark image not found: {config.ImagePath}");
            return;
        }

        // Process each PDF file in the input folder
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            // Build the output file name (original name with "_watermarked" suffix)
            string outputPath = Path.Combine(
                outputFolder,
                Path.GetFileNameWithoutExtension(pdfPath) + "_watermarked.pdf");

            try
            {
                // Initialize the PdfFileStamp facade
                using (PdfFileStamp fileStamp = new PdfFileStamp())
                {
                    // Bind the source PDF file
                    fileStamp.BindPdf(pdfPath);

                    // Create a fully qualified Stamp instance
                    Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();

                    // Bind the watermark image to the stamp
                    stamp.BindImage(config.ImagePath);

                    // Set the position where the watermark will be placed (float values required)
                    stamp.SetOrigin(config.PositionX, config.PositionY);

                    // Configure appearance
                    stamp.Opacity = config.Opacity;
                    stamp.IsBackground = config.IsBackground;

                    // Add the stamp to the PDF
                    fileStamp.AddStamp(stamp);

                    // Save the watermarked PDF to the output path
                    fileStamp.Save(outputPath);

                    // Close the facade (optional, called automatically by using)
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
