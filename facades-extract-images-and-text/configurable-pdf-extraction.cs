using System;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

namespace PdfExtractionDemo
{
    // Configuration model matching the JSON file
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
                config = JsonSerializer.Deserialize<ExtractionConfig>(json) ?? throw new InvalidOperationException("Configuration deserialization returned null.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to read configuration: {ex.Message}");
                return;
            }

            // Use PdfExtractor (Facades API) inside a using block for deterministic disposal
            using (PdfExtractor extractor = new PdfExtractor())
            {
                // Bind the source PDF
                extractor.BindPdf(inputPdfPath);

                // -----------------------------------------------------------------
                // Text extraction (if enabled)
                // -----------------------------------------------------------------
                if (config.ExtractText)
                {
                    extractor.ExtractText();
                    // GetText writes to a stream – use a MemoryStream and read the text back
                    using (MemoryStream textStream = new MemoryStream())
                    {
                        extractor.GetText(textStream);
                        textStream.Position = 0;
                        using (StreamReader reader = new StreamReader(textStream))
                        {
                            string extractedText = reader.ReadToEnd();
                            string textOutputPath = Path.ChangeExtension(inputPdfPath, ".txt");
                            File.WriteAllText(textOutputPath, extractedText);
                            Console.WriteLine($"Text extracted to: {textOutputPath}");
                        }
                    }
                }

                // -----------------------------------------------------------------
                // Image extraction (if enabled)
                // -----------------------------------------------------------------
                if (config.ExtractImages)
                {
                    // The ExtractImageMode property does not exist in the current Aspose.Pdf version.
                    // Calling ExtractImage() extracts all images by default.
                    extractor.ExtractImage();

                    string imagesDir = Path.Combine(Path.GetDirectoryName(inputPdfPath) ?? string.Empty, "ExtractedImages");
                    Directory.CreateDirectory(imagesDir);

                    int imageIndex = 1;
                    while (extractor.HasNextImage())
                    {
                        string imgPath = Path.Combine(imagesDir, $"Image_{imageIndex}.png");
                        // GetNextImage saves the current image to the supplied path
                        extractor.GetNextImage(imgPath);
                        Console.WriteLine($"Image {imageIndex} saved to: {imgPath}");
                        imageIndex++;
                    }
                }

                // -----------------------------------------------------------------
                // Attachment extraction (if enabled)
                // -----------------------------------------------------------------
                if (config.ExtractAttachments)
                {
                    // Use the Document API to enumerate embedded files.
                    using (Document pdfDoc = new Document(inputPdfPath))
                    {
                        string attachDir = Path.Combine(Path.GetDirectoryName(inputPdfPath) ?? string.Empty, "ExtractedAttachments");
                        Directory.CreateDirectory(attachDir);

                        if (pdfDoc.EmbeddedFiles != null && pdfDoc.EmbeddedFiles.Count > 0)
                        {
                            foreach (FileSpecification fileSpec in pdfDoc.EmbeddedFiles)
                            {
                                // Use the Name property for the file name; fall back to a GUID if empty.
                                string attachmentName = string.IsNullOrWhiteSpace(fileSpec.Name)
                                    ? $"Attachment_{Guid.NewGuid()}.bin"
                                    : fileSpec.Name;

                                string attachPath = Path.Combine(attachDir, attachmentName);
                                using (FileStream fs = new FileStream(attachPath, FileMode.Create, FileAccess.Write))
                                {
                                    // The attachment data is available via the Contents stream.
                                    if (fileSpec.Contents != null)
                                    {
                                        if (fileSpec.Contents.CanSeek)
                                            fileSpec.Contents.Position = 0;
                                        fileSpec.Contents.CopyTo(fs);
                                    }
                                }
                                Console.WriteLine($"Attachment saved to: {attachPath}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("No attachments found in the PDF.");
                        }
                    }
                }
            }

            Console.WriteLine("Extraction process completed.");
        }
    }
}
