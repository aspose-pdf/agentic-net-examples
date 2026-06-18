using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

namespace PdfExtractionDemo
{
    // Configuration model matching the JSON structure
    public class ExtractionConfig
    {
        public bool ExtractText { get; set; } = true;
        public bool ExtractImages { get; set; } = true;
        public bool ExtractAttachments { get; set; } = true;
        public string InputPdfPath { get; set; } = "input.pdf";
        public string OutputDirectory { get; set; } = "ExtractionOutput";
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Determine config file location (default: config.json)
            string configPath = args.Length > 0 ? args[0] : "config.json";

            if (!File.Exists(configPath))
            {
                Console.Error.WriteLine($"Configuration file not found: {configPath}");
                return;
            }

            // Deserialize configuration
            ExtractionConfig config;
            try
            {
                string json = File.ReadAllText(configPath);
                config = JsonSerializer.Deserialize<ExtractionConfig>(json);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to read configuration: {ex.Message}");
                return;
            }

            // Validate input PDF
            if (!File.Exists(config.InputPdfPath))
            {
                Console.Error.WriteLine($"Input PDF not found: {config.InputPdfPath}");
                return;
            }

            // Ensure output directory exists
            Directory.CreateDirectory(config.OutputDirectory);

            // Use PdfExtractor facade inside a using block for deterministic disposal
            using (PdfExtractor extractor = new PdfExtractor())
            {
                // Bind the source PDF file
                extractor.BindPdf(config.InputPdfPath);

                // ---------- Text Extraction ----------
                if (config.ExtractText)
                {
                    try
                    {
                        // Extract all text in pure mode (0) – default
                        extractor.ExtractText();
                        string textOutputPath = Path.Combine(config.OutputDirectory, "ExtractedText.txt");
                        extractor.GetText(textOutputPath);
                        Console.WriteLine($"Text extracted to: {textOutputPath}");
                    }
                    catch (Exception ex)
                    {
                        Console.Error.WriteLine($"Text extraction failed: {ex.Message}");
                    }
                }

                // ---------- Image Extraction ----------
                if (config.ExtractImages)
                {
                    try
                    {
                        // Extract images defined in resources (default mode)
                        extractor.ExtractImage();

                        int imageIndex = 1;
                        while (extractor.HasNextImage())
                        {
                            string imagePath = Path.Combine(config.OutputDirectory, $"Image_{imageIndex}.png");
                            // Retrieve next image in PNG format
                            extractor.GetNextImage(imagePath, System.Drawing.Imaging.ImageFormat.Png);
                            Console.WriteLine($"Image {imageIndex} saved to: {imagePath}");
                            imageIndex++;
                        }

                        if (imageIndex == 1)
                        {
                            Console.WriteLine("No images found in the PDF.");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.Error.WriteLine($"Image extraction failed: {ex.Message}");
                    }
                }

                // ---------- Attachment Extraction ----------
                if (config.ExtractAttachments)
                {
                    try
                    {
                        // Extract all attachments to memory streams
                        extractor.ExtractAttachment();
                        IList<string> attachmentNames = extractor.GetAttachNames();

                        // Retrieve streams for each attachment
                        MemoryStream[] attachmentStreams = extractor.GetAttachment();

                        for (int i = 0; i < attachmentStreams.Length; i++)
                        {
                            string attachmentPath = Path.Combine(config.OutputDirectory, attachmentNames[i]);
                            using (FileStream fs = new FileStream(attachmentPath, FileMode.Create, FileAccess.Write))
                            {
                                attachmentStreams[i].Position = 0;
                                attachmentStreams[i].CopyTo(fs);
                            }
                            Console.WriteLine($"Attachment saved to: {attachmentPath}");
                        }

                        if (attachmentNames.Count == 0)
                        {
                            Console.WriteLine("No attachments found in the PDF.");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.Error.WriteLine($"Attachment extraction failed: {ex.Message}");
                    }
                }
            }

            Console.WriteLine("Extraction process completed.");
        }
    }
}