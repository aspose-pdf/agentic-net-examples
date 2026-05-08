using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using Aspose.Pdf.Facades;
using Aspose.Pdf;
using System.Drawing.Imaging;

namespace PdfExtractionDemo
{
    // Configuration model matching the JSON file
    public class ExtractionConfig
    {
        public bool ExtractText { get; set; } = true;
        public bool ExtractImages { get; set; } = true;
        public bool ExtractAttachments { get; set; } = true;
        // Optional output directories
        public string TextOutputPath { get; set; } = "ExtractedText.txt";
        public string ImagesOutputDir { get; set; } = "Images";
        public string AttachmentsOutputDir { get; set; } = "Attachments";
    }

    class Program
    {
        static void Main()
        {
            const string pdfPath = "input.pdf";
            const string configPath = "extractionConfig.json";

            if (!File.Exists(pdfPath))
            {
                Console.Error.WriteLine($"PDF file not found: {pdfPath}");
                return;
            }

            if (!File.Exists(configPath))
            {
                Console.Error.WriteLine($"Config file not found: {configPath}");
                return;
            }

            // Load configuration
            ExtractionConfig config;
            try
            {
                string json = File.ReadAllText(configPath);
                config = JsonSerializer.Deserialize<ExtractionConfig>(json);
                if (config == null) throw new Exception("Deserialization returned null.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to read config: {ex.Message}");
                return;
            }

            // Ensure output directories exist
            if (config.ExtractImages && !Directory.Exists(config.ImagesOutputDir))
                Directory.CreateDirectory(config.ImagesOutputDir);
            if (config.ExtractAttachments && !Directory.Exists(config.AttachmentsOutputDir))
                Directory.CreateDirectory(config.AttachmentsOutputDir);

            // Use PdfExtractor within a using block for deterministic disposal
            using (PdfExtractor extractor = new PdfExtractor())
            {
                // Bind the source PDF
                extractor.BindPdf(pdfPath);

                // -------- Text Extraction ----------
                if (config.ExtractText)
                {
                    // Use pure text mode (0) – default; can be changed if needed
                    extractor.ExtractTextMode = 0;
                    extractor.ExtractText();
                    extractor.GetText(config.TextOutputPath);
                    Console.WriteLine($"Text extracted to: {config.TextOutputPath}");
                }

                // -------- Image Extraction ----------
                if (config.ExtractImages)
                {
                    // Extract all images defined in resources (default mode)
                    extractor.ExtractImageMode = ExtractImageMode.DefinedInResources;
                    extractor.ExtractImage();

                    int imageIndex = 1;
                    while (extractor.HasNextImage())
                    {
                        string imagePath = Path.Combine(config.ImagesOutputDir, $"Image_{imageIndex}.png");
                        // Save each image as PNG; you can change ImageFormat if required
                        extractor.GetNextImage(imagePath, ImageFormat.Png);
                        Console.WriteLine($"Image saved: {imagePath}");
                        imageIndex++;
                    }
                }

                // -------- Attachment Extraction ----------
                if (config.ExtractAttachments)
                {
                    extractor.ExtractAttachment();

                    // Retrieve attachment names
                    IList<string> attachmentNames = extractor.GetAttachNames();

                    // Get all attachments as streams
                    MemoryStream[] attachmentStreams = extractor.GetAttachment();

                    for (int i = 0; i < attachmentStreams.Length; i++)
                    {
                        string name = attachmentNames[i];
                        string outputFile = Path.Combine(config.AttachmentsOutputDir, name);

                        using (FileStream fs = new FileStream(outputFile, FileMode.Create, FileAccess.Write))
                        {
                            attachmentStreams[i].Position = 0;
                            attachmentStreams[i].CopyTo(fs);
                        }

                        Console.WriteLine($"Attachment saved: {outputFile}");
                    }
                }
            }

            Console.WriteLine("Extraction completed.");
        }
    }
}