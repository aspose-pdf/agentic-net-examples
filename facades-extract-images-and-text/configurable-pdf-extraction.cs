using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging; // ImageFormat for image extraction

// Configuration model for toggling extraction features
public class ExtractionConfig
{
    public string InputPdfPath { get; set; }          // Path to the source PDF
    public string OutputDirectory { get; set; }      // Base folder for all extracted files
    public bool ExtractText { get; set; }             // Enable/disable text extraction
    public bool ExtractImages { get; set; }           // Enable/disable image extraction
    public bool ExtractAttachments { get; set; }      // Enable/disable attachment extraction
}

// Main program
class Program
{
    static void Main()
    {
        const string configPath = "config.json";

        if (!File.Exists(configPath))
        {
            Console.Error.WriteLine($"Configuration file not found: {configPath}");
            return;
        }

        // Load configuration
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

        // Validate required fields
        if (string.IsNullOrWhiteSpace(config.InputPdfPath) || !File.Exists(config.InputPdfPath))
        {
            Console.Error.WriteLine("Input PDF path is missing or the file does not exist.");
            return;
        }

        if (string.IsNullOrWhiteSpace(config.OutputDirectory))
        {
            Console.Error.WriteLine("Output directory is not specified.");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(config.OutputDirectory);

        // Use PdfExtractor facade to perform the requested extractions
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the source PDF file
            extractor.BindPdf(config.InputPdfPath);

            // ---------- Text Extraction ----------
            if (config.ExtractText)
            {
                try
                {
                    // Extract all text from the document
                    extractor.ExtractText();

                    // Save extracted text to a .txt file
                    string textOutputPath = Path.Combine(config.OutputDirectory, "extracted_text.txt");
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

                    // Prepare a subfolder for images
                    string imagesDir = Path.Combine(config.OutputDirectory, "Images");
                    Directory.CreateDirectory(imagesDir);

                    int imageIndex = 1;
                    // Retrieve each image sequentially
                    while (extractor.HasNextImage())
                    {
                        string imagePath = Path.Combine(imagesDir, $"image_{imageIndex}.png");
                        // Save image as PNG; you can change the format if needed
                        extractor.GetNextImage(imagePath, ImageFormat.Png);
                        Console.WriteLine($"Image {imageIndex} saved to: {imagePath}");
                        imageIndex++;
                    }

                    if (imageIndex == 1)
                    {
                        Console.WriteLine("No images were found in the PDF.");
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
                    // Extract all attachments from the PDF
                    extractor.ExtractAttachment();

                    // Prepare a subfolder for attachments
                    string attachDir = Path.Combine(config.OutputDirectory, "Attachments");
                    Directory.CreateDirectory(attachDir);

                    // Save all attachments to the directory
                    extractor.GetAttachment(attachDir);
                    Console.WriteLine($"Attachments extracted to: {attachDir}");
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Attachment extraction failed: {ex.Message}");
                }
            }
        }
    }
}