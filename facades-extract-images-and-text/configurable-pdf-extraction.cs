using System;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

namespace PdfExtractionDemo
{
    // Simple configuration model matching the JSON file structure
    public class ExtractionConfig
    {
        public bool ExtractText { get; set; }
        public bool ExtractImages { get; set; }
        public bool ExtractAttachments { get; set; }
        // Output directories (optional, defaults will be used if null or empty)
        public string TextOutputPath { get; set; }
        public string ImagesOutputDir { get; set; }
        public string AttachmentsOutputDir { get; set; }
    }

    class Program
    {
        static void Main()
        {
            const string inputPdfPath = "input.pdf";
            const string configPath   = "extractionConfig.json";

            if (!File.Exists(inputPdfPath))
            {
                Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
                return;
            }

            if (!File.Exists(configPath))
            {
                Console.Error.WriteLine($"Configuration file not found: {configPath}");
                return;
            }

            // Load configuration (JSON) – no code changes required to toggle extraction options
            ExtractionConfig config = JsonSerializer.Deserialize<ExtractionConfig>(File.ReadAllText(configPath));

            // Apply defaults for output locations if not provided
            string textOutputPath = string.IsNullOrWhiteSpace(config.TextOutputPath) ? "extractedText.txt" : config.TextOutputPath;
            string imagesOutputDir = string.IsNullOrWhiteSpace(config.ImagesOutputDir) ? "ExtractedImages" : config.ImagesOutputDir;
            string attachmentsOutputDir = string.IsNullOrWhiteSpace(config.AttachmentsOutputDir) ? "ExtractedAttachments" : config.AttachmentsOutputDir;

            // Ensure output directories exist
            if (config.ExtractImages && !Directory.Exists(imagesOutputDir))
                Directory.CreateDirectory(imagesOutputDir);
            if (config.ExtractAttachments && !Directory.Exists(attachmentsOutputDir))
                Directory.CreateDirectory(attachmentsOutputDir);

            // Use Aspose.Pdf Document inside a using block (lifecycle rule)
            using (Document doc = new Document(inputPdfPath))
            {
                // PdfExtractor also implements IDisposable – wrap in using
                using (PdfExtractor extractor = new PdfExtractor())
                {
                    // Bind the PDF document to the extractor
                    extractor.BindPdf(doc);

                    // ---------- Text Extraction ----------
                    if (config.ExtractText)
                    {
                        // Extract all text from the document
                        extractor.ExtractText();
                        // Save extracted text to the configured file
                        extractor.GetText(textOutputPath);
                        Console.WriteLine($"Text extracted to: {textOutputPath}");
                    }

                    // ---------- Image Extraction ----------
                    if (config.ExtractImages)
                    {
                        // Use the mode that extracts actually used images (optional)
                        extractor.ExtractImageMode = ExtractImageMode.ActuallyUsed;
                        extractor.ExtractImage();

                        int imageIndex = 1;
                        while (extractor.HasNextImage())
                        {
                            // Save each image as a separate file (PNG format)
                            string imagePath = Path.Combine(imagesOutputDir, $"Image_{imageIndex}.png");
                            extractor.GetNextImage(imagePath, System.Drawing.Imaging.ImageFormat.Png);
                            Console.WriteLine($"Image {imageIndex} saved to: {imagePath}");
                            imageIndex++;
                        }
                    }

                    // ---------- Attachment Extraction ----------
                    if (config.ExtractAttachments)
                    {
                        // Extract all attachments from the PDF
                        extractor.ExtractAttachment();

                        // Retrieve attachment names (must call after ExtractAttachment)
                        var attachmentNames = extractor.GetAttachNames();

                        // Retrieve attachment streams
                        MemoryStream[] attachmentStreams = extractor.GetAttachment();

                        for (int i = 0; i < attachmentStreams.Length; i++)
                        {
                            string name = attachmentNames[i] as string ?? $"Attachment_{i}";
                            string attachmentPath = Path.Combine(attachmentsOutputDir, name);

                            // Write the stream to a file
                            using (FileStream fs = new FileStream(attachmentPath, FileMode.Create, FileAccess.Write))
                            {
                                attachmentStreams[i].Position = 0;
                                attachmentStreams[i].CopyTo(fs);
                            }

                            Console.WriteLine($"Attachment saved to: {attachmentPath}");
                        }
                    }
                }
            }

            Console.WriteLine("Extraction process completed.");
        }
    }
}