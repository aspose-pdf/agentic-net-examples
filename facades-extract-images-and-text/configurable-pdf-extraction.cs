using System;
using System.IO;
using System.Text.Json;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging; // required for ImageFormat when saving images

// Configuration model matching the JSON file structure
public class ExtractionConfig
{
    public bool ExtractText { get; set; }
    public bool ExtractImages { get; set; }
    public bool ExtractAttachments { get; set; }
    public string? InputPdf { get; set; }          // Path to the source PDF (nullable to satisfy compiler)
    public string? OutputFolder { get; set; }      // Folder where results will be written (nullable)
}

class Program
{
    static void Main()
    {
        const string configPath = "extractConfig.json";

        // Verify configuration file exists
        if (!File.Exists(configPath))
        {
            Console.Error.WriteLine($"Configuration file not found: {configPath}");
            return;
        }

        // Deserialize JSON configuration
        ExtractionConfig? cfg;
        try
        {
            string json = File.ReadAllText(configPath);
            cfg = JsonSerializer.Deserialize<ExtractionConfig>(json);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to read configuration: {ex.Message}");
            return;
        }

        // Ensure deserialization succeeded
        if (cfg == null)
        {
            Console.Error.WriteLine("Configuration could not be parsed.");
            return;
        }

        // Validate input PDF path
        if (string.IsNullOrWhiteSpace(cfg.InputPdf) || !File.Exists(cfg.InputPdf))
        {
            Console.Error.WriteLine("Input PDF path is missing or the file does not exist.");
            return;
        }

        // Ensure output directory exists (fallback to "Output" if not supplied)
        string outputDir = string.IsNullOrWhiteSpace(cfg.OutputFolder) ? "Output" : cfg.OutputFolder;
        Directory.CreateDirectory(outputDir);

        // Use PdfExtractor (Facade) inside a using block for deterministic disposal
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the source PDF file
            extractor.BindPdf(cfg.InputPdf);

            // -------------------- Text Extraction --------------------
            if (cfg.ExtractText)
            {
                extractor.ExtractText();
                string textFile = Path.Combine(outputDir, "extracted_text.txt");
                extractor.GetText(textFile);
                Console.WriteLine($"Text extracted to: {textFile}");
            }

            // -------------------- Image Extraction --------------------
            if (cfg.ExtractImages)
            {
                // The ExtractImageMode property does not exist in the referenced Aspose.Pdf version.
                // Calling ExtractImage() extracts all images by default.
                extractor.ExtractImage();

                int imageIndex = 1;
                while (extractor.HasNextImage())
                {
                    string imageFile = Path.Combine(outputDir, $"image_{imageIndex}.png");
                    extractor.GetNextImage(imageFile, ImageFormat.Png);
                    Console.WriteLine($"Image {imageIndex} saved to: {imageFile}");
                    imageIndex++;
                }
            }

            // -------------------- Attachment Extraction --------------------
            if (cfg.ExtractAttachments)
            {
                extractor.ExtractAttachment();
                extractor.GetAttachment(outputDir);
                Console.WriteLine($"Attachments saved to folder: {outputDir}");
            }
        }
    }
}
