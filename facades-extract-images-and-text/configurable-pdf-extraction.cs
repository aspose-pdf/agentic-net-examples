using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf.Facades;

namespace PdfExtractionDemo
{
    // Configuration model matching the JSON file
    public class ExtractionConfig
    {
        public required string InputPdfPath { get; set; }
        public required string OutputDirectory { get; set; }
        public bool ExtractText { get; set; }
        public bool ExtractImages { get; set; }
        public bool ExtractAttachments { get; set; }
    }

    class Program
    {
        static void Main()
        {
            // Load configuration from appsettings.json (placed beside executable)
            const string configPath = "appsettings.json";
            if (!File.Exists(configPath))
            {
                Console.Error.WriteLine($"Configuration file not found: {configPath}");
                return;
            }

            ExtractionConfig? config;
            try
            {
                string json = File.ReadAllText(configPath);
                config = JsonSerializer.Deserialize<ExtractionConfig>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to read configuration: {ex.Message}");
                return;
            }

            if (config == null ||
                string.IsNullOrWhiteSpace(config.InputPdfPath) ||
                string.IsNullOrWhiteSpace(config.OutputDirectory))
            {
                Console.Error.WriteLine("Invalid configuration values.");
                return;
            }

            if (!File.Exists(config.InputPdfPath))
            {
                Console.Error.WriteLine($"Input PDF not found: {config.InputPdfPath}");
                return;
            }

            Directory.CreateDirectory(config.OutputDirectory);

            // Use PdfExtractor facade to perform the requested extractions
            using (PdfExtractor extractor = new PdfExtractor())
            {
                extractor.BindPdf(config.InputPdfPath);

                // Text extraction
                if (config.ExtractText)
                {
                    try
                    {
                        extractor.ExtractText();
                        string textOutput = Path.Combine(config.OutputDirectory, "extracted_text.txt");
                        extractor.GetText(textOutput);
                        Console.WriteLine($"Text extracted to: {textOutput}");
                    }
                    catch (Exception ex)
                    {
                        Console.Error.WriteLine($"Text extraction failed: {ex.Message}");
                    }
                }

                // Image extraction
                if (config.ExtractImages)
                {
                    try
                    {
                        extractor.ExtractImage();
                        int imageIndex = 1;
                        while (extractor.HasNextImage())
                        {
                            string imagePath = Path.Combine(config.OutputDirectory, $"image_{imageIndex}.png");
                            extractor.GetNextImage(imagePath);
                            Console.WriteLine($"Image {imageIndex} saved to: {imagePath}");
                            imageIndex++;
                        }

                        if (imageIndex == 1)
                            Console.WriteLine("No images found in the PDF.");
                    }
                    catch (Exception ex)
                    {
                        Console.Error.WriteLine($"Image extraction failed: {ex.Message}");
                    }
                }

                // Attachment extraction
                if (config.ExtractAttachments)
                {
                    try
                    {
                        extractor.ExtractAttachment();
                        IList<string> attachNames = extractor.GetAttachNames();
                        MemoryStream[] attachStreams = extractor.GetAttachment();

                        for (int i = 0; i < attachStreams.Length; i++)
                        {
                            string name = attachNames[i] ?? $"attachment_{i}";
                            string attachPath = Path.Combine(config.OutputDirectory, name);
                            using (FileStream fs = new FileStream(attachPath, FileMode.Create, FileAccess.Write))
                            {
                                attachStreams[i].Position = 0;
                                attachStreams[i].CopyTo(fs);
                            }
                            Console.WriteLine($"Attachment saved to: {attachPath}");
                        }

                        if (attachNames.Count == 0)
                            Console.WriteLine("No attachments found in the PDF.");
                    }
                    catch (Exception ex)
                    {
                        Console.Error.WriteLine($"Attachment extraction failed: {ex.Message}");
                    }
                }
            }
        }
    }
}
