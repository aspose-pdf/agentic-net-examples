using System;
using System.IO;
using System.Text.Json;
using System.Drawing.Imaging;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

namespace PdfExtractionDemo
{
    // Configuration model matching the JSON structure
    public class ExtractionConfig
    {
        public bool ExtractText { get; set; }
        public bool ExtractImages { get; set; }
        public bool ExtractAttachments { get; set; }
    }

    class Program
    {
        static void Main()
        {
            const string inputPdfPath = "input.pdf";
            const string configPath   = "config.json";

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

            // Load configuration from JSON file
            ExtractionConfig config;
            try
            {
                string json = File.ReadAllText(configPath);
                config = JsonSerializer.Deserialize<ExtractionConfig>(json);
                if (config == null)
                {
                    Console.Error.WriteLine("Failed to parse configuration.");
                    return;
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error reading configuration: {ex.Message}");
                return;
            }

            // Prepare output directories
            const string textOutputDir       = "ExtractedText";
            const string imagesOutputDir     = "ExtractedImages";
            const string attachmentsOutputDir = "ExtractedAttachments";

            if (config.ExtractText)       Directory.CreateDirectory(textOutputDir);
            if (config.ExtractImages)     Directory.CreateDirectory(imagesOutputDir);
            if (config.ExtractAttachments)Directory.CreateDirectory(attachmentsOutputDir);

            // Use PdfExtractor from Aspose.Pdf.Facades for text and image extraction
            using (PdfExtractor extractor = new PdfExtractor())
            {
                // Bind the source PDF
                extractor.BindPdf(inputPdfPath);

                // -----------------------------------------------------------------
                // Text extraction (if enabled)
                // -----------------------------------------------------------------
                if (config.ExtractText)
                {
                    // Extract text to an in‑memory stream and then read it as a string.
                    extractor.ExtractText();
                    using (MemoryStream textStream = new MemoryStream())
                    {
                        extractor.GetText(textStream); // write extracted text to the stream
                        textStream.Position = 0;        // rewind for reading
                        using (StreamReader reader = new StreamReader(textStream))
                        {
                            string extractedText = reader.ReadToEnd();
                            string textFilePath = Path.Combine(textOutputDir,
                                Path.GetFileNameWithoutExtension(inputPdfPath) + ".txt");
                            File.WriteAllText(textFilePath, extractedText);
                            Console.WriteLine($"Text extracted to: {textFilePath}");
                        }
                    }
                }

                // -----------------------------------------------------------------
                // Image extraction (if enabled)
                // -----------------------------------------------------------------
                if (config.ExtractImages)
                {
                    // Ensure we extract images defined in resources (most common scenario)
                    extractor.ExtractImageMode = ExtractImageMode.DefinedInResources;
                    extractor.ExtractImage(); // start image extraction

                    int imageIndex = 1;
                    while (extractor.HasNextImage())
                    {
                        string outPath = Path.Combine(imagesOutputDir, $"Image_{imageIndex}.png");
                        // GetNextImage writes the image to the supplied path using the specified format.
                        extractor.GetNextImage(outPath, ImageFormat.Png);
                        Console.WriteLine($"Image extracted: {outPath}");
                        imageIndex++;
                    }
                }
            }

            // ---------------------------------------------------------------------
            // Attachment extraction (if enabled) – uses Document.EmbeddedFiles
            // ---------------------------------------------------------------------
            if (config.ExtractAttachments)
            {
                Document doc = new Document(inputPdfPath);
                if (doc.EmbeddedFiles != null && doc.EmbeddedFiles.Count > 0)
                {
                    foreach (FileSpecification fileSpec in doc.EmbeddedFiles)
                    {
                        string attachmentPath = Path.Combine(attachmentsOutputDir, fileSpec.Name);
                        using (Stream src = fileSpec.Contents)
                        using (FileStream dst = new FileStream(attachmentPath, FileMode.Create, FileAccess.Write))
                        {
                            src.CopyTo(dst);
                        }
                        Console.WriteLine($"Attachment extracted: {attachmentPath}");
                    }
                }
                else
                {
                    Console.WriteLine("No attachments found in the PDF.");
                }
            }
        }
    }
}
